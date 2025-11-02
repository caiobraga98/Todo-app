using Microsoft.EntityFrameworkCore;

namespace ToDo_app.Data
{
    public class DataDbContext: DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }
        public DbSet<Model.Tarefa> Tarefas { get; set; }
    }
}
