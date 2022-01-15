using Microsoft.AspNetCore.Mvc;
using MyTODOList.Entities.DTOs;
using MyTODOList.Entities.Request;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Services.Services;

namespace MyTODOList.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaService _tarefaService;

        public TarefaController(ITarefaService tarefaService)
        {
            _tarefaService = tarefaService;
        }

        [HttpPost]
        public IActionResult Salvar(TarefaRequest tarefaDto)
        {
            try
            {
                _tarefaService.Salvar(tarefaDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }

            return Created($"/tarefa/{tarefaDto.Id}", tarefaDto);            
        }

        [HttpPut]
        public IActionResult Finalizar([FromBody] int[] ids)
        {
            try
            {
                _tarefaService.Finalizar(ids);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }

            return Ok(ids);
        }

        [HttpDelete]
        public IActionResult Excluir(int id)
        {
            try
            {
                _tarefaService.Excluir(id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }

            return Ok(id);
        }

        [HttpGet]
        public IActionResult ConsultarTodos()
        {
            var tarefas = _tarefaService.ConsultarTodos();

            return Ok(tarefas);
        }
    }
}
