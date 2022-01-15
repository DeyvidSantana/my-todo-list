using MyTODOList.Entities.Entities;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Services.Services;
using MyTODOList.ServicesImpl.Services;
using NSubstitute;
using Xunit;

namespace MyTODOList.UnitTests.Services
{
    public class ValidacaoTarefaServiceTest
    {
        private readonly IValidacaoTarefaService _validacaoTarefaService;

        public ValidacaoTarefaServiceTest()
        {
            _validacaoTarefaService = new ValidacaoTarefaService();
        }

        [Fact]
        public void ValidarExistenciaTarefa_TarefaNaoExistente_LancarExcecao()
        {
            // Arrange
            Substitute.For<IValidacaoTarefaService>()
               .When(v => v.ValidarExistenciaTarefa(null))
               .Do(v => { throw new NotFoundException(); });

            // Act & Assert
            Assert.Throws<NotFoundException>(() => _validacaoTarefaService.ValidarExistenciaTarefa(null));
        }
    }
}
