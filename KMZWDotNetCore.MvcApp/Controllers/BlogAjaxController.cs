using KMZWDotNetCore.Database.Models;
using KMZWDotNetCore.Domain.Features.Blog;
using KMZWDotNetCore.MvcApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMZWDotNetCore.MvcApp.Controllers
{
    public class BlogAjaxController : Controller
    {
        private readonly IBlogServices _blogServices;

        public BlogAjaxController(IBlogServices blogServices)
        {
            _blogServices = blogServices;
        }

        [ActionName("Index")]
        public IActionResult GetBlogList()
        {
            return View("BlogList");
        }


        [ActionName("List")]
        public IActionResult GetBlogListAjax()
        {
            var lst = _blogServices.GetBlogs();
            return Json(lst);
        }

        [ActionName("Create")]
        public IActionResult CreateBlog()
        {
            return View("BlogCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult SaveBlog(BlogRequestModel requestModel)
        {
            MessageModel messageModel;

            try
            {
                TblBlog tblBlog = new TblBlog()
                {
                    BlogAuthor = requestModel.Author,
                    BlogTitle = requestModel.Title,
                    BlogContent = requestModel.Content,
                };

                _blogServices.CreateBlog(tblBlog);

                messageModel = new MessageModel(true, "Blog Create Successfully");
            }
            catch (Exception ex)
            {
                messageModel = new MessageModel(false, ex.ToString());
            }

            return Json(messageModel);

        }


        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            BlogRequestModel model;
            var lst = _blogServices.GetBlog(id);


            model = new BlogRequestModel()
            {
                Id = lst.BlogId,
                Author = lst.BlogAuthor,
                Title = lst.BlogTitle,
                Content = lst.BlogContent
            };

            return View("BlogEdit", model);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel requestModel)
        {
            MessageModel model;
            try
            {

                TblBlog tblBlog = new TblBlog()
                {
                    BlogAuthor = requestModel.Author,
                    BlogTitle = requestModel.Title,
                    BlogContent = requestModel.Content,
                };

                _blogServices.UpdateBlog(id, tblBlog);

                model = new MessageModel(true, "Blog update successfully");

            }
            catch (Exception ex)
            {
                model = new MessageModel(false, ex.ToString());
            }

            return Json(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            MessageModel model;
            try
            {
                _blogServices.DeleteBlog(id);
                model = new MessageModel(true, "Successfully deleted.");
            }
            catch (Exception ex)
            {
                model = new MessageModel(false, ex.ToString());
            }

            return Json(model);
        }

    }
}

public class MessageModel
{

    public MessageModel(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}