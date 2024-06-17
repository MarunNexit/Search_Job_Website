using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Web_search_job.Data;
using Web_search_job.DatabaseClasses.UserFolder.Mediator.Auth;
using Web_search_job.Host.Extensions;
using MediatR;
using Web_search_job.Host.Middleware;
using Web_search_job.Services.UserService;
using Web_search_job.Controllers.DatabaseControllers;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using Azure.Identity;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"), options => options.CommandTimeout(60)).ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.MultipleCollectionIncludeWarning));
});

builder.Services.AddScoped<UserController>();
builder.Services.AddScoped<OtherInfoController>();

builder.Services.RegisterAuthentication(configuration)
    .AddMediatR(typeof(LoginUserCommand));

builder.Services.AddHttpClient();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.RegisterSwagger();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddCors(options => options.AddPolicy(name: "JobOrigins",
    policy =>
    {
        policy.WithOrigins("https://localhost:44434", "http://localhost:44434").AllowAnyMethod().AllowAnyHeader();
    }
    ));



builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddControllersWithViews();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => // UseSwaggerUI is called only in Development.
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
        options.DocumentTitle = "MySwagger";
    }); 
}

app.UseCors("JobOrigins");
app.ConfigureExceptionHandler();
app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");*/


if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}




using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await SeedManager.Seed(services);
}



app.UseStaticFiles();


app.Run();



