// See https://aka.ms/new-console-template for more information
using KMZWDotNetCore.Database.Models;
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var model = db.TblBlogs.Where(blogs => blogs.DeleteFlag == 0).ToList();
//foreach (var item in model)
//{
//    Console.WriteLine(item.BlogId);
//    Console.WriteLine(item.BlogAuthor);
//    Console.WriteLine(item.BlogTitle);
//    Console.WriteLine(item.BlogContent);
//    Console.WriteLine("------------------------");
//}

var blog = new BlogModal
{
    Id = 1,
    Title = "Testing",
    Author = "Testing",
    Description = "Testing"
};

//string blogJsonStr = JsonConvert.SerializeObject(blog, Formatting.Indented);
string jsonString = blog.ToJson();
Console.WriteLine(jsonString);
Console.ReadLine();

public class BlogModal
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Author { get; set; }

    public string Description { get; set; }

}

public static class DevCode
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }

}

