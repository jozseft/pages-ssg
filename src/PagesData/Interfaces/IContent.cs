namespace PagesData.Interfaces
{
    public interface IContent : IEntity
    {
        public string Title { get; set; }

        public string SourceName { get; set; }
    }
}
