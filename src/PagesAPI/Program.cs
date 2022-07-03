using Microsoft.EntityFrameworkCore;
using PagesCommon.Interfaces;
using PagesCommon.Services;
using PagesConfig;
using PagesData.Context;
using PagesData.Interfaces;
using PagesData.Repositories;
using PagesServices.Interfaces;
using PagesServices.Services;

var _allowedCors = "allowedCors";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _allowedCors,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration["AllowedHosts"])
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

builder.Services.Configure<FilesConfig>(builder.Configuration.GetSection("FilesPath"));

var x = builder.Services.AddDbContext<PagesContext>(
       options => options.UseSqlServer(builder.Configuration.GetSection("Database")["ConnectionString"]));

builder.Services.AddScoped<IPostProcessor, PostProcessor>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PagesContext>();
    DataSeeder seeder = new DataSeeder(dbContext);

    seeder.Seed();
}

//// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var options = new DefaultFilesOptions();
options.DefaultFileNames.Clear();
options.DefaultFileNames.Add("index.html");
app.UseDefaultFiles(options);

app.UseStaticFiles();

app.UseAuthorization();

app.UseCors(_allowedCors);

app.MapControllers();

app.Run();

