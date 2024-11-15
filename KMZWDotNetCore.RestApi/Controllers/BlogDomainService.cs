using KMZWDotNetCore.Database.Models;
using KMZWDotNetCore.Domain.Features.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KMZWDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDomainService : ControllerBase
    {
        private readonly BlogServices _blogServices;

        public BlogDomainService()
        {
            _blogServices = new BlogServices();
        }


        [HttpGet]
        public IActionResult GetBlogs()
        {
            var model = _blogServices.GetBlogs();
            return Ok(model);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var model = _blogServices.GetBlog(id);

            if (model is null)
            {
                return BadRequest("No data found!!");
            }

            return Ok(model);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var model = _blogServices.CreateBlog(blog);
            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var model = _blogServices.UpdateBlog(id, blog);

            if (model is null)
            {
                return BadRequest("Failed to update!!");
            }

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var model = _blogServices.DeleteBlog(id);
            if(model == false)
            {
                return BadRequest("Failed to delete!!");
            }

            return Ok("Successfully deleted.");
        }
    }
}
