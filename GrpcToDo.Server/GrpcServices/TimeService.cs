using System.Runtime.CompilerServices;
using GrpcToDo.Shared.Services;
using ProtoBuf.Grpc;

namespace GrpcToDo.Server.GrpcServices
{
    public class TimeService : ITimeService
    {
        public IAsyncEnumerable<string> SubscribeAsync(CallContext context = default)
            => SubscribeAsyncImpl(context.CancellationToken);

        private async IAsyncEnumerable<string> SubscribeAsyncImpl(
            [EnumeratorCancellation] CancellationToken cancel)
        {
            while (!cancel.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), cancel);
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                yield return $"{DateTime.Now.ToLongTimeString()}";
            }
        }
    }
}