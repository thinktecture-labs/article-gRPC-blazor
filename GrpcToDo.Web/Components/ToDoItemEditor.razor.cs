using System;
using System.Threading.Tasks;
using GrpcToDo.Shared.DTOs;
using GrpcToDo.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GrpcToDo.Web.Components
{
    public partial class ToDoItemEditor
    {
        [Inject] private ToDoService ToDoService { get; set; }

        [Parameter]
        public bool DialogIsOpen
        {
            get => _dialogIsOpen;
            set => _dialogIsOpen = value;
        }

        [Parameter] public EventCallback<bool> DialogClosed { get; set; }

        private bool _dialogIsOpen;
        private string _newTaskTitle;
        private string _newTaskDescription;

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
            _dialogIsOpen = false;
            _newTaskTitle = null;
            _newTaskDescription = null;
            await DialogClosed.InvokeAsync(true);
        }

        private async Task Close()
        {
            _dialogIsOpen = false;
            await DialogClosed.InvokeAsync(false);
        }
    }
}