using Microsoft.AspNetCore.Components;
using MudBlazor;
using Grpc.Core;
using GrpcToDo.Shared.Services;
using ProtoBuf.Grpc;

namespace GrpcToDo.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject] private ITimeService TimeService { get; set; } = default!;

        private string _time = "";
        private CancellationTokenSource _cts;

        private MudTheme _currentTheme = new();

        private readonly MudTheme _defaultTheme = new()
        {
            Palette = new Palette()
            {
                Black = "#272c34",
                AppbarBackground = "#ff584f",
                ActionDefault = "#ff584f",
                Primary = "#ff584f",
                Secondary = "#3d6fb4"
            }
        };

        protected override async Task OnInitializedAsync()
        {
            _currentTheme = _defaultTheme;
            await StartTime();
            await base.OnInitializedAsync();
        }

        public void Dispose()
        {
            StopTime();
        }

        private async Task StartTime()
        {
            _cts = new CancellationTokenSource();
            var options = new CallOptions(cancellationToken: _cts.Token);

            try
            {
                await foreach (var time in TimeService.SubscribeAsync(new CallContext(options)))
                {
                    _time = time;
                    StateHasChanged();
                }
            }
            catch (RpcException)
            {
            }
            catch (OperationCanceledException)
            {
            }
        }

        private void StopTime()
        {
            _cts?.Cancel();
            _cts = null;
            _time = "";
        }
    }
}