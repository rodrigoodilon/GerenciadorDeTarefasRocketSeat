namespace BLL;

public class FuncionsBLL
{
    /// <summary>
    /// Método estendido para verificar se é uma data válida.
    /// </summary>
    /// <param name="date">A data que deverá ser checada</param>
    /// <returns>Retorna true ou false</returns>   
    public static bool IsDate(string date)
    {
        DateTime dt;

        return DateTime.TryParse(date, out dt);
    }

    /// <summary>
    /// Método estendido para retornar se é um número.
    /// </summary>
    /// <param name="Valor">O Valor que será validado</param>
    /// <param name="PermiteNegativo">Se permite ou não numero negativo</param>
    /// <returns>Retorna a boolena true ou false</returns>     
    public static bool IsNumeric(string Valor, bool PermiteNegativo = true)
    {
        decimal resultado;
        if (decimal.TryParse(Valor, out resultado))
        {
            if (resultado < 0 && PermiteNegativo == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
