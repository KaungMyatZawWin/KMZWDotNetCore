using KMZWDotNetCore.ConsoleApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        #region ReadMethod
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var result = db.Blogs.Where(x => x.DeleteFlag == 0).ToList();

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
        #endregion

        #region CreateMethod
        public void Create()
        {
            Console.Write("Enter Author Name: ");
            string authorName = Console.ReadLine()!;
            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine()!;
            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine()!;
            Console.Write("Are you Published or Draft , Type only 0 or 1 (0 is publish) :");
            string deleteStr = Console.ReadLine()!;
            int isDelete = int.Parse(deleteStr);

            EFCoreDataModel efCoreBlog = new EFCoreDataModel
            {
                BlogAuthor = authorName,
                BlogTitle = blogTitle,
                BlogContent = blogContent,
                DeleteFlag = isDelete,
            };

            AppDbContext db = new AppDbContext();
            db.Blogs.Add(efCoreBlog);
            int model = db.SaveChanges();

            string result = model == 1 ? "Successfully Created New Blog." : "Failed to create!";
            Console.WriteLine(result);
        }
        #endregion
    }
}
