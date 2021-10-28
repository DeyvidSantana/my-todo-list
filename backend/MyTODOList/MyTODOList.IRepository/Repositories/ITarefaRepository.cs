using MyTODOList.Entities.Entities;
using System.Collections.Generic;

namespace MyTODOList.Repository.Repositories
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        IList<Tarefa> ConsultarTodos();
        Tarefa ObterPorId(int id);
    }
}
