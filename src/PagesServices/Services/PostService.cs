using Microsoft.Extensions.Options;
using PagesCommon.DTOs;
using PagesCommon.Interfaces;
using PagesConfig;
using PagesData.Entities;
using PagesData.Interfaces;
using PagesServices.Interfaces;
using PagesCommon.Enums;
using System.IO;

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

        public Guid SavePost(PostDTO newPost)
        {
            string sourceName = newPost.Title.ToLower().Replace(" ", "-");

            WriteFile(_filesConfig.Value.MarkdownFilesPath + $"{sourceName}.md", newPost.MarkdownText);

            if (newPost.Id.HasValue)
            {
                Post post = _postRepository.GetPost(newPost.Id.Value);

                if (post != null && post.SourceName != sourceName)
                {
                    File.Delete(_filesConfig.Value.MarkdownFilesPath + $"{post.SourceName}.md");

                    post.Title = newPost.Title;
                    post.SourceName = sourceName;

                    _postRepository.UpdatePostTitleAndSourceName(post);
                }
            }
            else
            {
                var post = new Post
                {
                    Id = newPost.Id ?? Guid.NewGuid(),
                    Title = newPost.Title,
                    SourceName = sourceName,
                    Status = PostStatus.Writing
                };

                _postRepository.SavePost(post);

                newPost.Id = post.Id;
            }

            return newPost.Id.Value;
        }

        public IEnumerable<PostListItemDTO> GetAllPosts()
        {
            return _postRepository.GetAllPosts().Select(p => new PostListItemDTO
            {
                Id = p.Id,
                Title = p.Title,
                SourceName = p.SourceName,
                Status = p.Status
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

        public PostDTO GetPost(Guid id)
        {
            Post post = _postRepository.GetPost(id);

            if (post != null)
            {
                return new PostDTO
                {
                    Title = post.Title,
                    Id = id,
                    SourceName = post.SourceName,
                    MarkdownText = File.ReadAllText(_filesConfig.Value.MarkdownFilesPath + $"{post.SourceName}.md")
                };
            };

            return null;
        }

        public bool PublishPost(Guid id)
        {
            Post post = _postRepository.GetPost(id);

            if (post != null)
            {
                string markdownText = File.ReadAllText(_filesConfig.Value.MarkdownFilesPath + $"{post.SourceName}.md");

                if (!Directory.Exists(_filesConfig.Value.HTMLPagesPath + post.SourceName))
                    Directory.CreateDirectory(_filesConfig.Value.HTMLPagesPath + post.SourceName);

                string htmlPath = _filesConfig.Value.HTMLPagesPath + $"{post.SourceName}\\index.html";
                WriteFile(htmlPath, putHtmlBaseTags(post.Title, _postProcessor.GetHtmlByMarkdown(markdownText)));

                post.Status = PostStatus.Published;
                post.PublishedDate = DateTime.UtcNow;

                _postRepository.UpdatePost(post);

                return true;
            };

            return false;
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

