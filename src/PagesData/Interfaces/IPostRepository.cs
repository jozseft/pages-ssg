using PagesCommon.Enums;
using PagesData.Entities;

namespace PagesData.Interfaces
{
    public interface IPostRepository
    {
        void SavePost(Post post);

        IEnumerable<Post> GetAllPosts();

        Post GetPost(Guid id);

        void UpdatePostTitleAndSourceName(Post post);

        void UpdatePostStatus(Guid id, PostStatus status);
    }
}
