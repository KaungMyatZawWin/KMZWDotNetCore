using Newtonsoft.Json;

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

app.MapGet("/birds", () =>
{
    string folderPath = "Data/Birds.json";
    var jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    return Results.Ok(result.Tbl_Bird);
})
.WithName("GetBirds")
.WithOpenApi();

app.MapGet("/bird/{id}", (int id) =>
{
    string forderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(forderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var model = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (model is null)
    {
        return Results.BadRequest("No data found!!");
    }

    return Results.Ok(model);
})
.WithName("GetById")
.WithOpenApi();




app.Run();



public class BirdResponseModel
{
    public BirdDataModel[] Tbl_Bird { get; set; }
}

public class BirdDataModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
