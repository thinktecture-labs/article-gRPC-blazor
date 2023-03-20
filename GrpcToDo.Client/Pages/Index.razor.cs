using GrpcToDo.Client.Components;
using GrpcToDo.Client.Services;
using GrpcToDo.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace GrpcToDo.Client.Pages
{
    public partial class Index
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;
        [Inject] private IDialogService DialogService { get; set; } = default!;

        private List<ToDoData> _toDoItems = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadTodosFromServerAsync();
        }

        private async Task OpenDialog()
        {
            var reference = DialogService.Show<ToDoItemEditor>("Aufgabe hinzufügen");
            var result = await reference.Result;
            if (result is null || !result.Canceled)
            {
                await RefreshAsync();
            }
        }

        private async Task RefreshAsync()
        {
            await LoadTodosFromServerAsync();
        }

        private async Task LoadTodosFromServerAsync()
        {
            try
            {
                _toDoItems = await ToDoService.GetToDoListAsync();
                StateHasChanged();
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to load todo list.");
                _toDoItems = new List<ToDoData>();
            }
        }
    }
}