using System.Collections.Generic;
using System.Threading.Tasks;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Web.Components;
using GrpcToDo.Web.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GrpcToDo.Web.Pages
{
    public partial class Index
    {
        [Inject] private ToDoService ToDoService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }

        private List<ToDoData> _toDoItems;

        protected override async Task OnInitializedAsync()
        {
            await GetToDoListAsync();
        }

        private async Task GetToDoListAsync()
        {
            _toDoItems = await ToDoService.GetToDoListAsync();
        }

        private async Task OpenDialog()
        {
            var reference = DialogService.Show<ToDoItemEditor>("Aufgabe hinzufügen");
            var result = await reference.Result;
            if (!result.Cancelled)
            {
                await RefreshAsync();
            }
        }

        private async Task RefreshAsync()
        {
            _toDoItems = await ToDoService.GetToDoListAsync();
            StateHasChanged();
        }
    }
}