using Microsoft.AspNetCore.Http.HttpResults;
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

app.MapPost("/birds", (BirdDataModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    requestModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;

    result.Tbl_Bird.Add(requestModel);

    var jsonStrToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(requestModel);

});

app.MapPut("/birds/{id}", (int id, BirdDataModel requestModal) =>
{
    string filePath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(filePath);

    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var foundBird = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (foundBird is null)
    {
        return Results.BadRequest("No data found!!");
    }

    foundBird.BirdMyanmarName = requestModal.BirdMyanmarName;
    foundBird.BirdEnglishName = requestModal.BirdEnglishName;
    foundBird.Description = requestModal.Description;
    foundBird.ImagePath = requestModal.ImagePath;

    result.Tbl_Bird.Add(foundBird);

    var jsonToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(filePath, jsonToWrite);

    return Results.Ok(foundBird);

});

app.MapPatch("/birds/{id}", (int id, BirdDataModel requestModel) =>
{
    string filePath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(filePath);

    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var foundBird = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (foundBird is null)
    {
        return Results.BadRequest("No data found!!");
    }

    if (!string.IsNullOrEmpty(requestModel.BirdMyanmarName))
    {
        foundBird.BirdMyanmarName = requestModel.BirdMyanmarName;
    }
    if (!string.IsNullOrEmpty(requestModel.BirdEnglishName))
    {
        foundBird.BirdEnglishName = requestModel.BirdEnglishName;
    }
    if (!string.IsNullOrEmpty(requestModel.Description))
    {
        foundBird.Description = requestModel.Description;
    }
    if (!string.IsNullOrEmpty(requestModel.ImagePath))
    {
        foundBird.ImagePath = requestModel.ImagePath;
    }

    result.Tbl_Bird.Add(foundBird);

    var jonToWrite = JsonConvert.SerializeObject(result);
    File.WriteAllText(filePath, jonToWrite);

    return Results.Ok(foundBird);


});

app.Run();



public class BirdResponseModel
{
    public List<BirdDataModel> Tbl_Bird { get; set; }
}

public class BirdDataModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}