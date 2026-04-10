using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Paginacion;
using TaskFlow.API.Repositories;
using TaskFlow.API.Repositories.Interfaces;
using TaskFlow.API.Repositories.UnitOfWork;
using TaskFlow.API.Services.Interfaces;

namespace TaskFlow.API.Services
{
    public class TaskService : ITaskService
    {
        //private readonly ITaskRepository _taskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskItemDTO>> GetAll() {
            var tasks = await _unitOfWork.Tasks.GetAll();
            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }
           

        public async Task<TaskItemDTO?> GetById(int id)
        {
            var task = await _unitOfWork.Tasks.GetById(id);
            if (task == null) {
                return null;
            }
            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<TaskItemDTO> Create(TaskItemInsertDTO taskItemInsertDTO)
        {
            var task = _mapper.Map<TaskItem>(taskItemInsertDTO); // Mapear el DTO a la entidad TaskItem
            var result = await _unitOfWork.Tasks.Create(task);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TaskItemDTO>(result); // Mapear la entidad TaskItem a TaskItemDTO para devolverlo
        }

        public async Task<bool> Update(int id, TaskItemUpdateDTO task)
        {
            var taskItem = await _unitOfWork.Tasks.GetById(id);
            if (taskItem == null)
                return false;

            _mapper.Map(task, taskItem);
            await _unitOfWork.Tasks.Update(taskItem);
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var taskItem = await _unitOfWork.Tasks.GetById(id);
            if (taskItem == null)
            {
                return false;
            }
            await _unitOfWork.Tasks.Delete(taskItem);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TaskItemDTO>> GetPaged(PaginationParams pagination)
        {
            var tasks = await _unitOfWork.Tasks.GetPaged(pagination.Page, pagination.PageSize);
            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }

    }
}
