// See https://aka.ms/new-console-template for more information
using KMZWDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

//SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
//sqlConnectionStringBuilder.DataSource = ".";
//sqlConnectionStringBuilder.InitialCatalog = "DotNetTrainingBatch5";
//sqlConnectionStringBuilder.UserID = "sa";
//sqlConnectionStringBuilder.Password = "sasa@123";

//SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

//Console.WriteLine("Connection is opening....");
//connection.Open();
//Console.WriteLine("Connection opened");

//string queryString = "select * from Tbl_Blog where DeleteFlag = 0 ";
//SqlCommand cmd = new SqlCommand(queryString,connection);


//SqlDataReader reader = cmd.ExecuteReader();
//while (reader.Read())
//{
//    Console.WriteLine("BlogId: " + reader["BlogId"]);
//    Console.WriteLine("Blog Aurthor: " + reader["BlogAurthor"]);
//    Console.WriteLine("Blog Title: " + reader["BlogTitle"]);
//    Console.WriteLine("Blog Content: " + reader["BlogContent"]);
//}




//Console.WriteLine("Connection is closing....");
//connection.Close();
//Console.WriteLine("Connection was closed!");

//AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();
//adoDotNetExample.Update();
//adoDotNetExample.Create();
//adoDotNetExample.Delete();
//adoDotNetExample.Edit();

//DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create();
//dapperExample.Update();
//dapperExample.Delete();
//dapperExample.Edit();

EFCoreExample efcoreExample = new EFCoreExample();
//efcoreExample.Read();
efcoreExample.Create();




