using BLL;
using DTO;
using GerenciadorDeTarefasRocketSeat.Utilities;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Obtém a versão do assembly da API
Version version = Assembly.GetEntryAssembly()?.GetName().Version!;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    // Aqui adiciona o seu filtro customizado
    options.OperationFilter<ExamplesRequestResponseFilter>();

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Gerenciador de Tarefas - API",
        Version = $"{version}",
        Description = "Gerenciador de Tarefas",

        TermsOfService = new Uri("https://github.com/rodrigoodilon/DesafioRocketSeat1"),
        Contact = new OpenApiContact
        {
            Name = "Rodrigo Odilon Mariano da Silva",
            Email = "rodrigo.odilon@hotmail.com",
        },
        License = new OpenApiLicense
        {
            Name = "MIT License ©️ 2025 Rodrigo Odilon",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
});

/* Configura um rate limiter com política específica para a rota de inserção de tarefas ("TaskInsertPolicy").
 * Permite até 5 requisições por IP a cada 20 segundos, sem permitir fila de espera.
 * Requisições além do limite são imediatamente rejeitadas.*/
builder.Services.AddRateLimiter(options =>
{
    options.AddPolicy("TaskInsertPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromSeconds(20),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Cria as colunas do DataTable para armazenar as tarefas
SysBLL.ListTasks.Columns.Add("Id", typeof(int));
SysBLL.ListTasks.Columns.Add("Nome", typeof(string));
SysBLL.ListTasks.Columns.Add("Descricao", typeof(string));
SysBLL.ListTasks.Columns.Add("Prioridade", typeof(SysDTO.Priority));
SysBLL.ListTasks.Columns.Add("Data_Inicio", typeof(DateTime));
SysBLL.ListTasks.Columns.Add("Data_Fim", typeof(DateTime));
SysBLL.ListTasks.Columns.Add("Status", typeof(SysDTO.Status));


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
