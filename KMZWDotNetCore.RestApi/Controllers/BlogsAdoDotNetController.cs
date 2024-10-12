using KMZWDotNetCore.Database.Models;
using KMZWDotNetCore.RestApi.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace KMZWDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder = new SqlConnectionStringBuilder()
        { 
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch5",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true
        };

        #region ReadMethod

        [HttpGet]
        public IActionResult GetBlog()
        {
            List<BlogsViewModel> lst = new List<BlogsViewModel>();

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string queryString = @"select * from Tbl_Blog";

            SqlCommand cmd = new SqlCommand(queryString, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lst.Add(new BlogsViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlag = Convert.ToInt32(reader["DeleteFlag"])
                });
            }

            connection.Close();

            return Ok(lst);
        }

        #endregion

        #region CreateMethod

        [HttpPost]
        public IActionResult CreateBlog(BlogsViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string queryString = @"INSERT INTO [dbo].[Tbl_Blog]
                   ([BlogAuthor]
                   ,[BlogTitle]
                   ,[BlogContent]
                   ,[DeleteFlag])
             VALUES
                   (@BlogAuthor
                   ,@BlogTitle
                   ,@BlogContent
                   ,@DeleteFlag)";

            SqlCommand cmd = new SqlCommand(queryString, connection);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            cmd.Parameters.AddWithValue("@DeleteFlag", blog.DeleteFlag);

            var model = cmd.ExecuteNonQuery();
            connection.Close();
            var result = model == 1 ? "Successfully created new blog" : "Failed to create!!";
            return Ok(new { Message = result });
        }

        #endregion


        #region UpdateMethod

        [HttpPut]
        public IActionResult UpdateBlog(BlogsViewModel blog)
        {

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            string queryString = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogAuthor] = @BlogAuthor
                  ,[BlogTitle] = @BlogTitle
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = @DeleteFlag
             WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(queryString, connection);
            cmd.Parameters.AddWithValue("@BlogId", blog.Id);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            cmd.Parameters.AddWithValue("@DeleteFlag", blog.DeleteFlag);

            var model = cmd.ExecuteNonQuery();

            connection.Close();

            var result = model == 1 ? "Successfully Updated. " : "Failed to update!!";

            return Ok(new { Message = result });
        }

        #endregion
    }
}
