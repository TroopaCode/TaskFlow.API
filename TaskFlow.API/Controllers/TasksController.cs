using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Models;
using TaskFlow.API.Paginacion;
using TaskFlow.API.Services;
using TaskFlow.API.Services.Interfaces;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDTO>>> GetAll()
        {
            var tasks = await _taskService.GetAll();
            return Ok(tasks);
        }*/

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItemDTO>>> GetAll([FromQuery] PaginationParams pagination)
        {
            var tasks = await _taskService.GetPaged(pagination);
            return Ok(tasks);
        }

        [HttpGet("{id}", Name = "GetTaskById")]
        public async Task<ActionResult<TaskItemDTO>> GetById(int id)
        {
            var task = await _taskService.GetById(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskItemDTO>> Create(TaskItemInsertDTO dto)
        {
            var task = await _taskService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = task.TaskItemID },
                task
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TaskItemUpdateDTO dto)
        {
            var updated = await _taskService.Update(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _taskService.Delete(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpGet("error")]
        public IActionResult TestError()
        {
            throw new Exception("Este es un error de prueba");
        }
    }
}


