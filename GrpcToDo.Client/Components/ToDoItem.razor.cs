using GrpcToDo.Client.Services;
using GrpcToDo.Shared.DTOs;
using Microsoft.AspNetCore.Components;

namespace GrpcToDo.Client.Components
{
    public partial class ToDoItem
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;

        [Parameter] public ToDoData Item { get; set; } = default!;

        [Parameter] public EventCallback<bool> ToDoItemChanged { get; set; }

        private string _cssClass = "item";

        protected override void OnInitialized()
        {
            _cssClass = Item.Status ? $"item checked" : "item";
            base.OnInitialized();
        }

        private async Task UpdateTaskAsync()
        {
            Item.Status = true;
            _cssClass = Item.Status ? $"item checked" : "item";
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