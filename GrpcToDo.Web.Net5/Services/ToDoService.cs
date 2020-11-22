using GrpcToDo.Shared.DTOs;
using GrpcToDo.Shared.Services;
using System;
using System.Threading.Tasks;

namespace GrpcToDo.Web.Services
{
    public class ToDoService
    {
        private readonly IToDoService _toDoService;

        public ToDoService(IToDoService todoService)
        {
            _toDoService = todoService;
        }

        public async Task<bool> AddToDoData(ToDoData data)
        {
            var response = await _toDoService.AddToDoItemAsync(data);
            return response.Status;
        }

        public async Task<bool> UpdateToDoData(ToDoData data)
        {
            var response = await _toDoService.UpdateToDoItemAsync(data);
            return response.Status;
        }

        public async Task<bool> DeleteDataAsync(string id)
        {
            var response = await _toDoService.DeleteToDoItemAsync(new ToDoQuery() { Id = Convert.ToInt32(id) });
            return response.Status;
        }
        public async Task<List<ToDoData>> GetToDoList()
        {
            return await _toDoService.GetToDosAsync();
        }

        public async Task<ToDoData> GetToDoItemAsync(int id)
        {
            return await _toDoService.GetTodoItem(new ToDoQuery() { Id = Convert.ToInt32(id) });
        }
    }
}
