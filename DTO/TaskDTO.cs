namespace DTO;

/// <summary>
/// Objeto de transferência de dados que representa uma tarefa no sistema.
/// </summary>
public class TaskDTO
{
    /// <summary>
    /// Identificador único da tarefa.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Nome da tarefa.
    /// </summary>
    public string Nome { get; set; } = string.Empty;

    /// <summary>
    /// Descrição detalhada da tarefa.
    /// </summary>
    public string Descricao { get; set; } = string.Empty;

    /// <summary>
    /// Prioridade atribuída à tarefa.
    /// </summary>
    public SysDTO.Priority Prioridade { get; set; }

    /// <summary>
    /// Data de início da tarefa.
    /// </summary>
    public DateTime Data_Inicio { get; set; }

    /// <summary>
    /// Data de término da tarefa.
    /// </summary>
    public DateTime Data_Fim { get; set; }

    /// <summary>
    /// Status atual da tarefa.
    /// </summary>
    public SysDTO.Status Status { get; set; }

    /// <summary>
    /// Operação a ser realizada sobre a tarefa (Inserir, Atualizar ou Excluir).
    /// </summary>
    public SysDTO.Operations Operation { get; set; } = SysDTO.Operations.Insert;
}

