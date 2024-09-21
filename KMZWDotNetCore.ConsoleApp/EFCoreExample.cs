using KMZWDotNetCore.ConsoleApp.Model;
using Microsoft.EntityFrameworkCore;
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

        #region EditMethod
        public void Edit()
        {
            Console.Write("Enter BlogId to Find Blog to Edit:");
            string intStr = Console.ReadLine()!;
            int blogId = int.Parse(intStr);

            EFCoreDataModel eFCoreDataModel = new EFCoreDataModel
            { BlogId = blogId };

            AppDbContext db = new AppDbContext();
            var model = db.Blogs.FirstOrDefault(x => x.BlogId == eFCoreDataModel.BlogId);

            if (model is null)
            {
                Console.WriteLine("Blogs not found!");
                return;
            }

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("BlogId : " + model.BlogId);
            Console.WriteLine("BlogAuthor : " + model.BlogAuthor);
            Console.WriteLine("BlogTitle : " + model.BlogTitle);
            Console.WriteLine("BlogContent : " + model.BlogContent);
            Console.WriteLine("DeleteFlag : " + model.DeleteFlag);
            Console.WriteLine("---------------------------------------------");
        }
        #endregion

        #region UpdateMethod
        public void Update()
        {
            Console.Write("Enter BlogId to Find Blog to Update :");
            string intStr = Console.ReadLine()!;
            int blogId = int.Parse(intStr);

            EFCoreDataModel eFCoreDataModel = new EFCoreDataModel
            { BlogId = blogId };

            AppDbContext db = new AppDbContext();
            var model = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);

            if (model is null)
            {
                Console.WriteLine("Blogs not found!");
                return;
            }

            Console.Write("Enter Author Name: ");
            string authorName = Console.ReadLine()!;
            Console.Write("Enter Blog Title: ");
            string blogTitle = Console.ReadLine()!;
            Console.Write("Enter Blog Content: ");
            string blogContent = Console.ReadLine()!;
            Console.Write("Are you Published or Draft , Type only 0 or 1 (0 is publish) :");
            string deleteStr = Console.ReadLine()!;
            int isDelete = int.Parse(deleteStr);

            if (!string.IsNullOrEmpty(authorName))
            {
                model.BlogAuthor = authorName;
            }

            if (!string.IsNullOrEmpty(blogTitle))
            {
                model.BlogTitle = blogTitle;
            }

            if (!string.IsNullOrEmpty(blogContent))
            {
                model.BlogContent = blogContent;
            }

            if (!string.IsNullOrEmpty(deleteStr))
            {
                model.DeleteFlag = isDelete;
            }
            db.Entry(model).State = EntityState.Modified;
            int resp = db.SaveChanges();

            string result = resp == 1 ? "Successfully updated." : "Failed to update!";
            Console.WriteLine(result);
        }
        #endregion


    }
}
