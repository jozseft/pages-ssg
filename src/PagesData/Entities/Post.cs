using PagesCommon.Enums;
using PagesData.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace PagesData.Entities
{
    public class Post : IContent
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        [MaxLength(170)]
        public string SourceName { get; set; }

        public PostStatus Status { get; set; }

        public DateTime? PublishedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
    }
}
