using DAL;
using DTO;

namespace BLL;

/// <summary>
/// Camada de negócios responsável por gerenciar tarefas, delegando operações para a camada de acesso a dados.
/// </summary>
public class TaskBLL
{
    TaskDAL taskDAL = null;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="TaskBLL"/>.
    /// </summary>
    public TaskBLL()
    {
        taskDAL = new TaskDAL();
    }

    /// <summary>
    /// Recupera os dados de uma tarefa específica pelo seu identificador.
    /// </summary>
    /// <param name="Id">O identificador da tarefa.</param>
    /// <returns>Um objeto <see cref="TaskDTO"/> contendo os dados da tarefa.</returns>
    public TaskDTO GetTask(int Id)
    {
        try
        {
            return taskDAL.GetTask(Id, SysBLL.ListTasks);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Recupera a lista de todas as tarefas.
    /// </summary>
    /// <returns>Uma lista de objetos <see cref="TaskDTO"/> com todas as tarefas.</returns>
    public List<TaskDTO> GetAllTasks()
    {
        try
        {
            return taskDAL.GetAllTasks(SysBLL.ListTasks);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Registra uma tarefa no sistema, inserindo ou atualizando conforme a operação especificada.
    /// </summary>
    /// <param name="taskDTO">O objeto <see cref="TaskDTO"/> com os dados da tarefa.</param>
    /// <returns><c>true</c> se a operação for bem-sucedida; caso contrário, lança exceção.</returns>
    /// <exception cref="Exception">Lançada caso a operação seja inválida.</exception>
    public bool Register(TaskDTO taskDTO)
    {
        try
        {
            if (taskDTO.Operation == SysDTO.Operations.Insert)
            {
                taskDAL.Insert(taskDTO, SysBLL.ListTasks, SysBLL.Id_Task_Identity);

                return true;
            }
            else if (taskDTO.Operation == SysDTO.Operations.Update)
            {
                taskDAL.Update(taskDTO, SysBLL.ListTasks, SysBLL.Id_Task_Identity);

                return true;
            }
            else
            {
                throw new Exception("Operação inválida,favor informar uma operação válida!");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Exclui uma tarefa específica pelo seu identificador.
    /// </summary>
    /// <param name="Id">O identificador da tarefa a ser excluída.</param>
    /// <returns><c>true</c> se a exclusão for bem-sucedida; caso contrário, lança exceção.</returns>
    public bool Delete(int Id)
    {
        try
        {
            return taskDAL.Delete(Id, SysBLL.ListTasks);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
