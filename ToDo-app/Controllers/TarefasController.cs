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

    }
}