using GrpcToDo.Server.Data;
using GrpcToDo.Server.GrpcServices;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.Server;
using System.IO.Compression;


var _corsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(_corsPolicy,
        builder =>
        {
            builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
                .WithOrigins("https://localhost:7085");
        });
});
builder.Services.AddGrpc();
builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });
builder.Services.AddDbContext<ToDoDbContext>(options => options.UseInMemoryDatabase("ToDoDatabase"));
builder.Services.AddControllers();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(_corsPolicy);
app.UseRouting();
app.UseGrpcWeb();
app.UseAuthorization();
app.MapControllers();
app.MapGrpcService<ToDoService>().EnableGrpcWeb();
app.MapGrpcService<TimeService>().EnableGrpcWeb();

app.Run();
