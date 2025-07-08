using DTO;

namespace GerenciadorDeTarefasRocketSeat.Communication.Response;

/// <summary>
/// Objeto de resposta que representa uma tarefa retornada pela API ou sistema.
/// </summary>
public class TaskResponse
{
    /// <summary>
    /// Identificador único da tarefa.
    /// </summary>
    public int id { get; set; }

    /// <summary>
    /// Nome da tarefa.
    /// </summary>
    public string nome { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada da tarefa.
    /// </summary>
    public string descricao { get; set; } = string.Empty;

    /// <summary>
    /// Prioridade da tarefa.
    /// </summary>
    public SysDTO.Priority priority { get; set; }

    /// <summary>
    /// Data de início da tarefa, no formato de string.
    /// </summary>
    public string data_inicio { get; set; } = string.Empty;

    /// <summary>
    /// Data de término da tarefa, no formato de string.
    /// </summary>
    public string data_fim { get; set; } = string.Empty;

    /// <summary>
    /// Status atual da tarefa.
    /// </summary>
    public SysDTO.Status Status { get; set; }
}

