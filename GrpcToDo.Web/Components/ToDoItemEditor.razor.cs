using System;
using System.Threading.Tasks;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;

namespace GrpcToDo.Web.Components
{
    public partial class ToDoItemEditor
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; }

        [Parameter] public EventCallback<bool> DialogClosed { get; set; }
        [Inject] private ToDoService ToDoService { get; set; }

        private string _newTaskTitle;
        private string _newTaskDescription;
        private bool _success;
        private MudForm _form;
        private void Close() => MudDialog.Close();

        private async Task AddNewTask(MouseEventArgs e)
        {
            var rnd = new Random();
            await ToDoService.AddToDoData(new ToDoData
            {
                Id = rnd.Next(0, Int32.MaxValue),
                Title = _newTaskTitle,
                Description = _newTaskDescription,
                Status = false
            });
            _newTaskTitle = null;
            _newTaskDescription = null;
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
}