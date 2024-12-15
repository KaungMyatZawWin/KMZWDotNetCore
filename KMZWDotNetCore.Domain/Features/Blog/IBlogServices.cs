using KMZWDotNetCore.Database.Models;

namespace KMZWDotNetCore.Domain.Features.Blog
{
    public interface IBlogServices
    {
        TblBlog CreateBlog(TblBlog blog);
        bool DeleteBlog(int id);
        TblBlog GetBlog(int id);
        List<TblBlog> GetBlogs();
        TblBlog UpdateBlog(int id, TblBlog blog);
    }
}