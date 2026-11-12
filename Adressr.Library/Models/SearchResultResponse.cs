namespace Adressr.Library.Models
{
    public static class SearchResultTypes
    {
        public const string User = "user";
        public const string Company = "company";
    }

    public class SearchResultResponse
    {
        public required string SearchResultType { get; set; }
        public required string Key { get; set; }
        public required string Name { get; set; }
        public required string ImageLocation { get; set; }
    }
}