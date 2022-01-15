using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MyTODOList.Entities.Entities;
using MyTODOList.Entities.Request;
using MyTODOList.Infra.Exceptions;
using MyTODOList.Services.Services;
using System.Collections.Generic;

namespace MyTODOList.WebAPI.APIs
{
    public static class TarefaAPIExtensions
    {
        public static void AddTarefaEndpoints(this WebApplication app)
        {
            app.MapGet("/v1/consultarTodos", (ITarefaService _tarefaService) => {
                IList<Tarefa> tarefas = _tarefaService.ConsultarTodos();

                return Results.Ok(tarefas);
            });

            app.MapPost("/v1/salvar", (ITarefaService _tarefaService, TarefaRequest request) =>
            {
                try
                {
                    _tarefaService.Salvar(request);
                }
                catch (NotFoundException ex)
                {
                    return Results.NotFound(ex);
                }

                return Results.Created($"/tarefa/{request.Id}", request);
            });

            app.MapPut("/v1/finalizar", (ITarefaService _tarefaService, int[] ids) =>
            {
                try
                {
                    _tarefaService.Finalizar(ids);
                }
                catch (NotFoundException ex)
                {
                    return Results.NotFound(ex);
                }

                return Results.Ok(ids);
            });

            app.MapDelete("/v1/excluir", (ITarefaService _tarefaService, int id) =>
            {
                try
                {
                    _tarefaService.Excluir(id);
                }
                catch (NotFoundException ex)
                {
                    return Results.NotFound(ex);
                }

                return Results.Ok(id);
            });
        }
    }
}
