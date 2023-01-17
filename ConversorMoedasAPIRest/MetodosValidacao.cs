using System;
using System.Text.RegularExpressions;

namespace ConversorMoedasAPIRest
{
    public static class MetodosValidacao
    {
        public static bool VerificaSiglaValida(this string siglasValidas, string siglaAValidar)
        {
            return siglasValidas.Contains(siglaAValidar.ToUpper());
        }

        public static bool VerificaOrigem(this string moeda)
        {
            return string.IsNullOrEmpty(moeda);
        }

        public static bool VerificaFormato(this string moeda)
        {
            return Regex.IsMatch(moeda, @"^\w{3}$");
        }

        public static bool VerificaValor(this string valor)
        {
            try
            {
                var aux = double.Parse(valor);
                return true;
            }
            catch (Exception e) { }

            return false;
        }

        public static bool VerificaValorNegativo(this string valor)
        {
            valor = valor.Replace(',', '.');
            double aux = double.Parse(valor);
            return aux > 0;
        }
    }
}
