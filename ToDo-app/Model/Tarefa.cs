namespace ToDo_app.Model
{
    public class Tarefa
    {
        public int Id { get; set; }
        public required string titulo { get; set; }
        public string? descricao { get; set; }
        public DateTime dataCriacao { get; set; }
        public DateTime? dataConclusao { get; set; }
        public bool concluida { get; set; }
    }
}
