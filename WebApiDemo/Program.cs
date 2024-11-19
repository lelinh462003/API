using Microsoft.EntityFrameworkCore;
using WebApiDemo.Data;
using WebApiDemo.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Thêm d?ch v? vào container.
string strcnn = builder.Configuration.GetConnectionString("cnn");
builder.Services.AddDbContext<ApiContext>(options => options.UseSqlServer(strcnn));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.MyConfigureRepositoryService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
