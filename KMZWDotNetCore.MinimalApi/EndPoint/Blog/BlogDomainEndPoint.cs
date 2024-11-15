using KMZWDotNetCore.Domain.Features.Blog;

namespace KMZWDotNetCore.MinimalApi.EndPoint.Blog
{
    public static class BlogDomainEndPoint
    {

        public static void MapBlogDomainEndPoint(this IEndpointRouteBuilder app)
        {

            app.MapGet("/blogs", () =>
            {
                BlogServices service = new BlogServices();

                var model = service.GetBlogs();

                return Results.Ok(model);
            })
            .WithName("GetBlogs")
            .WithOpenApi();

            app.MapGet("/blogs/{id}", (int id) =>
            {
                BlogServices services = new BlogServices();

                var model = services.GetBlog(id);
                return Results.Ok(model);
            })
            .WithName("GetBlog")
            .WithOpenApi();

            app.MapPost("/blogs", (TblBlog blog) =>
            {
                BlogServices service = new BlogServices();

                var model = service.CreateBlog(blog);
                return Results.Ok(model);
            })
            .WithName("CreateBlog")
            .WithOpenApi();

            app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
            {
                BlogServices services = new BlogServices();

                var model = services.UpdateBlog(id, blog);

                if (model is null)
                {
                    return Results.BadRequest("Failed to Update!!");
                }

                return Results.Ok(model);
            })
            .WithName("UpdateBlog")
            .WithOpenApi();

            app.MapDelete("/blogs/{id}", (int id) =>
            {
                BlogServices services = new BlogServices();

                var model = services.DeleteBlog(id);
                if (model == false)
                {
                    return Results.BadRequest("Failed to delete!!");
                }

                return Results.Ok("Successfully deleted.");
            });
        }
    }
}
