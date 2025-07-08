namespace DTO;

/// <summary>
/// Contém definições comuns do sistema, como prioridades, status e operações.
/// </summary>
public class SysDTO
{
    /// <summary>
    /// Define os níveis de prioridade para uma tarefa.
    /// </summary>
    public enum Priority
    {
        /// <summary>
        /// Prioridade alta.
        /// </summary>
        Alta = 1,

        /// <summary>
        /// Prioridade média.
        /// </summary>
        Media = 2,

        /// <summary>
        /// Prioridade baixa.
        /// </summary>
        Baixa = 3
    }

    /// <summary>
    /// Define os possíveis status de uma tarefa.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Tarefa concluída.
        /// </summary>
        Concluida = 1,

        /// <summary>
        /// Tarefa em andamento.
        /// </summary>
        Em_Andamento = 2,

        /// <summary>
        /// Tarefa aguardando início ou alguma condição.
        /// </summary>
        Aguardando = 3
    }

    /// <summary>
    /// Define as operações possíveis sobre uma tarefa.
    /// </summary>
    public enum Operations
    {
        /// <summary>
        /// Inserir nova tarefa.
        /// </summary>
        Insert,

        /// <summary>
        /// Atualizar tarefa existente.
        /// </summary>
        Update,

        /// <summary>
        /// Excluir tarefa.
        /// </summary>
        Delete
    }
}
