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

        public Task<ToDoRequestResponse> AddToDoItemAsync(ToDoData data)
        {
            _dataContext.ToDoDbItems.Add(data);
            var result = _dataContext.SaveChanges();
            if (result > 0)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Added Successfully"
                });
            }

            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public async Task<ToDoRequestResponse> UpdateToDoItemAsync(ToDoData data)
        {
            var item = await _dataContext.ToDoDbItems
                .FirstOrDefaultAsync(toDoData => toDoData.Id == data.Id);
            if (item == null)
            {
                return new ToDoRequestResponse()
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
            return new ToDoRequestResponse()
            {
                Status = true,
                StatusCode = 100,
                StatusMessage = "Added Successfully"
            };
        }

        public Task<ToDoRequestResponse> DeleteToDoItemAsync(ToDoIdQuery idQuery)
        {
            var item = _dataContext.ToDoDbItems
                .Single(toDoData => toDoData.Id == idQuery.Id);

            _dataContext.ToDoDbItems.Remove(item);

            var result = _dataContext.SaveChanges();

            if (result > 0)
            {
                return Task.FromResult(new ToDoRequestResponse()
                {
                    Status = true,
                    StatusCode = 100,
                    StatusMessage = "Deleted Successfully"
                });
            }

            return Task.FromResult(new ToDoRequestResponse()
            {
                Status = false,
                StatusCode = 500,
                StatusMessage = "Issue Occured."
            });
        }

        public async Task<List<ToDoData>> GetToDosAsync()
        {
            return await _dataContext.ToDoDbItems.ToListAsync();
        }
    }
}