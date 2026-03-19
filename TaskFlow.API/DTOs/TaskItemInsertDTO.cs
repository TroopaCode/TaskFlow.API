namespace TaskFlow.API.DTOs
{
    public class TaskItemInsertDTO
    {
        public string Title { get; set; }

        //IsCompleted no estará presente en el DTO de inserción, ya que se asume que una nueva tarea no estará completada al momento de su creación.
        //El valor predeterminado para IsCompleted será false, lo que refleja el estado inicial de la tarea.
        //public bool IsCompleted { get; set; }
    }
}
