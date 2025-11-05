using Microsoft.AspNetCore.Mvc;
using ToDo_app.Model;
using ToDo_app.Repository;

namespace ToDo_app.Controllers
{
    // Adicionar os atributos e herança
    [ApiController]
    [Route("api/[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly IRepository<Tarefa> _repository;
        public TarefasController(IRepository<Tarefa> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tarefas = await _repository.GetAllAsync();
            return Ok(tarefas);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound($"Tarefa com ID {id} não encontrada.");
            }
            return Ok(tarefa);
        }
        [HttpPost]
        public async Task<IActionResult> InsertTarefa(Tarefa novaTarefa)
        {
            await _repository.AddAsync(novaTarefa);
            return CreatedAtAction(nameof(GetById), novaTarefa);
        }
        [HttpDelete]
        public async Task<IActionResult> RemoveTarefa(int id)
        {
            var tarefa = await _repository.GetByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound($"Tarefa com ID {id} não encontrada.");
            }
            else
            {
                await _repository.DeleteAsync(id);
                return Ok(tarefa);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateTarefa(Tarefa tarefa)
        {
            var _tarefa = await _repository.GetByIdAsync(tarefa.Id);
            if (_tarefa == null)
            {
                return NotFound($"Tarefa com ID {tarefa.Id} não encontrada.");
            }else
            {
                await _repository.UpdateAsync(tarefa);
                return Ok(tarefa);
            }
        }
    }
}