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
        [Parameter] public string Class { get; set; }

        [Parameter] public EventCallback<bool> ToDoItemChanged { get; set; }

        private string _cssClass;

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