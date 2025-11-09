using Microsoft.AspNetCore.Mvc;
using ToDo_app.Model;
using ToDo_app.Repository;

[ApiController]
[Route("api/[controller]")] // Rota: api/tarefas
public class TarefasController : ControllerBase
{
    private readonly IRepository<Tarefa> _repository;

    // Injeção de Dependência no Construtor
    public TarefasController(IRepository<Tarefa> repository)
    {
        _repository = repository;
    }

    // GET api/tarefas
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tarefas = await _repository.GetAllAsync();
        return Ok(tarefas);
    }

    // GET api/tarefas/5
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

    // POST api/tarefas (Criação)
    [HttpPost]
    public async Task<IActionResult> InsertTarefa(Tarefa novaTarefa)
    {
        // O Id será preenchido após o AddAsync chamar SaveChanges no Repository
        await _repository.AddAsync(novaTarefa);

        // Retorna Status 201 Created com a nova rota (Location Header)
        return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
    }

    // PUT api/tarefas/5 (Atualização Completa)
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateTarefa(int id, Tarefa tarefa)
    {
        if (id != tarefa.Id)
        {
            return BadRequest("O ID da rota não corresponde ao ID do corpo da tarefa.");
        }

        var _tarefa = await _repository.GetByIdAsync(id);
        if (_tarefa == null)
        {
            return NotFound($"Tarefa com ID {id} não encontrada.");
        }

        await _repository.UpdateAsync(tarefa);

        // Status 204 No Content para indicar sucesso na atualização
        return NoContent();
    }

    // DELETE api/tarefas/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveTarefa(int id)
    {
        var tarefa = await _repository.GetByIdAsync(id);
        if (tarefa == null)
        {
            return NotFound($"Tarefa com ID {id} não encontrada.");
        }

        await _repository.DeleteAsync(id);

        // Status 204 No Content para indicar sucesso na remoção
        return NoContent();
    }
}