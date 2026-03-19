namespace TaskFlow.API.DTOs
{
    public class TaskItemUpdateDTO
    {
        //El ID no se actualiza, por lo que no se incluye en el DTO de actualización. El ID es un identificador único que se asigna
        //a cada tarea y no debe cambiarse una vez que la tarea ha sido creada. En su lugar, el ID se utiliza para identificar qué tarea se va a actualizar.
        //public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
