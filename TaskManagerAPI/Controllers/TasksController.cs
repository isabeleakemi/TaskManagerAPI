using Microsoft.AspNetCore.Mvc;
using TaskManagerAPI.Models;
using TaskManagerAPI.Services;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private readonly RabbitMQService _rabbitMQ;

        public TasksController(RabbitMQService rabbitMQ)
        {
            _rabbitMQ = rabbitMQ;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            await _rabbitMQ.SendMessage(task);

            return Ok(new { message = "Task enviada para a fila!" });
        }

    }
}
