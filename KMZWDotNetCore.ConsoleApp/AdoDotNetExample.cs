using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "DotNetTrainingBatch5",
            UserID = "sa",
            Password = "sasa@123"
        };

        #region CreateMethod
        public void Create()
        {

            Console.Write("Enter BlogAuthor: ");
            string blogAuthor = Console.ReadLine();

            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine();

            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine();

            Console.Write("Want to delete? Please type 0 or 1 : ");
            string isDelete = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is open.");

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
            cmd.Parameters.AddWithValue("@BlogAuthor", blogAuthor);
            cmd.Parameters.AddWithValue("@BlogTitle", blogTitle);
            cmd.Parameters.AddWithValue("@BlogContent", blogContent);
            cmd.Parameters.AddWithValue("@DeleteFlag", isDelete);

            var result = cmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection was closed!");

            var message = result > 0 ? "Successfully Created Blog." : "Failed to Create!";
            Console.WriteLine(message);
        }
        #endregion

        #region ReadMethod
        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is open.");

            string qeuryString = "select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(qeuryString, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("Blog Id :" + reader["BlogId"]);
                Console.WriteLine("Blog Author :" + reader["BlogAuthor"]);
                Console.WriteLine("Blog Title :" + reader["BlogTitle"]);
                Console.WriteLine("Blog Content :" + reader["BlogContent"]);
                Console.WriteLine("--------------------------------------------------");
            }

            connection.Close();
            Console.WriteLine("Connection was closed!");
        }
        #endregion

        #region UpdateMethod
        public void Update()
        {
            Console.Write("Enter BlogId: ");
            string blogIdStr = Console.ReadLine()!;
            var blogId = int.Parse(blogIdStr);

            Console.Write("Enter BlogAuthor: ");
            string blogAuthor = Console.ReadLine();

            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine();

            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine();

            Console.Write("Want to delete? Please type 0 or 1 : ");
            string isDelete = Console.ReadLine();

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is open.");

            string queryString = @"UPDATE [dbo].[Tbl_Blog]
               SET [BlogAuthor] = @BlogAuthor
                  ,[BlogTitle] = @BlogTitle
                  ,[BlogContent] = @BlogContent
                  ,[DeleteFlag] = @DeleteFlag
             WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(queryString, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogAuthor);
            cmd.Parameters.AddWithValue("@BlogTitle", blogTitle);
            cmd.Parameters.AddWithValue("@BlogContent", blogContent);
            cmd.Parameters.AddWithValue("@DeleteFlag", isDelete);

            int result = cmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection was closed!");

            var message = result == 0 ? "Update Failed!" : "Successfully Updated.";
            Console.WriteLine(message);

        }
        #endregion

        #region DeleteMethod
        public void Delete()
        {
            Console.Write("Enter Blog Id you want to delete :");
            string blogIdStr = Console.ReadLine()!;
            int blogId = int.Parse(blogIdStr);

            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection is Open.");

            string queryString = @"DELETE FROM [dbo].[Tbl_Blog]
                WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(queryString, connection);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            int result = cmd.ExecuteNonQuery();

            connection.Close();
            Console.WriteLine("Connection was Close!");

            string message = result > 0 ? "Successfully Deleted." : "Failed to Delete!";
            Console.WriteLine(message);
        }
        #endregion
    }
}
