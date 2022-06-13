using PagesCommon.Enums;
using PagesData.Context;
using PagesData.Entities;
using PagesData.Interfaces;

namespace PagesData.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PagesContext _context;

        public PostRepository(PagesContext context)
        {
            _context = context;
        }

        public void SavePost(Post post)
        {
            _context.Posts.Add(post);
            _context.SaveChanges();
        }

        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.AsEnumerable<Post>();
        }

        public Post GetPost(Guid id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public void UpdatePostTitleAndSourceName(Post post) 
        {
            Post dbPost = _context.Posts.FirstOrDefault(p => p.Id == post.Id);

            if (dbPost != null) 
            { 
                dbPost.Title = post.Title;
                dbPost.SourceName = post.SourceName;

                _context.SaveChanges();
            }
        }

        public void UpdatePostStatus(Guid id, PostStatus status)
        {
            Post dbPost = _context.Posts.FirstOrDefault(p => p.Id == id);

            if (dbPost != null)
            {
                dbPost.Status = status;

                _context.SaveChanges();
            }
        }
    }
}
