
using Dapper;
using KMZWDotNetCore.RestApi.DataModel;
using KMZWDotNetCore.RestApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KMZWDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch5",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };

        #region ReadMethod
        [HttpGet]
        public IActionResult GetBlog()
        {
            using (IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString))
            {
                string query = "select * from Tbl_Blog where DeleteFlag=0";

                var model = db.Query<BlogsDataModel>(query).ToList();

                return Ok(model);
            }

        }
        #endregion

        #region CreateMethod
        [HttpPost]
        public IActionResult CreatedBlog(BlogsViewModel blog)
        {
            using (IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString))
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

                int model = db.Execute(query, new { BlogAuthor = blog.Author, BlogTitle = blog.Title, BlogContent = blog.Content, DeleteFlag = blog.DeleteFlag });
                var result = model == 1 ? "Successfully Created New Blog. " : "Failed to create!";

                return Ok(result);
            };

        }
        #endregion
    }
}
