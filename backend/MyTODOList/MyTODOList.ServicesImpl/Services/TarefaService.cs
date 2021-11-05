using MyTODOList.Entities.DTOs;
using MyTODOList.Entities.Entities;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Repository.Repositories;
using MyTODOList.Services.Services;
using System.Collections.Generic;

namespace MyTODOList.ServicesImpl.Services
{
    public class TarefaService : Service, ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaService(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public void Salvar(TarefaDTO tarefaDto)
        {
            if(!tarefaDto.Id.HasValue)
            {
                var novaTarefa = new Tarefa
                {
                    Descricao = tarefaDto.Descricao,
                    Finalizada = tarefaDto.Finalizada
                };

                _tarefaRepository.Add(novaTarefa);
            } 
            else
            {
                var tarefa = _tarefaRepository.ObterPorId(tarefaDto.Id.Value);

                ValidarExistenciaTarefa(tarefa);

                tarefa.Descricao = tarefaDto.Descricao;
                tarefa.Finalizada = tarefaDto.Finalizada;

                _tarefaRepository.Update(tarefa);
            }

            _tarefaRepository.SaveChanges();
        }

        public void Finalizar(int[] ids)
        {
            var tarefas = _tarefaRepository.ConsultarPorIds(ids);

            foreach (var tarefa in tarefas)
            {
                tarefa.Finalizada = true;
                _tarefaRepository.Update(tarefa);
            }

            _tarefaRepository.SaveChanges();
        }

        public void Excluir(int id)
        {
            var tarefa = _tarefaRepository.ObterPorId(id);
            
            ValidarExistenciaTarefa(tarefa);

            _tarefaRepository.Delete(tarefa);
            _tarefaRepository.SaveChanges();
        }        

        public IList<Tarefa> ConsultarTodos()
        {
            return _tarefaRepository.ConsultarTodos();
        }

        public Tarefa ObterPorId(int id)
        {
            var tarefa = _tarefaRepository.ObterPorId(id);

            ValidarExistenciaTarefa(tarefa);

            return tarefa;
        }

        private void ValidarExistenciaTarefa(Tarefa tarefa)
        {
            if (tarefa is null)
            {
                throw new NotFoundException("Tarefa não encontrada!");
            }
        }        
    }
}
