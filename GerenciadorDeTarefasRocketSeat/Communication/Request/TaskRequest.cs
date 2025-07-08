using DTO;

namespace GerenciadorDeTarefasRocketSeat.Communication.Request;

/// <summary>
/// Representa uma requisição para criar ou atualizar uma tarefa.
/// </summary>
public class TaskRequest
{
    /// <summary>
    /// Identificador único da tarefa.
    /// </summary>
    public string id { get; set; } = string.Empty;

    /// <summary>
    /// Nome da tarefa.
    /// </summary>
    public string nome { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada da tarefa.
    /// </summary>
    public string descricao { get; set; } = string.Empty;

    /// <summary>
    /// Prioridade atribuída à tarefa.
    /// </summary>
    public SysDTO.Priority prioridade { get; set; }

    /// <summary>
    /// Data de início da tarefa (formato string).
    /// </summary>
    public string data_inicio { get; set; } = string.Empty;

    /// <summary>
    /// Data de término da tarefa (formato string).
    /// </summary>
    public string data_fim { get; set; } = string.Empty;

    /// <summary>
    /// Status atual da tarefa.
    /// </summary>
    public SysDTO.Status status { get; set; }
}
