using Microsoft.AspNetCore.Mvc;
using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});
builder.Services.AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));

builder.Services
    .AddRefitClient<ISnakeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.MapGet("/biris", async ([FromServices] HttpClient httpClient) =>
//{
//    var response = await httpClient.GetAsync("birds");
//    return await response.Content.ReadAsStringAsync();
//});


//app.MapGet("pick-a-pile", async ([FromServices] RestClient respClient) =>
//{
//    RestRequest restRequest = new RestRequest("pick-a-pile", Method.Get);
//    var response = await respClient.GetAsync(restRequest);
//    return response.Content;
//});


app.MapGet("/snakes", async ([FromServices] ISnakeApi snakeApi) =>
{
    var response = await snakeApi.GetSnake();
    return response;
});

app.Run();

public interface ISnakeApi
{
    [Get("/snakes")]
    Task< List<SnakeModel>> GetSnake();
}



public class SnakeModel
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string MMName { get; set; }
    public string EngName { get; set; }
    public string Detail { get; set; }
    public string IsPoison { get; set; }
    public string IsDanger { get; set; }
}
