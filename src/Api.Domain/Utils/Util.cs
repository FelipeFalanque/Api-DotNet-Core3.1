namespace Api.Domain.Utils
{
    public static class Util
    {
        public static string ObterNumeroMenorQue10EmDoisDigitos(int numero)
        {
            return numero < 10 ? $"0{numero}" : $"{numero}";
        }
    }
}