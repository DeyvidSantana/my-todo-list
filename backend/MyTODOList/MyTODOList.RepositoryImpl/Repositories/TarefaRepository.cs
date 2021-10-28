using MyTODOList.Data;
using MyTODOList.Entities.Entities;
using MyTODOList.Repository.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace MyTODOList.RepositoryImpl.Repositories
{
    public class TarefaRepository : Repository<Tarefa>, ITarefaRepository
    {
        public TarefaRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public IList<Tarefa> ConsultarTodos()
        {
            return _dataContext.Tarefas.ToArray();
        }

        public Tarefa ObterPorId(int id)
        {
            return _dataContext.Tarefas.FirstOrDefault(t => t.Id == id);
        }
    }
}
