using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp3
{
    public interface IBlogApi
    {

        [Get("/api/blogs")]
        Task<List<BlogsDataModel>> GetBlogs();

        [Get("/api/blogs/{id}")]
        Task<BlogsDataModel> EditBlog(int id);

        [Post("/api/blogs")]
        Task<BlogsDataModel> CreateBlog(BlogsDataModel blog);

        [Put("/api/blogs/{id}")]
        Task<BlogsDataModel> UpdateBlog(int id , BlogsDataModel request);

        [Put("/api/blogs/{id}")]
        Task<BlogsDataModel> DeleteBlog(int id);

    }

    public class BlogsDataModel
    {
        public int BlogId { get; set; }
        public string BlogAuthor { get; set; }
        public string BlogTitle { get; set; }
        public string BlogContent { get; set; }
        public int DeleteFlag { get; set; }
    }

}
