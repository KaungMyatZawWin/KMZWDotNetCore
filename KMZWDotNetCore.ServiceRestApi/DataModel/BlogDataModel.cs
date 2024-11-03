namespace KMZWDotNetCore.ServiceRestApi.DataModel
{
    public class BlogDataModel
    {

        public int BlogId { get; set; }

        public string AuthorName { get; set; }

        public string BlogTitle { get; set; }

        public string BlogContent { get; set; }

        public int DeleteFlag { get; set; }
    }
}
