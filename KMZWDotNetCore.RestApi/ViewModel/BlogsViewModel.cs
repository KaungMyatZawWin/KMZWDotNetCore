namespace KMZWDotNetCore.RestApi.ViewModel
{
    public class BlogsViewModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int DeleteFlag { get; set; }
    }
}
