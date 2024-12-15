using KMZWDotNetCore.Domain.Features.Blog;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KMZWDotNetCore.MinimalApi.EndPoint.Blog
{
    public static class BlogDomainEndPoint
    {

        public static void MapBlogDomainEndPoint(this IEndpointRouteBuilder app)
        {

            app.MapGet("/blogs", ([FromServices] IBlogServices service) =>
            {

                var model = service.GetBlogs();

                return Results.Ok(model);
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapGet("/blogs/{id}", ([FromServices] IBlogServices service, int id) =>
            {

                var model = service.GetBlog(id);
                return Results.Ok(model);
            })
            .WithName("GetBlog")
            .WithOpenApi();

            app.MapPost("/blogs", ([FromServices] IBlogServices service, TblBlog blog) =>
            {

                var model = service.CreateBlog(blog);
                return Results.Ok(model);
            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", ([FromServices] IBlogServices service, int id, TblBlog blog) =>
            {

                var model = service.UpdateBlog(id, blog);

                if (model is null)
                {
                    return Results.BadRequest("Failed to Update!!");
                }

                return Results.Ok(model);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            app.MapDelete("/blogs/{id}", ([FromServices] IBlogServices service, int id) =>
            {

                var model = service.DeleteBlog(id);
                if (model == false)
                {
                    return Results.BadRequest("Failed to delete!!");
                }

                return Results.Ok("Successfully deleted.");
            });
        }
    }
}
