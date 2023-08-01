using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toro.API.Domain.Resources.Result;

public static class ResourceMessage
{
    // general
    public const string ErrorRequired = "Erro {PropertyName}: O campo é obrigatório.";
    public const string ErrorMinLength = "Erro {PropertyName}: O quantidade minima de caracteres não foi atingida.";
    public const string ErrorMaxLength = "Erro {PropertyName}: O quantidade maxima de caracteres foi atingida.";
    public const string ErrorInvalidValue = "Erro {PropertyName}: O valor é inválido.";

    public const string Success = "Sucesso!";
    public const string SuccessCreateItem = "Item cadastrado com sucesso!";

    // Validation
    public const string ErrorExpiredToken = "Erro {PropertyName}: Este token está expirado.";

    public static string InformVariable(string errorMessage, string variable)
    {
        return $"Erro em {variable}: {errorMessage}";
    }

    public static string InformVariable(string errorMessage, string variable, string secVariable)
    {
        return $"Erro em {variable}: {errorMessage} Valor deve ser: {secVariable}";
    }

    public static string InformVariable(string errorMessage, string variable, int secVariable)
    {
        return $"Erro em {variable}: {errorMessage} Valor deve ser: {secVariable}";
    }

    public static string UpdatePropName(this string message, string value)
    {
        return message.Replace("{PropertyName}", value);
    }
}
