using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Repositories.Interfaces;
using TaskFlow.API.Services.Interfaces;

namespace TaskFlow.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository repository, IMapper mapper)
        {
            _taskRepository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDTO>> GetAll() {
            var tasks = await _taskRepository.GetAll();
            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
           

        public async Task<TaskItemDTO?> GetById(int id)
        {
            var task = await _taskRepository.GetById(id);
            if (task == null) {
                return null;
            }
            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<TaskItemDTO> Create(TaskItemInsertDTO taskItemInsertDTO)
        {
            var task = _mapper.Map<TaskItem>(taskItemInsertDTO); // Mapear el DTO a la entidad TaskItem
            var result = await _taskRepository.Create(task);
            return _mapper.Map<TaskItemDTO>(result); // Mapear la entidad TaskItem a TaskItemDTO para devolverlo
        }

        public async Task<bool> Update(int id, TaskItemUpdateDTO task)
        {
            var taskItem = await _taskRepository.GetById(id);
            if (taskItem == null)
                return false;

            _mapper.Map(task, taskItem); //El mapper no es requerido aquí?
            return await _taskRepository.Update(taskItem);
        }

        public async Task<bool> Delete(int id)
        {
            var taskItem = await _taskRepository.GetById(id);
            if (taskItem == null)
            {
                return false;
            }
            return await _taskRepository.Delete(taskItem);
        }
        
    }
}
