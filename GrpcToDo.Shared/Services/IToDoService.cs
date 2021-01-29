using GrpcToDo.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GrpcToDo.Shared.Services
{
    [ServiceContract]
    public interface IToDoService
    {
        [OperationContract]
        Task<List<ToDoData>> GetToDosAsync();
        
        [OperationContract]
        Task<ToDoRequestResponse> AddToDoItemAsync(ToDoData data);
        
        [OperationContract]
        Task<ToDoRequestResponse> UpdateToDoItemAsync(ToDoData data);
        
        [OperationContract]
        Task<ToDoRequestResponse> DeleteToDoItemAsync(ToDoIdQuery idQuery);
    }
}
