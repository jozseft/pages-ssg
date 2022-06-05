using PagesCommon.DTOs;

namespace PagesServices.Interfaces
{
    public interface IPostService
    {
        void SavePost(PostDTO content);

        IEnumerable<PostListItemDTO> GetAllPosts();
    }
}
