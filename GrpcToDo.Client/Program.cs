using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using GrpcToDo.Client;
using GrpcToDo.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ProtoBuf.Grpc.Client;
using GrpcToDo.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<ToDoService>();

builder.Services.AddScoped(services =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress("https://localhost:7298", new GrpcChannelOptions { HttpClient = httpClient });
    return channel.CreateGrpcService<IToDoService>();
});
builder.Services.AddScoped(services =>
{
    var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
    var channel = GrpcChannel.ForAddress("https://localhost:7298", new GrpcChannelOptions { HttpClient = httpClient });
    return channel.CreateGrpcService<ITimeService>();
});
builder.Services.AddMudServices();
await builder.Build().RunAsync();
