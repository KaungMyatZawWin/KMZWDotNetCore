
using Dapper;
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

                var model = db.Query(query).ToList();

                return Ok(model);
            }

        }
        #endregion
    }
}
