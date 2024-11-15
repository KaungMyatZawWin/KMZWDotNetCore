﻿using KMZWDotNetCore.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMZWDotNetCore.Domain.Features.Blog
{
    public class BlogServices
    {
        private readonly AppDbContext _db = new AppDbContext();

        public List<TblBlog> GetBlogs()
        {
            var model = _db.TblBlogs.AsNoTracking().ToList();
            return model;
        }

        public TblBlog GetBlog(int id)
        {
            var result = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

            return result;

        }

        public TblBlog CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return blog;
        }

        public TblBlog UpdateBlog(int id ,TblBlog blog)
        {
            var result = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId == id);
            if(result is null)
            {
                return result;
            }

            result.BlogTitle = blog.BlogTitle;
            result.BlogAuthor = blog.BlogAuthor;
            result.BlogContent = blog.BlogContent;

            _db.Entry(result).State = EntityState.Modified;
            _db.SaveChanges();

            return result;
        }

        public bool DeleteBlog(int id)
        {
            var result = _db.TblBlogs.AsNoTracking().FirstOrDefault(x=>x.BlogId == id);
            if (result is null)
            {
                return false;
            }

            _db.TblBlogs.Entry(result).State = EntityState.Deleted;
            int model = _db.SaveChanges();

            return model == 0 ? false : true;
        }
        
    }
}
