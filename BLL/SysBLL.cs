using System.Data;

namespace BLL;

public class SysBLL
{
    /// <summary>
    /// Tabela que armazena os registros das tarefas.
    /// </summary>
    public static DataTable ListTasks = new DataTable();

    /// <summary>
    /// Identificador incremental para controle de IDs das tarefas.
    /// </summary>
    public static int Id_Task_Identity { get; set; }
}
