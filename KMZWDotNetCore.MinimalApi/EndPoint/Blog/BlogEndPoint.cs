using KMZWDotNetCore.Database.Models;
using Microsoft.AspNetCore.Mvc;

namespace KMZWDotNetCore.MinimalApi.EndPoint.Blog
{
    public static class BlogEndPoint
    {
        public static void MapBlogEndPoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
            {
                //AppDbContext db = new AppDbContext();
                var result = db.TblBlogs.AsNoTracking().ToList();
                return Results.Ok(result);
            })
.WithName("GetAllBlogs")
            .WithOpenApi();

            app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
            {
                db.TblBlogs.Add(blog);
                db.SaveChanges();
                return Results.Ok(blog);
            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
            {
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

                if (item is null)
                {
                    return Results.BadRequest("No data found!!");
                }

                item.BlogTitle = blog.BlogTitle;
                item.BlogAuthor = blog.BlogAuthor;
                item.BlogContent = blog.BlogContent;

                db.Entry(item).State = EntityState.Modified;

                db.SaveChanges();
                return Results.Ok(blog);

            })
                .WithName("UpdateBlog").WithOpenApi();

            app.MapPatch("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
            {
                var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (item is null)
                {
                    return Results.BadRequest("No data found!!");
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

                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return Results.Ok(item);

            });

            app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
            {
                var result = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
                if (result is null)
                {
                    return Results.BadRequest("No data found!!");
                }

                db.Entry(result).State = EntityState.Deleted;
                var model = db.SaveChanges();
                return Results.Ok(model == 1 ? "Successfully deleted." : "Failed to delete!!");
            });
        }
    }
}
