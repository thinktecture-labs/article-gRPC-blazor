using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcToDo.Server.Data;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace GrpcToDo.Server.GrpcServices
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _dataContext;

        public ToDoService(ToDoDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<ToDoPostResponse> AddToDoItemAsync(ToDoData data)
        {
            _dataContext.ToDoDbItems.Add(data);
            var result = _dataContext.SaveChanges();
            if (result > 0)
            {
                return Task.FromResult(new ToDoPostResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Added Successfully"
                });
            }

            return Task.FromResult(new ToDoPostResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public async Task<ToDoPostResponse> UpdateToDoItemAsync(ToDoData data)
        {
            var item = await _dataContext.ToDoDbItems
                .FirstOrDefaultAsync(toDoData => toDoData.Id == data.Id);
            if (item == null)
            {
                return new ToDoPostResponse()
                {
                    Status = false,
                    StatusCode = 404,
                    StatusMessage = "Item not found."
                };
            }

            item.Title = data.Title;
            item.Description = data.Description;
            item.Status = data.Status;
            await _dataContext.SaveChangesAsync();
            return new ToDoPostResponse()
            {
                Status = true,
                StatusCode = 100,
                StatusMessage = "Added Successfully"
            };
        }

        public Task<ToDoPostResponse> DeleteToDoItemAsync(ToDoQuery query)
        {
            var item = _dataContext.ToDoDbItems
                .Single(toDoData => toDoData.Id == query.Id);

            _dataContext.ToDoDbItems.Remove(item);

            var result = _dataContext.SaveChanges();

            if (result > 0)
            {
                return Task.FromResult(new ToDoPostResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Deleted Successfully"
                });
            }

            return Task.FromResult(new ToDoPostResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public Task<ToDoData> GetTodoItem(ToDoQuery query)
        {
            return _dataContext.ToDoDbItems.Where(item => item.Id == query.Id).FirstOrDefaultAsync();
        }

        public async Task<List<ToDoData>> GetToDosAsync()
        {
            return await _dataContext.ToDoDbItems.ToListAsync();
        }
    }
}