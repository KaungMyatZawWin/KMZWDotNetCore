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

        #region CreateMethod
        public void Create()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
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

                int model = db.Execute(query, new { BlogAuthor = authorName, BlogTitle = blogTitle, blogContent = blogContent, DeleteFlag = isDelete });
                string result = model == 1 ? "Successfully Created New Blog. " : "Failed to create!";

                Console.WriteLine(result);


            }

        }
        #endregion

        #region UpdateMethod
        public void Update()
        {

            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                Console.Write("Enter BlogId to Find Blog to Update:");
                string intStr = Console.ReadLine()!;
                int blogId = int.Parse(intStr);

                string findQuery = "select * from Tbl_Blog where @BlogId = blogId";

                var foundBlog = db.QueryFirstOrDefault<BlogDataModel>(findQuery, new { BlogId = blogId });
                if (foundBlog is null)
                {
                    Console.WriteLine("Can't find blog to update!");
                    return;
                }

                Console.Write("Enter Author Name: ");
                string authorName = Console.ReadLine()!;
                Console.Write("Enter Blog Title: ");
                string blogTitle = Console.ReadLine()!;
                Console.Write("Enter Blog Content: ");
                string blogContent = Console.ReadLine()!;
                Console.WriteLine("Are you Published or Draft , Type only 1 or 0 :");
                string deleteStr = Console.ReadLine()!;
                int isDelete = int.Parse(deleteStr);

                string query = @"UPDATE [dbo].[Tbl_Blog]
                       SET [BlogAuthor] = @BlogAuthor
                          ,[BlogTitle] = @BlogTitle
                          ,[BlogContent] = @BlogContent
                          ,[DeleteFlag] = @DeleteFlag
                     WHERE BlogId = @BlogId";

                int model = db.Execute(query, new { BlogId = blogId, BlogAuthor = authorName, BlogTitle = blogTitle, BlogContent = blogContent, DeleteFlag = isDelete });
                string result = model == 1 ? "Successfully Update blog. " : "Failed to Update!";
                Console.WriteLine(result);
            }

        }
        #endregion

        #region DeleteMethod
        public void Delete()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                Console.Write("Enter BlogId to Find Blog to Delete:");
                string intStr = Console.ReadLine()!;
                int blogId = int.Parse(intStr);

                string findQuery = "select * from Tbl_Blog where @BlogId = blogId";

                var foundBlog = db.QueryFirstOrDefault<BlogDataModel>(findQuery, new { BlogId = blogId });
                if (foundBlog is null)
                {
                    Console.WriteLine("Can't find blog to Delete!");
                    return;
                }

                string query = @"DELETE FROM [dbo].[Tbl_Blog]
                    WHERE BlogId = @BlogId";

                int model = db.Execute(query, new { BlogId = blogId });
                string result = model == 1 ? "Successfully Delete Blog." : "Failed to Delete!";
                Console.WriteLine(result);
            }
        }
        #endregion

        #region EditMethod
        public void Edit()
        {
            using (IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString))
            {
                Console.Write("Enter BlogId to Find Blog to Edit:");
                string intStr = Console.ReadLine()!;
                int blogId = int.Parse(intStr);

                string findQuery = "select * from Tbl_Blog where @BlogId = blogId";

                var foundBlog = db.QueryFirstOrDefault<BlogDataModel>(findQuery, new { BlogId = blogId });
                if (foundBlog is null)
                {
                    Console.WriteLine("Can't find blog to Edit!");
                    return;
                }

                string query = "select * from Tbl_Blog where BlogId = @BlogId";

                var model = db.QueryFirstOrDefault<BlogDataModel>(query, new { BlogId = blogId });

                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("BlogId : " + model.BlogId);
                Console.WriteLine("BlogAuthor : " + model.BlogAuthor);
                Console.WriteLine("BlogTitle : " + model.BlogTitle);
                Console.WriteLine("BlogContent : " + model.BlogContent);
                Console.WriteLine("DeleteFlag : " + model.DeleteFlag);
                Console.WriteLine("---------------------------------------------");

            };
        }
        #endregion
    }
}
