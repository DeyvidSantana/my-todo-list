using Microsoft.AspNetCore.Builder;
using MyTODOList.WebAPI.APIs;
using MyTODOList.WebAPI.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRegistrationDependencies();

var app = builder.Build();

app.AddConfigurationApp();

app.AddTarefaEndpoints();

app.Run();

