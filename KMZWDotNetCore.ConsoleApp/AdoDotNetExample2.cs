using KMZWDotNetCore.Shared;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KMZWDotNetCore.Shared.AdoDotNetService;

namespace KMZWDotNetCore.ConsoleApp
{
    public class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=true";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = "select * from Tbl_Blog";

            var dt = _adoDotNetService.Query(query);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }

        }

        public void Edit()
        {
            Console.Write("Enter Blog Id to find: ");
            string id = Console.ReadLine()!;

            string query = "select * from [dbo].[Tbl_Blog] where BlogId= @BlogId";

            var dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", id));
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }

        }

        public void Create()
        {

            Console.Write("Enter BlogAuthor: ");
            string blogAuthor = Console.ReadLine()!;

            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine()!;

            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine()!;

            Console.Write("Want to delete? Please type 0 or 1 : ");
            string isDelete = Console.ReadLine()!;



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



            int result = _adoDotNetService.Execute(query,
                new SqlParameterModel("@BlogAuthor", blogAuthor),
                new SqlParameterModel("@BlogTitle", blogTitle),
                new SqlParameterModel("@BlogContent", blogContent),
                new SqlParameterModel("@DeleteFlag", isDelete)
                );

            var message = result > 0 ? "Successfully Created Blog." : "Failed to Create!";
            Console.WriteLine(message);
        }
    }
}
