using Dapper;
using KMZWDotNetCore.ConsoleApp.Model;
using KMZWDotNetCore.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp
{
    public class DapperExample2
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=true";

        private readonly DapperService _dapperService;

        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }

        public void Read()
        {

            string query = "select * from Tbl_Blog where DeleteFlag=0";

            var result = _dapperService.Query<BlogDataModel>(query);

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

        public void Edit()
        {

            Console.Write("Enter BlogId to Find Blog to Edit:");
            string intStr = Console.ReadLine()!;
            int blogId = int.Parse(intStr);

            string query = "select * from Tbl_Blog where BlogId = @BlogId";

            var foundBlog = _dapperService.QueryFirstOrDefault<BlogDataModel>(query, new { BlogId = blogId });
            if (foundBlog is null)
            {
                Console.WriteLine("Can't find blog to Edit!");
                return;
            }

            //string query = "select * from Tbl_Blog where BlogId = @BlogId";

            var model = _dapperService.QueryFirstOrDefault<BlogDataModel>(query, new { BlogId = blogId });

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("BlogId : " + model.BlogId);
            Console.WriteLine("BlogAuthor : " + model.BlogAuthor);
            Console.WriteLine("BlogTitle : " + model.BlogTitle);
            Console.WriteLine("BlogContent : " + model.BlogContent);
            Console.WriteLine("DeleteFlag : " + model.DeleteFlag);
            Console.WriteLine("---------------------------------------------");

        }

        public void Create()
        {
            
                Console.Write("Enter Author Name: ");
                string authorName = Console.ReadLine()!;
                Console.Write("Enter Blog Title: ");
                string blogTitle = Console.ReadLine()!;
                Console.Write("Enter Blog Content: ");
                string blogContent = Console.ReadLine()!;
                Console.WriteLine("Are you Published or Draft , Type only 1 or 0 :");
                string deleteStr = Console.ReadLine()!;
                int isDelete = int.Parse(deleteStr);

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

                int model =_dapperService.Execute<BlogDataModel>(query, new { BlogAuthor = authorName, BlogTitle = blogTitle, blogContent = blogContent, DeleteFlag = isDelete });
                string result = model == 1 ? "Successfully Created New Blog. " : "Failed to create!";

                Console.WriteLine(result);


            

        }

        public void Delete()
        {
            
                Console.Write("Enter BlogId to Find Blog to Delete:");
                string intStr = Console.ReadLine()!;
                int blogId = int.Parse(intStr);

                string findQuery = "select * from Tbl_Blog where @BlogId = blogId";

                var foundBlog = _dapperService.QueryFirstOrDefault<BlogDataModel>(findQuery, new { BlogId = blogId });
                if (foundBlog is null)
                {
                    Console.WriteLine("Can't find blog to Delete!");
                    return;
                }

                string query = @"DELETE FROM [dbo].[Tbl_Blog]
                    WHERE BlogId = @BlogId";

                int model = _dapperService.Execute<BlogDataModel>(query, new { BlogId = blogId });
                string result = model == 1 ? "Successfully Delete Blog." : "Failed to Delete!";
                Console.WriteLine(result);
            
        }
    }
}
