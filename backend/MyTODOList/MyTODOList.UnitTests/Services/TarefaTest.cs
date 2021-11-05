using Bogus;
using FluentAssertions;
using MyTODOList.Entities.DTOs;
using MyTODOList.Entities.Entities;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Repository.Repositories;
using MyTODOList.Services.Services;
using MyTODOList.ServicesImpl.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using Xunit;

namespace MyTODOList.UnitTests.Services
{
    public class TarefaTest
    {
        private readonly ITarefaRepository _tarefaRepository;

        private readonly ITarefaService _tarefaService;

        public TarefaTest()
        {
            _tarefaRepository = Substitute.For<ITarefaRepository>();

            _tarefaService = new TarefaService(_tarefaRepository);
        }

        [Fact]
        public void Salvar_TarefaNova_SalvarComSucesso()
        {
            // Arrange
            var tarefaDTO = CriarTarefaDto(true);

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
            var tarefaDTO = CriarTarefaDto(false, "Modificado", 1);

            var tarefa = CriarTarefa(false);

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
            var tarefaDTO = CriarTarefaDto(false, id: 1);

            _tarefaRepository.ObterPorId(Arg.Any<int>()).Throws(new NotFoundException());

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _tarefaService.Salvar(tarefaDTO));
        }

        [Fact]
        public void Excluir_TarefaExistente_ExcluirComSucesso()
        {
            // Arrange
            var idTarefa = 1;
            var tarefa = CriarTarefa(true);

            _tarefaRepository.ObterPorId(Arg.Any<int>()).Returns(tarefa);

            // Act            
            _tarefaService.Excluir(idTarefa);

            // Assert
            _tarefaRepository.Received().Delete(Arg.Any<Tarefa>());
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
            _tarefaRepository.ObterPorId(Arg.Any<int>()).Throws(new NotFoundException());

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _tarefaService.ObterPorId(1));
        }

        private TarefaDTO CriarTarefaDto(bool finalizado, string descricao = null, int? id = null)
        {
            return new Faker<TarefaDTO>()
                .RuleFor(t => t.Id, t => id)
                .RuleFor(t => t.Descricao, f => !string.IsNullOrEmpty(descricao) ? descricao : f.Lorem.Text())
                .RuleFor(t => t.Finalizada, f => f.PickRandomParam(finalizado))
                .Generate();
        }

        private Tarefa CriarTarefa(bool finalizado)
        {
            return new Faker<Tarefa>()
                .RuleFor(t => t.Descricao, f => f.Lorem.Text())
                .RuleFor(t => t.Finalizada, f => f.PickRandomParam(finalizado))
                .Generate();
        }
    }
}
