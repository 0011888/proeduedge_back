using System;
using System.Reflection;
using proeduedge.Models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connString = builder.Configuration.GetConnectionString("proeduedge");
builder.Services.AddControllers();
//builder.Services.AddScoped<IRepository<Book>, BookRepository>();
builder.Services.AddDbContext<AppDBContext>(options =>
{
    options.UseSqlServer(connString);
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "ProEduEdge", Version = "v1" });
});
var app = builder.Build();
app.UseSwagger(x => x.SerializeAsV2 = true);
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
