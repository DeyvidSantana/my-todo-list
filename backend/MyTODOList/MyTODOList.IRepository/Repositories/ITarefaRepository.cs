using MyTODOList.Entities.Entities;
using System.Collections.Generic;

namespace MyTODOList.Repository.Repositories
{
    public interface ITarefaRepository : IRepository<Tarefa>
    {
        IList<Tarefa> ConsultarTodos();
        IList<Tarefa> ConsultarPorIds(int[] ids);
        Tarefa ObterPorId(int id);
    }
}
