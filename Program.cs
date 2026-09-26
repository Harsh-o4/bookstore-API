using books.api.dto;
using books.api.endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.mapBooksEndpoints();

app.Run();
