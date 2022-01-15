using MyTODOList.Entities.Entities;
using MyTODOList.Entities.Request;
using MyTODOList.Repository.Repositories;
using MyTODOList.Services.Services;
using System.Collections.Generic;

namespace MyTODOList.ServicesImpl.Services
{
    public class TarefaService : Service, ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IValidacaoTarefaService _validacaoTarefaService;

        public TarefaService(ITarefaRepository tarefaRepository,
            IValidacaoTarefaService validacaoTarefaService)
        {
            _tarefaRepository = tarefaRepository;
            _validacaoTarefaService = validacaoTarefaService;
        }

        public void Salvar(TarefaRequest request)
        {
            if(!request.Id.HasValue)
            {
                var novaTarefa = new Tarefa
                {
                    Descricao = request.Descricao,
                    Finalizada = request.Finalizada
                };

                _tarefaRepository.Add(novaTarefa);
            } 
            else
            {
                Tarefa tarefa = _tarefaRepository.ObterPorId(request.Id.Value);

                _validacaoTarefaService.ValidarExistenciaTarefa(tarefa);

                tarefa.Descricao = request.Descricao;
                tarefa.Finalizada = request.Finalizada;

                _tarefaRepository.Update(tarefa);
            }

            _tarefaRepository.SaveChanges();
        }

        public void Finalizar(int[] ids)
        {
            IList<Tarefa> tarefas = _tarefaRepository.ConsultarPorIds(ids);

            foreach (Tarefa tarefa in tarefas)
            {
                tarefa.Finalizada = true;
                _tarefaRepository.Update(tarefa);
            }

            _tarefaRepository.SaveChanges();
        }

        public void Excluir(int id)
        {
            Tarefa tarefa = _tarefaRepository.ObterPorId(id);

            _validacaoTarefaService.ValidarExistenciaTarefa(tarefa);

            _tarefaRepository.Delete(tarefa);
            _tarefaRepository.SaveChanges();
        }        

        public IList<Tarefa> ConsultarTodos()
        {
            return _tarefaRepository.ConsultarTodos();
        }

        public Tarefa ObterPorId(int id)
        {
            Tarefa tarefa = _tarefaRepository.ObterPorId(id);

            _validacaoTarefaService.ValidarExistenciaTarefa(tarefa);

            return tarefa;
        }               
    }
}
