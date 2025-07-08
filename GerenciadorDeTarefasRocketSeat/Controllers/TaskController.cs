using BLL;
using DTO;
using GerenciadorDeTarefasRocketSeat.Communication.Request;
using GerenciadorDeTarefasRocketSeat.Communication.Response;
using GerenciadorDeTarefasRocketSeat.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace GerenciadorDeTarefasRocketSeat.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    /// <summary>
    /// Insere uma nova tarefa no sistema com base nos dados fornecidos na requisição.
    /// </summary>
    /// <param name="request">Objeto contendo os dados da tarefa a ser inserida.</param>
    /// <returns>Código de status indicando o resultado da operação.</returns>
    /// <response code="201">Created - Tarefa criada com sucesso.</response>
    /// <response code="400">BadRequest - Dados inválidos para criação da tarefa.</response>
    /// <response code="500">InternalServerError - Erro inesperado ao tentar criar a tarefa.</response>
    [HttpPost]
    [Route("InsertTask")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(object))]
    [ProducesResponseType(typeof(void), StatusCodes.Status500InternalServerError)]
    [Produces("application/json")]
    [Consumes("application/json")]
    [EnableRateLimiting("TaskInsertPolicy")]
    public async Task<ActionResult<object>> InsertTask(TaskRequest request)
    {
        try
        {
            var validationResult = await TaskValidator.Validate(request, SysDTO.Operations.Insert);

            if (validationResult != null)
            {
                return BadRequest(validationResult);
            }

            TaskDTO taskDTO = new TaskDTO();
            taskDTO.Nome = request.nome;
            taskDTO.Descricao = request.descricao.Trim();
            taskDTO.Prioridade = request.prioridade;
            taskDTO.Data_Inicio = Convert.ToDateTime(request.data_inicio);
            taskDTO.Data_Fim = Convert.ToDateTime(request.data_fim);
            taskDTO.Status = request.status;
            taskDTO.Operation = SysDTO.Operations.Insert;

            TaskBLL taskBLL = new TaskBLL();
            
            if (taskBLL.Register(taskDTO))
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }   
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Atualiza uma tarefa existente no sistema com base nos dados fornecidos na requisição.
    /// </summary>
    /// <param name="request">Objeto contendo os dados atualizados da tarefa.</param>
    /// <returns>Código de status indicando o resultado da operação.</returns>
    /// <response code="201">Created - Tarefa atualizada com sucesso.</response>
    /// <response code="400">BadRequest - Dados inválidos para atualização da tarefa.</response>
    /// <response code="500">InternalServerError - Erro inesperado ao tentar atualizar a tarefa.</response>
    [HttpPut]
    [Route("UpdateTask")]
    public async Task<ActionResult<object>> UpdateTask([FromBody] TaskRequest request)
    {
        try
        {
            var validationResult = await TaskValidator.Validate(request, SysDTO.Operations.Update);

            if (validationResult != null)
            {
                return BadRequest(validationResult);
            }

            TaskDTO taskDTO = new TaskDTO();
            taskDTO.Id = Convert.ToInt32(request.id);
            taskDTO.Nome = request.nome;
            taskDTO.Descricao = request.descricao.Trim();
            taskDTO.Prioridade = request.prioridade;
            taskDTO.Data_Inicio = Convert.ToDateTime(request.data_inicio);
            taskDTO.Data_Fim = Convert.ToDateTime(request.data_fim);
            taskDTO.Status = request.status;
            taskDTO.Operation = SysDTO.Operations.Update;

            TaskBLL taskBLL = new TaskBLL();

            if (taskBLL.Register(taskDTO))
            {
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Exclui uma tarefa existente no sistema com base no identificador fornecido.
    /// </summary>
    /// <param name="Id">Identificador da tarefa a ser excluída.</param>
    /// <returns>Código de status indicando o resultado da operação.</returns>
    /// <response code="200">OK - Tarefa excluída com sucesso.</response>
    /// <response code="404">NotFound - Nenhuma tarefa encontrada para o identificador informado.</response>
    /// <response code="500">InternalServerError - Erro inesperado ao tentar excluir a tarefa.</response>
    [HttpDelete]
    [Route("DeleteTask{Id}")]
    public IActionResult DeleteTask([Required][FromRoute] int Id)
    {
        try
        {
            TaskBLL taskBLL = new TaskBLL();

            if (!taskBLL.Delete(Id))
            {
                return NotFound($"Não foi possível localizar a tarefa para o id informado.");
            }

            return Ok(new { status = 200, message = "Tarefa excluída com sucesso." });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retorna os detalhes de uma tarefa específica com base no identificador fornecido.
    /// </summary>
    /// <param name="Id">Identificador da tarefa a ser consultada.</param>
    /// <returns>Objeto contendo os dados da tarefa ou mensagem de erro caso não encontrada.</returns>
    /// <response code="200">OK - Tarefa encontrada e retornada com sucesso.</response>
    /// <response code="404">NotFound - Nenhuma tarefa encontrada para o identificador informado.</response>
    /// <response code="500">InternalServerError - Erro inesperado ao tentar buscar a tarefa.</response>
    [HttpGet]
    [Route("GetTask{Id}")]
    public async Task<ActionResult<object>> GetTask([Required][FromRoute] int Id)
    {
        try
        {
            TaskResponse? Response = new TaskResponse();

            TaskBLL taskBLL = new TaskBLL();

            TaskDTO? taskDTO = new TaskDTO();

            taskDTO = taskBLL.GetTask(Id);

            if (taskDTO == null)
            {
                return NotFound($"Não foi possível localizar a tarefa para o id informado.");
            }

            Response.id = taskDTO.Id;
            Response.nome = taskDTO.Nome;
            Response.descricao = taskDTO.Descricao;
            Response.priority = taskDTO.Prioridade;
            Response.data_inicio = string.Format("{0:dd/MM/yyyy}", taskDTO.Data_Inicio);
            Response.data_fim = string.Format("{0:dd/MM/yyyy}", taskDTO.Data_Fim);
            Response.Status = taskDTO.Status;

            return Ok(new { status = 200, Task = Response });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Retorna a lista de todas as tarefas registradas no sistema.
    /// </summary>
    /// <returns>Lista de tarefas ou mensagem de erro caso nenhuma tarefa esteja registrada.</returns>
    /// <response code="200">OK - Lista de tarefas retornada com sucesso.</response>
    /// <response code="404">NotFound - Nenhuma tarefa registrada no sistema.</response>
    /// <response code="500">InternalServerError - Erro inesperado ao tentar buscar as tarefas.</response>
    [HttpGet]
    [Route("GetAllTasks")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public async Task<ActionResult<object>> GetAllTasks()
    {
        try
        {
            List<TaskResponse> Response = new List<TaskResponse>();

            TaskBLL taskBLL = new TaskBLL();
            List<TaskDTO> listTaskDTO = new List<TaskDTO>();
            listTaskDTO = taskBLL.GetAllTasks();

            if (listTaskDTO.Count == 0)
            {
                return NotFound($"Não existem tarefas registradas no sistema.");
            }

            foreach (TaskDTO task in listTaskDTO)
            {
                TaskResponse item = new TaskResponse();

                item.id = task.Id;
                item.nome = task.Nome;
                item.descricao = task.Descricao;
                item.priority = task.Prioridade;
                item.data_inicio = string.Format("{0:dd/MM/yyyy}", task.Data_Inicio);
                item.data_fim = string.Format("{0:dd/MM/yyyy}", task.Data_Fim);
                item.Status = task.Status;
                Response.Add(item);
            }

            return Ok(new { status = 200, Task = Response });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
