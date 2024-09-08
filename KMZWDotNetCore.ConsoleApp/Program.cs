// See https://aka.ms/new-console-template for more information
using System.Data;
using System.Data.SqlClient;

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "DotNetTrainingBatch5";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

Console.WriteLine("Connection is opening....");
connection.Open();
Console.WriteLine("Connection opened");

string queryString = "select * from Tbl_Blog where DeleteFlag = 0 ";
SqlCommand cmd = new SqlCommand(queryString,connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

Console.WriteLine("Connection is closing....");
connection.Close();
Console.WriteLine("Connection was closed!");

foreach (DataRow dr in dt.Rows)
{
   Console.WriteLine("Blog Id: " + dr["BlogId"]);
   Console.WriteLine("Blog Aruthor: " + dr["BlogAurthor"]);
    Console.WriteLine("Blog Title: " + dr["BlogTitle"]);
    Console.WriteLine("Blog Content: " + dr["BlogContent"]);
}