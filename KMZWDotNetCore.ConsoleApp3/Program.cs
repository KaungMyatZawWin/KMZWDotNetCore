using KMZWDotNetCore.ConsoleApp3;

//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync();
//await httpClientExample.EditAsync(1);
//await httpClientExample.EditAsync(102);
//await httpClientExample.CreateAsync(101, "Tesing http client title", "testing http client body");
//await httpClientExample.UpdateAsync(1,10, "Testing update title", "Testing update body");
//await httpClientExample.DeleteAsync(1);

RestClientExample restClientExample = new RestClientExample();
//await restClientExample.ReadAsync();
//await restClientExample.EditAsync(1);
//await restClientExample.CreateAsync(1, "testing rest", "testing rest body");
//await restClientExample.UpdateAsync(1, 10, "Testing update title", "Testing update body");
await restClientExample.DeleteAsync(1);
