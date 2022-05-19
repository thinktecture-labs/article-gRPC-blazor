using System;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using GrpcToDo.Shared.Services;
using GrpcToDo.Web.Interceptors;
using GrpcToDo.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using MudBlazor.Services;
using ProtoBuf.Grpc.Client;

namespace GrpcToDo.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<ToDoService>();

            builder.Services.AddScoped(services =>
            {
                var js = services.GetService<IJSRuntime>();
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
                var invoker = channel.Intercept(new GrpcMessageInterceptor(js));
                return invoker.CreateGrpcService<IToDoService>();
            });
            builder.Services.AddScoped(services =>
            {
                var js = services.GetService<IJSRuntime>();
                var httpClient = new HttpClient(new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()));
                var channel = GrpcChannel.ForAddress("https://localhost:5001", new GrpcChannelOptions { HttpClient = httpClient });
                var invoker = channel.Intercept(new GrpcMessageInterceptor(js));
                return invoker.CreateGrpcService<ITimeService>();
            });
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
