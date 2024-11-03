namespace KMZWDotNetCore.ServiceRestApi.ViewModel
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }

        public string AuthorName { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public int IsDelete { get; set; }
    }
}
