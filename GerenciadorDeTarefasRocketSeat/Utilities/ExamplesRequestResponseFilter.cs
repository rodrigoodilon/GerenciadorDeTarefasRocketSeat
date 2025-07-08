using GerenciadorDeTarefasRocketSeat.Communication.Request;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;
using DTO;

namespace GerenciadorDeTarefasRocketSeat.Utilities
{
    public class ExamplesRequestResponseFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var request = operation.RequestBody;

            var requestType = context.MethodInfo.GetParameters().FirstOrDefault()?.ParameterType;

            List<Dictionary<int, OpenApiExample>> openApiExamples = new List<Dictionary<int, OpenApiExample>>();
            int countExample = 0;

            // TaskController
            if (requestType == typeof(TaskRequest))
            {
                openApiExamples = new List<Dictionary<int, OpenApiExample>>()
                {
                new Dictionary<int, OpenApiExample>()
                {
                    {
                        countExample++,
                        new OpenApiExample()
                        {
                            Summary = "Exemplo de Inclusão de livro",
                            Description = "Exemplo completo de inclusão de um novo livro.",
                            Value = new OpenApiString(
                                JsonSerializer.Serialize(new TaskRequest()
                                {
                                    id = "1",
                                    nome = "Exclusão de usuários",
                                    descricao = "Remover usuários onde a data de inatividade seja igual ou superior a um mês.",
                                    prioridade = SysDTO.Priority.Media,
                                    data_inicio = "01/03/2025",
                                    data_fim = "15/03/2025",
                                    status = SysDTO.Status.Aguardando
                                }))
                        }
                    }
                }
                };
            }
            
            foreach (var example in openApiExamples)
            {
                foreach (var kvp in example)
                {
                    request.Content["application/json"].Examples.Add(kvp.Key.ToString(), kvp.Value);
                }
            }
        }
    }
}
