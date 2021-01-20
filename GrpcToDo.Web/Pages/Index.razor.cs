using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Web.Services;
using Microsoft.AspNetCore.Components;

namespace GrpcToDo.Web.Pages
{
    public partial class Index
    {
        [Inject] private ToDoService ToDoService { get; set; }

        private List<ToDoData> _toDoItems;
        private bool _dialogIsOpen;

        protected override async Task OnInitializedAsync()
        {
            await GetToDoListAsync();
        }

        protected async Task GetToDoListAsync()
        {
            _toDoItems = await ToDoService.GetToDoList();
        }

        protected async Task CloseDialog(bool refresh)
        {
            _dialogIsOpen = false;
            if (refresh)
            {
                await Refresh();
            }
        }

        protected async Task Refresh()
        {
            _toDoItems = await ToDoService.GetToDoList();
            StateHasChanged();
        }
    }
}