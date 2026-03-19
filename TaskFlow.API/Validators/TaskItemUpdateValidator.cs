using FluentValidation;
using TaskFlow.API.DTOs;

namespace TaskFlow.API.Validators
{
    public class TaskItemUpdateValidator : AbstractValidator<TaskItemUpdateDTO>
    {
        public TaskItemUpdateValidator()
        {
            RuleFor(task => task.Title) //Valida el título de la tarea
            .NotEmpty().WithMessage("El título es obligatorio.") //El título no puede estar vacío
            .MaximumLength(30).WithMessage("El título no puede exceder los 30 caracteres.") //El título no puede tener más de 30 caracteres
            .When(task => task.IsCompleted); //Solo se aplican estas reglas si la tarea está marcada como completada

            RuleFor(task => task.Title) //Valida el título de la tarea
            .MinimumLength(5).WithMessage("El título debe contener al menos 5 caracteres") //Mensaje de error personalizado
            .When(task => task.IsCompleted); //Solo se aplican estas reglas si la tarea está marcada como completada

            RuleFor(task => task)
                .Must(task => !task.IsCompleted || task.Title.Length >= 5)
                .WithMessage("Una tarea marcada como completada debe tener un título de al menos 5 caracteres.");
        }
    }
}
