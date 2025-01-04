using KMZWDotNetCore.Database.Models;
using KMZWDotNetCore.Domain.Features.Blog;
using KMZWDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMZWDotNetCore.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        public IActionResult Index()
        {
            var lst = _blogServices.GetBlogs();

            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }

        [ActionName("Save")]
        [HttpPost]
        public IActionResult BlogSave(BlogRequestModel requestModel)
        {
            try
            {
                TblBlog tblBlog = new TblBlog()
                {
                    BlogAuthor = requestModel.Author,
                    BlogTitle = requestModel.Title,
                    BlogContent = requestModel.Content,
                };

                _blogServices.CreateBlog(tblBlog);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Successfully Created New Blog.";


            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.Message.ToString();

            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            try
            {
                _blogServices.DeleteBlog(id);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Successfully deleted!";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.Message.ToString();
            }

            return RedirectToAction("Index");

        }

        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var lst = _blogServices.GetBlog(id);

            BlogRequestModel requestModel = new BlogRequestModel() { 
                Id = lst.BlogId,
                Author  = lst.BlogAuthor,
                Title = lst.BlogTitle,
                Content = lst.BlogContent
            };

            return View("BlogEdit", requestModel);

        }

        [ActionName("Update")]
        public IActionResult BlogUpdate(int id , BlogRequestModel requestModel)
        {
            try
            {
                TblBlog tblBlog = new TblBlog() { 
                    BlogAuthor = requestModel.Author,
                    BlogTitle = requestModel.Title,
                    BlogContent = requestModel.Content
                };

                _blogServices.UpdateBlog(id, tblBlog);

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Successfully Updated.";

            }catch (Exception ex)
            {
                TempData["IsSuccess"] = false ;
                TempData["Message"] = ex.Message.ToString();
            }

            return RedirectToAction("Index");
        }
    }
}
