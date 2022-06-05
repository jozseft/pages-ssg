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
    }
}
