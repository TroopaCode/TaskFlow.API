using FluentValidation;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Services;

namespace TaskFlow.API.Validators
{
    public class TaskItemInsertValidator : AbstractValidator<TaskItemInsertDTO>
    {
        public TaskItemInsertValidator() {
            RuleFor(task => task.Title) //Valida el título de la tarea
            .NotEmpty().WithMessage("El título es obligatorio.") //El título no puede estar vacío
            .MaximumLength(30).WithMessage("El título no puede exceder los 30 caracteres.");

            RuleFor(task => task.Title) //Valida el título de la tarea
            .MinimumLength(5).WithMessage("El título debe contener al menos 5 caracteres");
        }
    }
}
