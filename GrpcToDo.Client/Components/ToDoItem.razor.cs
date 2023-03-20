using GrpcToDo.Client.Services;
using GrpcToDo.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GrpcToDo.Client.Components
{
    public partial class ToDoItem
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;

        [Parameter] public ToDoData Item { get; set; } = default!;
        [Parameter] public string Class { get; set; } = string.Empty;

        [Parameter] public EventCallback<bool> ToDoItemChanged { get; set; }

        private string _cssClass = string.Empty;

        protected override void OnInitialized()
        {
            _cssClass = Item.Status ? $"{Class} checked" : Class;
            base.OnInitialized();
        }

        private async Task UpdateTaskAsync()
        {
            Item.Status = true;
            _cssClass = Item.Status ? $"{Class} checked" : Class;
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