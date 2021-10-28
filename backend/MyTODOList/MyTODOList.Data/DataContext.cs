using Microsoft.EntityFrameworkCore;
using MyTODOList.Entities.Entities;

namespace MyTODOList.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
