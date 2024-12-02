using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KMZWDotNetCore.ConsoleApp3
{
    public class RestClientExample
    {
        private readonly RestClient _client;
        private readonly string _postEndPoint = "https://jsonplaceholder.typicode.com/posts";

        public RestClientExample()
        {
            _client = new RestClient();
        }
        public async Task ReadAsync()
        {
            RestRequest request = new RestRequest(_postEndPoint, Method.Get);

            var response = await _client.GetAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content!;
                Console.WriteLine(result);
            }
        }

        public async Task EditAsync(int id)
        {
            RestRequest request = new RestRequest($"{_postEndPoint}/{id}", Method.Get);

            var response = await _client.GetAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content;
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

            RestRequest request = new RestRequest(_postEndPoint, Method.Post);
            request.AddJsonBody(postModel);

            var model = await _client.PostAsync(request);

            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(model.Content);
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

            RestRequest request = new RestRequest($"{_postEndPoint}/{id}", Method.Post);
            request.AddJsonBody(postModel);

            var model = await _client.PutAsync(request);
            if (model.IsSuccessStatusCode)
            {
                Console.WriteLine(model.Content);
            }
        }

        public async Task DeleteAsync(int id)
        {

            RestRequest request = new RestRequest($"{_postEndPoint}/{id}", Method.Delete);

            var PostModel = await _client.DeleteAsync(request);

            if (PostModel.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found!!");
                return;
            }

            if (PostModel.IsSuccessStatusCode)
            {
                Console.WriteLine("Successfully deleted.");
            }
        }

    }


}
