using BLL;
using DTO;
using GerenciadorDeTarefasRocketSeat.Communication.Request;
using System.Drawing;
using static DTO.SysDTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GerenciadorDeTarefasRocketSeat.Validators;

/// <summary>
/// Classe Responsável por validar os dados de uma requisição de tarefa.
/// </summary>
public class TaskValidator
{
    /// <summary>
    /// Valida os dados de uma <see cref="TaskRequest"/> de acordo com a operação especificada.
    /// </summary>
    /// <param name="request">Os dados da requisição da tarefa a serem validados.</param>
    /// <param name="Operation">A operação que está sendo executada.</param>
    /// <returns>
    /// Um objeto representando o resultado da validação ou <c>null</c> caso não haja erros.
    /// </returns>
    public async static Task<object?> Validate(TaskRequest request, SysDTO.Operations Operation)
    {
        try
        {
            if (Operation == SysDTO.Operations.Update && FuncionsBLL.IsNumeric(request.id, false) == false)
            {
                return new { status = 400, message = "Id não informado ou inválido" };
            }

            if (string.IsNullOrEmpty(request.nome?.Trim()))
            {
                return new { status = 400, message = "Nome não informado" };
            }

            if (string.IsNullOrEmpty(request.descricao?.Trim()))
            {
                return new { status = 400, message = "Descriçaõ não informada" };
            }

            if (string.IsNullOrEmpty(request.data_inicio?.Trim()) || FuncionsBLL.IsDate(request.data_inicio) == false)
            {
                return new { status = 400, message = "Data de inicio não informada ou inválida" };
            }

            if (string.IsNullOrEmpty(request.data_fim?.Trim()) || FuncionsBLL.IsDate(request.data_fim) == false)
            {
                return new { status = 400, message = "Data final não informada ou inválida" };
            }
            else if (Convert.ToDateTime(request.data_inicio) > Convert.ToDateTime(request.data_fim))
            {
                return new { status = 400, message = "A Data de inicio não pode ser maior que a data final" };
            }

            if (Enum.IsDefined(typeof(Priority), request.prioridade) == false)
            {
                return new { status = 400, message = "Prioridade não informada ou inválida" };
            }

            if (Enum.IsDefined(typeof(Status), request.status) == false)
            {
                return new { status = 400, message = "Status não informado ou inválido" };
            }

            return null; // Retorne null se todas as validações passarem
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
