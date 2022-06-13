using PagesCommon.Enums;

namespace PagesCommon.DTOs
{
    public class PostListItemDTO
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string SourceName { get; set; }

        public PostStatus Status { get; set; }
    }
}
