using GrpcToDo.Shared.DTOs;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GrpcToDo.Shared.Services
{
    [ServiceContract]
    public interface ITimeService
    {
        IAsyncEnumerable<string> SubscribeAsync(CallContext context = default);
    }
}
