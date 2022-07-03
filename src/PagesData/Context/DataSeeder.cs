using PagesData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
