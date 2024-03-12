using System;
using AsyncApi.Net.Ui.Extensions;
using AsyncApi.Net.Ui.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder();
builder.Services.AddRazorPages();
builder.Services.AddAsyncApiDocumentation(new AsyncApiDocInfo()
{
    Title = "My title AsyncAPI",
    Description = "The great description of the my AcyncApi",
    ContactName = "Crazy Coder",
    ContactUrl = "https://www.microsoft.com/"
});

var app = builder.Build();
app.MapControllers();
app.MapRazorPages();
app.Run();
Console.WriteLine("Sample is running");
