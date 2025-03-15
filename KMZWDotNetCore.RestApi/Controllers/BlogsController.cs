using KMZWDotNetCore.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KMZWDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var model = _db.TblBlogs.AsNoTracking().Where(blog => blog.DeleteFlag == 0).ToList();

            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlogs(int id)
        {
            var model = _db.TblBlogs.AsNoTracking().FirstOrDefault(blog => blog.BlogId == id);

            if (model is null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlogs(TblBlog blog)
        {
            TblBlog tblBLog = new TblBlog()
            {
                BlogId = blog.BlogId,
                BlogAuthor = blog.BlogAuthor,
                BlogTitle = blog.BlogTitle,
                BlogContent = blog.BlogContent,
                DeleteFlag = 0
            };

            _db.TblBlogs.Add(tblBLog);
            var result = _db.SaveChanges();
            if(result <= 0)
            {
                return BadRequest();
            }

            return Ok(blog);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlogs(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, TblBlog blog)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                item.BlogContent = blog.BlogContent;
            }

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound();
            }

            item.DeleteFlag = 1;
            _db.Entry(item).State = EntityState.Modified;

            _db.SaveChanges();

            return Ok();
        }
    }
}
