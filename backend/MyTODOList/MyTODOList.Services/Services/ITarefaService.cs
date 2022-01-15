using MyTODOList.Entities.Request;
using MyTODOList.Entities.Entities;
using System.Collections.Generic;

namespace MyTODOList.Services.Services
{
    public interface ITarefaService : IService
    {
        void Salvar(TarefaRequest request);
        void Excluir(int id);
        IList<Tarefa> ConsultarTodos();
        Tarefa ObterPorId(int id);
        void Finalizar(int[] ids);
    }
}
