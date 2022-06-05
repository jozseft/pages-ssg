using PagesData.Entities;

namespace PagesData.Interfaces
{
    public interface IPostRepository
    {
        void SavePost(Post post);

        IEnumerable<Post> GetAllPosts();
    }
}
