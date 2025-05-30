using ImageUp.Data.Models;
using ImageUp.Data.Repositories;
using ImageUp.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ApplicationDbContext>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IFileService, FileService>();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
     policy =>
    {
        policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); 
    });
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{ 
    FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath,
    "Uploads")),
    RequestPath = "/Resources"
});
app.UseCors();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
