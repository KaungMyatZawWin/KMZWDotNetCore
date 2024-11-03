using KMZWDotNetCore.ServiceRestApi.ViewModel;
using KMZWDotNetCore.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using static KMZWDotNetCore.Shared.AdoDotNetService;

namespace KMZWDotNetCore.ServiceRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=true";

        private readonly AdoDotNetService _adoDotNetService;

        public BlogAdoDotNetController()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        [HttpGet]
        public IActionResult GetBlog()
        {
            List<BlogViewModel> lst = new List<BlogViewModel>();

            string query = @"select * from Tbl_Blog"; ;

            var dt = _adoDotNetService.Query(query);

            foreach (DataRow dr in dt.Rows)
            {
                lst.Add(new BlogViewModel
                {
                    BlogId = Convert.ToInt32(dr["BlogId"]),
                    AuthorName = Convert.ToString(dr["BlogAuthor"])!,
                    Title = Convert.ToString(dr["BlogTitle"])!,
                    Content = Convert.ToString(dr["BlogContent"])!,
                    IsDelete = Convert.ToInt32(dr["DeleteFlag"])

                });
            }

            return Ok(lst);

        }


        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
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

            int model = _adoDotNetService.Execute(query,
               new SqlParameterModel("@BlogAuthor", blog.AuthorName),
               new SqlParameterModel("@BlogTitle", blog.Title),
               new SqlParameterModel("@BlogContent", blog.Content),
               new SqlParameterModel("@DeleteFlag", blog.IsDelete)
               );


            var result = model == 1 ? "Successfully created new blog" : "Failed to create!!";
            return Ok(new { Message = result });
        }

        [HttpPut]
        public IActionResult UpdateBlog(BlogViewModel blog)
        {

            string query = "select * from [dbo].[Tbl_Blog] where BlogId= @BlogId";

            var foundBlog = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", blog.BlogId));

            if (foundBlog is null)
            {
                return BadRequest("Blog not found!!");
            }

            string queryString = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogAuthor] = @BlogAuthor
                  ,[BlogTitle] = @BlogTitle
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = @DeleteFlag
             WHERE BlogId = @BlogId";


            var model = _adoDotNetService.Execute(queryString,
                new SqlParameterModel("@BlogId", blog.BlogId),
                new SqlParameterModel("@BlogAuthor", blog.AuthorName),
                new SqlParameterModel("@BlogTitle", blog.Title),
                new SqlParameterModel("@BlogContent", blog.Content),
                new SqlParameterModel("@DeleteFlag", blog.IsDelete)

                );

            var result = model == 1 ? "Successfully Updated. " : "Failed to update!!";

            return Ok(new { Message = result });
        }

        [HttpPatch("{id}")]
        public IActionResult EditBlog(int id, BlogViewModel blog)
        {

            string condition = "";
            if (!string.IsNullOrEmpty(blog.AuthorName))
            {
                condition += "[BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Title))
            {
                condition += "[BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                condition += "[BlogContent] = @BlogContent, ";
            }

            if (condition.Length < 0)
            {
                return BadRequest("Invalid Request!!");
            }

            condition = condition.Substring(0, condition.Length - 2);

            string queryString = $@"UPDATE [dbo].[Tbl_Blog]
               SET {condition}
             WHERE BlogId = @BlogId";

            var model = _adoDotNetService.Execute(queryString,
                new SqlParameterModel("@BlogId", id),
                new SqlParameterModel("@BlogAuthor", blog.AuthorName),
                new SqlParameterModel("@BlogTitle", blog.Title),
                new SqlParameterModel("@BlogContent", blog.Content),
                new SqlParameterModel("@DeleteFalg", blog.IsDelete)

                );

            var result = model == 1 ? "Siccessfully edited ." : "Failed to Edit";

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            string queryString = @"DELETE FROM [dbo].[Tbl_Blog]
                                        WHERE BlogId = @BlogId";

            var model = _adoDotNetService.Execute(queryString,
                new SqlParameterModel("@BlogId", id)
                );
            var result = model > 0 ? "Successfully deleted!" : "Failed to delete!!";
            
            return Ok(result);
        }

    }
}
