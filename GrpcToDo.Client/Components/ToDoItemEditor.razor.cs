using System;
using System.Threading.Tasks;
using GrpcToDo.Client.Services;
using GrpcToDo.Shared.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GrpcToDo.Client.Components
{
    public partial class ToDoItemEditor
    {
        [Inject] private ToDoService ToDoService { get; set; } = default!;
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
        [Parameter] public EventCallback<bool> DialogClosed { get; set; }

        private string? _newTaskTitle;
        private string? _newTaskDescription;
        private bool _success;
        private void Close() => MudDialog.Close();

        private async Task AddNewTask(MouseEventArgs e)
        {
            var rnd = new Random();
            await ToDoService.AddToDoData(new ToDoData
            {
                Id = rnd.Next(0, int.MaxValue),
                Title = _newTaskTitle ?? string.Empty,
                Description = _newTaskDescription ?? string.Empty,
                Status = false
            });
            _newTaskTitle = null;
            _newTaskDescription = null;
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}