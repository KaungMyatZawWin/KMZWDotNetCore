using KMZWDotNetCore.ConsoleApp3;

HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync();
//await httpClientExample.EditAsync(1);
//await httpClientExample.EditAsync(102);
//await httpClientExample.CreateAsync(101, "Tesing http client title", "testing http client body");
//await httpClientExample.UpdateAsync(1,10, "Testing update title", "Testing update body");
await httpClientExample.DeleteAsync(1);