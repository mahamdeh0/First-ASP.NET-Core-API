using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Filter;
using WebApplication5.middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(option => {

    option.Filters.Add<LogactivityFilter>();


    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
    options.UseSqlServer("server=MAHAMDEH\\SQLEXPRESS;database=CRUD;integrated security=true;trust server certificate=true"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ProfilingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
