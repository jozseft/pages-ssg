using System.ComponentModel.DataAnnotations;

namespace PagesCommon.DTOs
{
    public class PostDTO
    {
        public Guid? Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string MarkdownText { get; set; }

        public string? SourceName { get; set; }
    }
}
