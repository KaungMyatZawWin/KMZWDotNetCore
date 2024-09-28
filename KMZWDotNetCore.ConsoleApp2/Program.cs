// See https://aka.ms/new-console-template for more information
using KMZWDotNetCore.Database.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var model = db.TblBlogs.Where(blogs => blogs.DeleteFlag == 0).ToList();
foreach (var item in model)
{
    Console.WriteLine(item.BlogId);
    Console.WriteLine(item.BlogAuthor);
    Console.WriteLine(item.BlogTitle);
    Console.WriteLine(item.BlogContent);
    Console.WriteLine("------------------------");
}