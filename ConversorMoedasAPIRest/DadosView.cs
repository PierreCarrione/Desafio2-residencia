using System;

namespace ConversorMoedasAPIRest
{
    public class DadosView
    {
        public string MoedaOrigem { get; set; }
        public string MoedaDestino { get; set; }
        public double Valor { get; set; }


        public bool LerDados(string siglasValidas) {

            bool flag = true;
            Console.Write("Moeda origem: ");
            MoedaOrigem = Console.ReadLine();

            //----------------------------------- Input Moeda Origem -----------------------------------\\
            while (flag)
            {
                if (MetodosValidacao.VerificaOrigem(MoedaOrigem))
                {
                    return false;
                }
                else if (!MetodosValidacao.VerificaFormato(MoedaOrigem))
                {
                    Console.WriteLine("Sigla para a moeda deve ter apenas 3 digitos.Digite novamente a sigla.");
                    Console.Write("Moeda origem: ");
                    MoedaOrigem = Console.ReadLine();
                }
                else if (!MetodosValidacao.VerificaSiglaValida(siglasValidas, MoedaOrigem))
                {
                    Console.WriteLine("Sigla para conversão inválida.Digite novamente uma sigla válida.");
                    Console.Write("Moeda origem: ");
                    MoedaOrigem = Console.ReadLine();
                }
                else
                {
                    flag = false;
                }
            }
            //--------------------------------- Fim Input Moeda Origem ---------------------------------\\


            flag = true;
            Console.Write("Moeda destino: ");
            MoedaDestino = Console.ReadLine();

            //----------------------------------- Input Moeda Destino -----------------------------------\\
            while (flag)
            {
                if (VerificaOriDes())
                {
                    Console.WriteLine("Moeda de destino não pode ser igual a de origem.");
                    Console.Write("Moeda destino: ");
                    MoedaDestino = Console.ReadLine();
                }
                else if (!MetodosValidacao.VerificaFormato(MoedaDestino))
                {
                    Console.WriteLine("Sigla para a moeda deve ter apenas 3 digitos.Digite novamente a sigla.");
                    Console.Write("Moeda destino: ");
                    MoedaDestino = Console.ReadLine();
                }
                else if (!MetodosValidacao.VerificaSiglaValida(siglasValidas, MoedaDestino))
                {
                    Console.WriteLine("Sigla para conversão inválida.Digite novamente uma sigla válida.");
                    Console.Write("Moeda destino: ");
                    MoedaDestino = Console.ReadLine();
                }
                else
                {
                    flag = false;
                }
            }
            //--------------------------------- Fim Input Moeda Destino ---------------------------------\\


            flag = true;
            Console.Write("Valor: ");
            string aux;
            aux = Console.ReadLine();

            //--------------------------------------- Input Valor ---------------------------------------\\
            while (flag)
            {
                if (!MetodosValidacao.VerificaValor(aux))
                {
                    Console.WriteLine("Valor para conversão inválido.Só é aceito valores decimais.");
                    Console.Write("Valor: ");
                    aux = Console.ReadLine();
                }
                else if (!MetodosValidacao.VerificaValorNegativo(aux))
                {
                    Console.WriteLine("Valor para conversão deve ser maior que zero.");
                    Console.Write("Valor: ");
                    aux = Console.ReadLine();
                }
                else
                {
                    aux = aux.Replace(',','.');
                    Valor = double.Parse(aux);
                    flag = false;
                }
            }
            //------------------------------------- Fim Input Valor -------------------------------------\\

            return true;
        }

        public bool VerificaOriDes()
        {
            return MoedaOrigem == MoedaDestino;
        }
    }
}
