using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KMZWDotNetCore.ConsoleApp3
{
    public class HttpClientExample
    {
        private readonly HttpClient _client;
        private readonly string _postEndPoint = "https://jsonplaceholder.typicode.com/posts";

        public HttpClientExample()
        {
            _client = new HttpClient();
        }

        public async Task ReadAsync()
        {
            var response = await _client.GetAsync(_postEndPoint);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        }

        public async Task EditAsync(int id)
        {
            var response = await _client.GetAsync($"{_postEndPoint}/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            }
        }

        public async Task CreateAsync(int userId, string title, string body)
        {
            PostModel postModel = new PostModel()
            {
                userId = userId,
                title = title,
                body = body
            };

            string jsonReq = JsonConvert.SerializeObject(postModel);

            var httpContent = new StringContent(jsonReq, Encoding.UTF8, Application.Json);
            var model = await _client.PostAsync(_postEndPoint, httpContent);

            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(await model.Content.ReadAsStringAsync());
            }

        }

        public async Task UpdateAsync(int id, int userId, string title, string body)
        {
            PostModel postModel = new PostModel()
            {
                userId = userId,
                title = title,
                body = body
            };

            string jsonReq = JsonConvert.SerializeObject(postModel);

            var httpContent = new StringContent(jsonReq, Encoding.UTF8, Application.Json);

            var model = await _client.PutAsync($"{_postEndPoint}/{id}", httpContent);
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(await model.Content.ReadAsStringAsync());
            }
        }

        public async Task DeleteAsync(int id)
        {
            var PostModel = await _client.DeleteAsync($"{_postEndPoint}/{id}");

            if(PostModel.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }

            if(PostModel.IsSuccessStatusCode)
            {
                Console.WriteLine(await PostModel.Content.ReadAsStringAsync());
            }
        }
    }



    public class PostModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

}
