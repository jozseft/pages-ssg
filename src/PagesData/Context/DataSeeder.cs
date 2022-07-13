using PagesData.Entities;

namespace PagesData.Context
{
    public class DataSeeder
    {
        private readonly PagesContext _context;

        public DataSeeder(PagesContext context)
        {
            _context = context;
        }

        public void Seed() 
        {
            _context.Database.EnsureCreated();

            SeedFirstPost();
        }

        private void SeedFirstPost() 
        {
            string firstPostSourceName = "first-post";
            Post firstPost = _context.Posts.FirstOrDefault(p => p.SourceName == firstPostSourceName);

            if (firstPost == null)
            {
                Post newPost = new Post
                {
                    Id = Guid.NewGuid(),
                    Title = "First Post",
                    SourceName = firstPostSourceName,
                    Status = PagesCommon.Enums.PostStatus.Published,
                    PublishedDate = DateTime.UtcNow
                };

                _context.Posts.Add(newPost);
                _context.SaveChanges();
            }
        }
    }
}
