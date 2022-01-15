using MyTODOList.Entities.Entities;

namespace MyTODOList.Services.Services
{
    public interface IValidacaoTarefaService : IService
    {
        void ValidarExistenciaTarefa(Tarefa tarefa);
    }
}
