using DTO;
using System.Data;

namespace DAL;

/// <summary>
/// Camada de acesso a dados responsável por manipular tarefas armazenadas em um <see cref="DataTable"/>.
/// </summary>
public class TaskDAL
{
    /// <summary>
    /// Insere uma nova tarefa na lista de tarefas.
    /// </summary>
    /// <param name="taskDTO">Os dados da tarefa a serem inseridos.</param>
    /// <param name="ListTasks">A tabela onde as tarefas estão armazenadas.</param>
    /// <param name="Id_Identity">O próximo valor de identidade para a tarefa.</param>
    /// <returns><c>true</c> se a inserção for bem-sucedida; caso contrário, <c>false</c>.</returns>
    public bool Insert(TaskDTO taskDTO, DataTable ListTasks, int Id_Identity)
    {
        try
        {
            DataRow row = ListTasks.NewRow();
            row["Nome"] = taskDTO.Nome;
            row["Descricao"] = taskDTO.Descricao;
            row["Prioridade"] = taskDTO.Prioridade;
            row["Data_Inicio"] = taskDTO.Data_Inicio;
            row["Data_Fim"] = taskDTO.Data_Fim;
            row["Status"] = taskDTO.Status;

            Id_Identity++;
            taskDTO.Id = Id_Identity;

            row["Id"] = taskDTO.Id;

            ListTasks.Rows.Add(row);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Atualiza os dados de uma tarefa existente.
    /// </summary>
    /// <param name="taskDTO">Os novos dados da tarefa.</param>
    /// <param name="ListTasks">A tabela onde as tarefas estão armazenadas.</param>
    /// <param name="Id_Identity">O valor atual de identidade (não é modificado aqui).</param>
    /// <returns><c>true</c> se a atualização for bem-sucedida; caso contrário, <c>false</c>.</returns>
    public bool Update(TaskDTO taskDTO, DataTable ListTasks, int Id_Identity)
    {
        try
        {
            DataRow? row = ListTasks.Select().Where(x => Convert.ToInt32(x["Id"]) == taskDTO.Id).FirstOrDefault();

            if (row == null)
            {
                return false;
            }

            row["Nome"] = taskDTO.Nome;
            row["Descricao"] = taskDTO.Descricao;
            row["Prioridade"] = taskDTO.Prioridade;
            row["Data_Inicio"] = taskDTO.Data_Inicio;
            row["Data_Fim"] = taskDTO.Data_Fim;
            row["Status"] = taskDTO.Status;

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Exclui uma tarefa da lista de tarefas.
    /// </summary>
    /// <param name="Id">O identificador da tarefa a ser excluída.</param>
    /// <param name="ListTasks">A tabela onde as tarefas estão armazenadas.</param>
    /// <returns><c>true</c> se a exclusão for bem-sucedida; caso contrário, <c>false</c>.</returns>
    public bool Delete(int Id, DataTable ListTasks)
    {
        try
        {
            DataRow? row = ListTasks.Select().Where(x => Convert.ToInt32(x["Id"]) == Id).FirstOrDefault();

            if (row == null)
            {
                return false;
            }

            ListTasks.Rows.Remove(row);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// Obtém os dados de uma tarefa específica.
    /// </summary>
    /// <param name="Id">O identificador da tarefa.</param>
    /// <param name="ListTasks">A tabela onde as tarefas estão armazenadas.</param>
    /// <returns>
    /// Um objeto <see cref="TaskDTO"/> com os dados da tarefa, ou <c>null</c> se não encontrada.
    /// </returns>
    public TaskDTO? GetTask(int Id, DataTable ListTasks)
    {
        try
        {
            DataRow? row = ListTasks.Select().Where(x => Convert.ToInt32(x["Id"]) == Id).FirstOrDefault();

            if (row == null)
            {
                return null;
            }

            TaskDTO taskDTO = new TaskDTO();

            taskDTO.Id = Convert.ToInt32(row["Id"]);
            taskDTO.Nome = row["Nome"].ToString()!;
            taskDTO.Descricao = row["Descricao"].ToString()!;
            taskDTO.Prioridade = (SysDTO.Priority)Enum.Parse(typeof(SysDTO.Priority), row["Prioridade"].ToString()!);
            taskDTO.Data_Inicio = Convert.ToDateTime(row["Data_Inicio"]);
            taskDTO.Data_Fim = Convert.ToDateTime(row["Data_Fim"]);
            taskDTO.Status = (SysDTO.Status)Enum.Parse(typeof(SysDTO.Status), row["Status"].ToString()!);

            return taskDTO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Obtém a lista completa de tarefas.
    /// </summary>
    /// <param name="ListTasks">A tabela onde as tarefas estão armazenadas.</param>
    /// <returns>Uma lista de objetos <see cref="TaskDTO"/> representando todas as tarefas.</returns>
    public List<TaskDTO> GetAllTasks(DataTable ListTasks)
    {
        try
        {
            List<TaskDTO> listTaskDTO = new List<TaskDTO>();

            foreach (DataRow row in ListTasks.Rows)
            {
                TaskDTO taskDTO = new TaskDTO();

                taskDTO.Id = Convert.ToInt32(row["Id"]);
                taskDTO.Nome = row["Nome"].ToString()!;
                taskDTO.Descricao = row["Descricao"].ToString()!;
                taskDTO.Prioridade = (SysDTO.Priority)Enum.Parse(typeof(SysDTO.Priority), row["Prioridade"].ToString()!);
                taskDTO.Data_Inicio = Convert.ToDateTime(row["Data_Inicio"]);
                taskDTO.Data_Fim = Convert.ToDateTime(row["Data_Fim"]);
                taskDTO.Status = (SysDTO.Status)Enum.Parse(typeof(SysDTO.Status), row["Status"].ToString()!);

                listTaskDTO.Add(taskDTO);
            }

            return listTaskDTO;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
