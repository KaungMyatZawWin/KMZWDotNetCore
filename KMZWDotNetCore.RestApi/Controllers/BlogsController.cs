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
        private readonly AppDbContext _db = new AppDbContext();

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
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();

            return Ok(blog);
        }

        [HttpPut]
        public IActionResult UpdateBlogs()
        {
            return Ok();
        }

        [HttpPatch]
        public IActionResult PatchBlogs()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBlogs()
        {
            return Ok();
        }
    }
}
