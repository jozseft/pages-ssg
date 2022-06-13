using PagesCommon.DTOs;

namespace PagesServices.Interfaces
{
    public interface IPostService
    {
        Guid SavePost(PostDTO content);

        IEnumerable<PostListItemDTO> GetAllPosts();

        PostDTO GetPost(Guid id);

        bool PublishPost(Guid id);
    }
}
