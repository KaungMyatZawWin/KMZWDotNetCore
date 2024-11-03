using KMZWDotNetCore.ServiceRestApi.DataModel;
using KMZWDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMZWDotNetCore.ServiceRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=true";

        private readonly DapperService _dapperService;

        public BlogDapperController()
        {
            _dapperService = new DapperService(_connectionString);
        }


        private bool BlogExists(int id)
        {
            string query = @"SELECT * FROM tbl_blog WHERE DeleteFlag = 0 AND BlogId = @BlogId";
            var blog = _dapperService.Query<BlogDataModel>(query, new { BlogId = id });
            return blog != null;
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "select * from tbl_blog where DeleteFlag = 0;";
            var lst = _dapperService.Query<BlogDataModel>(query);
            return Ok(lst);
        }


        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            if (!BlogExists(id))
            {
                return NotFound("No data found.");
            }

            string query = @"select * from tbl_blog where DeleteFlag = 0 and BlogId = @BlogId";

            var item = _dapperService.Query<BlogDataModel>(query, new BlogDataModel
            {
                BlogId = id
            });

            if (item is null)
            {
                return NotFound();
            }
            return Ok(item);



        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                       ([BlogAuthor]
                       ,[BlogTitle]
                       ,[BlogContent]
                       ,[DeleteFlag])
                 VALUES
                       (@BlogAuthor
                       ,@BlogTitle
                       ,@BlogContent
                       ,@DeleteFlag)";

            int model = _dapperService.Execute<BlogDataModel>(query, new { BlogAuthor = blog.AuthorName, BlogTitle = blog.BlogTitle, blogContent = blog.BlogContent, DeleteFlag = blog.DeleteFlag });
            string result = model == 1 ? "Successfully Created New Blog. " : "Failed to create!";
            return Ok(result);

        }


        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }

            string query = @"INSERT INTO [dbo].[Tbl_Blog]
                       ([BlogAuthor]
                       ,[BlogTitle]
                       ,[BlogContent]
                       ,[DeleteFlag])
                 VALUES
                       (@BlogAuthor
                       ,@BlogTitle
                       ,@BlogContent
                       ,@DeleteFlag)";

            int model = _dapperService.Execute<BlogDataModel>(query, new { BlogAuthor = blog.AuthorName, BlogTitle = blog.BlogTitle, BlogContent = blog.BlogContent, DeleteFlag = blog.DeleteFlag });
            string result = model == 1 ? "Successfully Created New Blog. " : "Failed to create!";
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {

            if (!BlogExists(id))
            {
                return NotFound("Blog not found.");
            }

            string query = $@"UPDATE [dbo].[Tbl_Blog]
                            SET [DeleteFlag] = 1
                            WHERE BlogId = @BlogId";
            int result = _dapperService.Execute<BlogDataModel>(query, new BlogDataModel { BlogId = id });

            return Ok(result == 0 ? "Deleting Blog Failed !" : "Successfully Deleted Blog");

        }

    }
}
