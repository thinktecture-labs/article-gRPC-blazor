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
        Task<ToDoItems> GetToDosAsync();
        Task<ToDoData> GetTodoItem(ToDoQuery query);
        Task<ToDoPostResponse> AddToDoItemAsync(ToDoData data);
        Task<ToDoPostResponse> UpdateToDoItemAsync(ToDoData data);
        Task<ToDoPostResponse> DeleteToDoItemAsync(ToDoQuery query);
    }
}
