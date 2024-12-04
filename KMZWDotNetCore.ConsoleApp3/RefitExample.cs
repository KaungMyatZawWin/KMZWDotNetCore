using Refit;

namespace KMZWDotNetCore.ConsoleApp3
{
    public class RefitExample
    {
        public async Task Run()
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7291");
            var model = await blogApi.GetBlogs();
            foreach (var blog in model)
            {
                Console.WriteLine(blog.BlogTitle);
            }

            var item = await blogApi.EditBlog(1);
            Console.WriteLine(item.BlogAuthor);

            var item2 = await blogApi.CreateBlog(new BlogsDataModel
            {
                BlogAuthor = "Refit",
                BlogContent = "Refit",
                BlogTitle = "Title",
                DeleteFlag = 0
            });

            var item3 = await blogApi.UpdateBlog(22 , new BlogsDataModel
            {
                BlogTitle = "Refit update",
                BlogAuthor = "Refit update",
                BlogContent = "Refit update",
                DeleteFlag = 1
            });

            var item4 = await blogApi.DeleteBlog(23);
        }
    }
}
