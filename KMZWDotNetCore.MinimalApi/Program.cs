using KMZWDotNetCore.Database.Models;
using KMZWDotNetCore.MinimalApi.EndPoint.Blog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/blogs", () =>
//{
//    AppDbContext db = new AppDbContext();
//    var result = db.TblBlogs.AsNoTracking().ToList();
//    return Results.Ok(result);
//})
//.WithName("GetAllBlogs")
//.WithOpenApi();

//app.MapPost("/blogs", (TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    db.TblBlogs.Add(blog);
//    db.SaveChanges();
//    return Results.Ok(blog);
//})
//.WithName("CreateBlog")
//.WithOpenApi();

//app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);

//    if (item is null)
//    {
//        return Results.BadRequest("No data found!!");
//    }

//    item.BlogTitle = blog.BlogTitle;
//    item.BlogAuthor = blog.BlogAuthor;
//    item.BlogContent = blog.BlogContent;

//    db.Entry(item).State = EntityState.Modified;

//    db.SaveChanges();
//    return Results.Ok(blog);

//})
//    .WithName("UpdateBlog").WithOpenApi();

//app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item  = db.TblBlogs.AsNoTracking().FirstOrDefault(x=> x.BlogId == id);
//    if (item is null)
//    {
//        return Results.BadRequest("No data found!!");
//    }

//    if(!string.IsNullOrEmpty(blog.BlogTitle))
//    {
//        item.BlogTitle = blog.BlogTitle;
//    }
//    if(!string.IsNullOrEmpty(blog.BlogAuthor))
//    {
//        item.BlogAuthor = blog.BlogAuthor;
//    }
//    if (!string.IsNullOrEmpty(blog.BlogContent))
//    {
//        item.BlogContent= blog.BlogContent;
//    }

//    db.Entry(item).State = EntityState.Modified;
//    db.SaveChanges();
//    return Results.Ok(item);

//});

//app.MapDelete("/blogs/{id}", (int id) =>
//{
//    AppDbContext db = new AppDbContext();
//    var result = db.TblBlogs.AsNoTracking().FirstOrDefault(x=> x.BlogId == id);
//    if (result is null)
//    {
//        return Results.BadRequest("No data found!!");
//    }

//    db.Entry(result).State = EntityState.Deleted;
//    var model = db.SaveChanges();
//    return Results.Ok(model == 1 ? "Successfully deleted." : "Failed to delete!!");
//});

//app.MapBlogEndPoint();
app.MapBlogDomainEndPoint();

app.Run();