using Microsoft.Extensions.Options;
using PagesCommon.DTOs;
using PagesCommon.Interfaces;
using PagesConfig;
using PagesData.Entities;
using PagesData.Interfaces;
using PagesServices.Interfaces;
using PagesCommon.Enums;

namespace PagesServices.Services
{
    public class PostService : IPostService
    {
        private readonly IOptions<FilesConfig> _filesConfig;
        private readonly IPostProcessor _postProcessor;
        private readonly IPostRepository _postRepository;

        public PostService(IOptions<FilesConfig> filesConfig, IPostProcessor postProcessor, IPostRepository postRepository)
        {
            _filesConfig = filesConfig;
            _postProcessor = postProcessor;
            _postRepository = postRepository;
        }

        public void SavePost(PostDTO newPost)
        {
            string sourceName = newPost.Title.ToLower().Replace(" ", "-");
            WriteFile(_filesConfig.Value.MarkdownFilesPath + $"{sourceName}.md", newPost.MarkdownText);

            var post = new Post 
            { 
                Id = newPost.Id ?? Guid.NewGuid(),
                Title = newPost.Title,
                SourceName = sourceName,
                Status = PostStatus.Writing
            };

            _postRepository.SavePost(post);

            if (!Directory.Exists(_filesConfig.Value.HTMLPagesPath + sourceName))
                Directory.CreateDirectory(_filesConfig.Value.HTMLPagesPath + sourceName);

            string htmlPath = _filesConfig.Value.HTMLPagesPath + $"{sourceName}\\index.html";
            WriteFile(htmlPath, putHtmlBaseTags(newPost.Title, _postProcessor.GetHtmlByMarkdown(newPost.MarkdownText)));
        }

        public IEnumerable<PostListItemDTO> GetAllPosts() 
        {
            return _postRepository.GetAllPosts().Select(p => new PostListItemDTO
            {
                Id = p.Id,
                Title = p.Title,
                SourceName = p.SourceName
            }).AsEnumerable();
        }

        private void WriteFile(string path, string content)
        {
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteAsync(content);

                sw.Close();
            }
        }

        private string putHtmlBaseTags(string title, string content)
        {
            return @$"<!DOCTYPE html>
                    <html>
                        <head>
                            <title>{title}</title>
                            <link rel='stylesheet' href='../css/post.css'>
                        </head>
                    <body>
                        {content}    
                    </body>
                    </html>";
        }
    }
}

