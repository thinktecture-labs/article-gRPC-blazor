using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.JSInterop;

namespace GrpcToDo.Web.Interceptors
{
    public class GrpcMessageInterceptor : Interceptor
    {
        private readonly IJSRuntime _jsRuntime;
        private IJSObjectReference _module;

        public GrpcMessageInterceptor(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            Console.WriteLine($"Starting call. Type: {context.Method.Type}. " +
                              $"Method: {context.Method.Name}.");

            var call = continuation(request, context);

            return new AsyncUnaryCall<TResponse>(
                HandleResponse(context.Method.Name, context.Method.Type.ToString(), request, call.ResponseAsync),
                call.ResponseHeadersAsync,
                call.GetStatus,
                call.GetTrailers,
                call.Dispose);
        }

        public override AsyncServerStreamingCall<TResponse> AsyncServerStreamingCall<TRequest, TResponse>(
            TRequest request,
            ClientInterceptorContext<TRequest, TResponse> context,
            AsyncServerStreamingCallContinuation<TRequest, TResponse> continuation)
        {
            Console.WriteLine($"Starting call. Type: {context.Method.Type}. " +
                              $"Method: {context.Method.Name}.");

            var call = continuation(request, context);

            return new AsyncServerStreamingCall<TResponse>(
                call.ResponseStream,
                HandleStreamMetaData(call.ResponseHeadersAsync, call.ResponseStream, context.Method.Name, request),
                call.GetStatus,
                call.GetTrailers,
                call.Dispose);
        }

        private async Task<TResponse> HandleResponse<TResponse, TRequest>(string method, string methodType,
            TRequest request, Task<TResponse> inner)
        {
            try
            {
                var result = await inner;
                await HandleGrpcRequest(method, methodType, request, result);
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Custom error", ex);
            }
        }

        private async Task<Metadata> HandleStreamMetaData<TResponse, TRequest>(Task<Metadata> data,
            IAsyncStreamReader<TResponse> stream, string method,
            TRequest request)
        {
            try
            {
                await HandleGrpcStreamingRequest(stream, request, method);
                var result = await data;
                return result;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Custom error", ex);
            }
        }

        private async Task HandleGrpcRequest<TRequest, TResponse>(string method, string methodType, TRequest request, TResponse response)
        {
            if (_module == null)
            {
                _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/grpc-message-handler.js");
            }

            await _module.InvokeVoidAsync("sendGrpcRequest", method, methodType.ToLower(), request, response);
        }

        private async Task HandleGrpcStreamingRequest<TRequest, TResponse>(IAsyncStreamReader<TResponse> data, TRequest request,
            string method)
        {
            if (_module == null)
            {
                _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import",
                    "./js/grpc-message-handler.js");
            }
            
            await _module.InvokeVoidAsync("sendGrpcRequest", method, "server_streaming", request, null);

            while (data.Current != null)
            {
                await _module.InvokeVoidAsync("sendGrpcRequest", method, "server_streaming", null, data.Current);
                await Task.Delay(1000);
            }
        }
    }
}