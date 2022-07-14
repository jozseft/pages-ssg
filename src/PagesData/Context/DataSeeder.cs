using Microsoft.AspNetCore.Identity;
using PagesData.Entities;

namespace PagesData.Context
{
    public class DataSeeder
    {
        private readonly PagesContext _context;
        private readonly UserManager<User> _userManager;

        public DataSeeder(PagesContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed() 
        {
            _context.Database.EnsureCreated();

            await SeedUserAdmin();
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

        public async Task SeedUserAdmin() 
        {
            string email = "admin@example.com";
            User user = await _userManager.FindByEmailAsync(email);

            if (user == null) {
                string password = "Admin#45";
                User newUser = new User
                {
                    UserName = "Administrator",
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = "AdminFirstName",
                    LastName = "AdminLastName"
                };

                await _userManager.CreateAsync(newUser, password);
            }
        }
    }
}
