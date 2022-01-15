using MyTODOList.Entities.Entities;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Services.Services;

namespace MyTODOList.ServicesImpl.Services
{
    public class ValidacaoTarefaService : Service, IValidacaoTarefaService
    {
        public void ValidarExistenciaTarefa(Tarefa tarefa)
        {
            if (tarefa is null)
            {
                throw new NotFoundException("Tarefa não encontrada!");
            }
        }
    }
}
