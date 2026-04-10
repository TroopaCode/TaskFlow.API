using AutoMapper;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;

namespace TaskFlow.API.AutoMappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<TaskItemInsertDTO, TaskItem>();
            CreateMap<TaskItemUpdateDTO, TaskItem>();
            CreateMap<TaskItem, TaskItemDTO>();

            //Con esto evitas que el Id se mapee al crear un nuevo TaskItem, ya que el Id se genera automáticamente en la base de datos
            CreateMap<TaskItemInsertDTO, TaskItem>()
            .ForMember(dest => dest.TaskItemId, opt => opt.Ignore());
        }
    }
}
