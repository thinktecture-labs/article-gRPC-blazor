using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Shared.Services;

namespace GrpcToDo.Web.Services
{
    public class ToDoService
    {
        private readonly IToDoService _toDoService;

        public ToDoService(IToDoService todoService)
        {
            _toDoService = todoService;
        }

        public Task AddToDoData(ToDoData data)
        {
            return _toDoService.AddToDoItemAsync(data);
        }

        public Task UpdateToDoData(ToDoData data)
        {
            return _toDoService.UpdateToDoItemAsync(data);
        }

        public Task DeleteDataAsync(string id)
        {
            return _toDoService.DeleteToDoItemAsync(new ToDoIdQuery() {Id = Convert.ToInt32(id)});
        }

        public async Task<List<ToDoData>> GetToDoListAsync()
        {
            return await _toDoService.GetToDosAsync();
        }
    }
}