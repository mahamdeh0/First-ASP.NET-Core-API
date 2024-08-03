using FirstASP.NETCoreAPI;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Filter;
using WebApplication5.middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("config.json");

builder.Services.AddLogging(cfg =>
{
    cfg.AddDebug();
    cfg.AddConsole();
});

// Add services to the container.
builder.Services.Configure<AttachmentOptions>(builder.Configuration.GetSection("Attachments"));

builder.Services.AddControllers(option => {

    option.Filters.Add<LogactivityFilter>();


    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbcontext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefultConnection"]));


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
