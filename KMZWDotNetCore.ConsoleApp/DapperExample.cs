using Dapper;
using KMZWDotNetCore.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch5",
            UserID = "sa",
            Password = "sasa@123",
            TrustServerCertificate = true,
        };

        #region ReadMethod
        public void Read()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                string query = "select * from Tbl_Blog where DeleteFlag=0";

                var result = db.Query<BlogDataModel>(query).ToList();

                foreach (var item in result)
                {
                    Console.WriteLine("BlogId : " + item.BlogId);
                    Console.WriteLine("BlogAuthor : " + item.BlogAuthor);
                    Console.WriteLine("BlogTitle : " + item.BlogTitle);
                    Console.WriteLine("BlogContent : " + item.BlogContent);
                    Console.WriteLine("DeleteFlag : " + item.DeleteFlag);
                    Console.WriteLine("----------------------------------------------------");
                }
            }
        }
        #endregion

        
    }
}
