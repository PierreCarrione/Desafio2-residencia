using System;

namespace ConversorMoedasAPIRest
{
    public class SiglasValidas//Classe que servirá para verificação das siglas permitidas.
    {
        public string Siglas { get; private set; }
        private readonly string path = "SiglasMoedas.txt";//As siglas foram retiradas desse site https://pt.iban.com/currency-codes

        public SiglasValidas()
        {
            Siglas = RecuperaSiglas();
        }

        public string RecuperaSiglas()
        {
            string siglas;

            using (FileStream file = File.Open(path, FileMode.Open))
            using (StreamReader sr = new StreamReader(file))
            {
                siglas = sr.ReadToEnd();
            }

            return siglas;
        }
    }
}
