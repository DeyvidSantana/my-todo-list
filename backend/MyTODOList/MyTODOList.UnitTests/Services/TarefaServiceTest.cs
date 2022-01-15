using Bogus;
using FluentAssertions;
using MyTODOList.Entities.Request;
using MyTODOList.Entities.Entities;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Repository.Repositories;
using MyTODOList.Services.Services;
using MyTODOList.ServicesImpl.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;
using System.Collections.Generic;

namespace MyTODOList.UnitTests.Services
{
    public class TarefaServiceTest
    {
        private readonly ITarefaRepository _tarefaRepository;

        private readonly IValidacaoTarefaService _validacaoTarefaService;
        private readonly ITarefaService _tarefaService;

        public TarefaServiceTest()
        {
            _tarefaRepository = Substitute.For<ITarefaRepository>();

            _validacaoTarefaService = Substitute.For<IValidacaoTarefaService>();
            _tarefaService = new TarefaService(_tarefaRepository, _validacaoTarefaService);
        }

        [Fact]
        public void Salvar_TarefaNova_SalvarComSucesso()
        {
            // Arrange
            TarefaRequest tarefaDTO = CriarTarefaRequest(true);

            _tarefaRepository.Add(Arg.Any<Tarefa>());

            // Act
            _tarefaService.Salvar(tarefaDTO);

            // Assert
            _tarefaRepository.Received().Add(Arg.Any<Tarefa>());
            _tarefaRepository.Received().SaveChanges();
        }        

        [Fact]
        public void Salvar_TarefaModificadaExistente_SalvarComSucesso()
        {
            // Arrange
            var idTarefa = 1;
            TarefaRequest tarefaDTO = CriarTarefaRequest(false, "Modificado", idTarefa);
            Tarefa tarefa = CriarTarefa(idTarefa, false);

            _tarefaRepository.ObterPorId(Arg.Any<int>()).Returns(tarefa);
            _tarefaRepository.Update(Arg.Any<Tarefa>());

            // Act
            _tarefaService.Salvar(tarefaDTO);

            // Assert
            tarefa.Descricao.Should().BeEquivalentTo("Modificado");
            tarefa.Finalizada.Should().Be(false);

            _tarefaRepository.Received().Update(Arg.Any<Tarefa>());
            _tarefaRepository.Received().SaveChanges();
        }        

        [Fact]
        public void Salvar_TarefaModificadaInexistente_LancarExcecao()
        {
            // Arrange
            TarefaRequest tarefaDTO = CriarTarefaRequest(false, id: 1);

            _tarefaRepository.ObterPorId(Arg.Any<int>()).Throws(new NotFoundException());

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _tarefaService.Salvar(tarefaDTO));
        }

        [Fact]
        public void Excluir_TarefaExistente_ExcluirComSucesso()
        {
            // Arrange
            var idTarefa = 1;
            Tarefa tarefa = CriarTarefa(idTarefa, true);

            _tarefaRepository.ObterPorId(Arg.Any<int>()).Returns(tarefa);

            // Act            
            _tarefaService.Excluir(idTarefa);

            // Assert
            _tarefaRepository.Received().Delete(Arg.Any<Tarefa>());
            _tarefaRepository.Received().SaveChanges();
        }

        [Fact]
        public void Finalizar_TarefasParaFinalizar_OperacaoComSucesso()
        {
            // Arrange
            int[] ids = new int[] { 1, 2 };
            var tarefas = new List<Tarefa>
            {
                CriarTarefa(1, true),
                CriarTarefa(2, true)
            };

            _tarefaRepository.ConsultarPorIds(ids).Returns(tarefas);

            // Act            
            _tarefaService.Finalizar(ids);

            // Arrange
            _tarefaRepository.Received().Update(Arg.Any<Tarefa>());
            _tarefaRepository.Received().SaveChanges();
        }

        [Fact]
        public void Excluir_TaferaInexistente_LancarExcecao()
        {
            // Arrange
            _tarefaRepository.ObterPorId(Arg.Any<int>()).Throws(new NotFoundException());

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _tarefaService.Excluir(1));
        }

        [Fact]
        public void ObterPorId_TarefaInexistente_LancarExcecao()
        {
            // Arrange
            _validacaoTarefaService
                .When(v => v.ValidarExistenciaTarefa(Arg.Any<Tarefa>()))
                .Do(v => { throw new NotFoundException(); });

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _tarefaService.ObterPorId(1));
        }

        private TarefaRequest CriarTarefaRequest(bool finalizado, string descricao = null, int? id = null)
        {
            return new Faker<TarefaRequest>()
                .RuleFor(t => t.Id, t => id)
                .RuleFor(t => t.Descricao, f => !string.IsNullOrEmpty(descricao) ? descricao : f.Lorem.Text())
                .RuleFor(t => t.Finalizada, f => f.PickRandomParam(finalizado))
                .Generate();
        }

        private Tarefa CriarTarefa(int id, bool finalizado)
        {
            return new Faker<Tarefa>()
                .RuleFor(t => t.Id, t => id)
                .RuleFor(t => t.Descricao, f => f.Lorem.Text())
                .RuleFor(t => t.Finalizada, f => f.PickRandomParam(finalizado))
                .Generate();
        }
    }
}
