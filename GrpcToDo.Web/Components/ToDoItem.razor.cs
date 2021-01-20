using System.Threading.Tasks;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Web.Services;
using Microsoft.AspNetCore.Components;

namespace GrpcToDo.Web.Components
{
    public partial class ToDoItem
    {
        [Inject] private ToDoService ToDoService { get; set; }

        [Parameter] public ToDoData Item { get; set; }

        [Parameter] public EventCallback<bool> ToDoItemChanged { get; set; }

        private async Task UpdateTaskAsync()
        {
            Item.Status = true;
            await ToDoService.UpdateToDoData(Item);
            await ToDoItemChanged.InvokeAsync(true);
        }

        private async Task DeleteTask()
        {
            await ToDoService.DeleteDataAsync(Item.Id.ToString());
            await ToDoItemChanged.InvokeAsync(true);
        }
    }
}