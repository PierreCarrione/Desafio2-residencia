using System;
using System.Text.Json;

namespace ConversorMoedasAPIRest
{
    public class Controller
    {
        private DadosView dadosView;
        private SiglasValidas siglasValidas;
        private double ValorConvertido { get; set; }
        private double Taxa { get; set; }

        public Controller()
        {
            dadosView = new DadosView();
            siglasValidas = new SiglasValidas();
        }

        public void run()
        {
            Console.Title = "Conversor de Moedas";
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            var variavel = dadosView.LerDados(siglasValidas.Siglas);

            while (variavel)
            {
                string complementoURL = "convert?from=" + dadosView.MoedaOrigem + "&to=" + dadosView.MoedaDestino + "&amount=" + dadosView.Valor.ToString();

                try
                {
                    var result = ReceberRequisicao(complementoURL);
                    ValorConvertido = result.GetProperty("result").Deserialize<Double>();//Retorna o valor da propriedade "result" no Json recebido do site e converte para double
                    Taxa = result.GetProperty("info").GetProperty("rate").Deserialize<Double>();//Retorna o valor da subpropriedade "rate" da propriedade "info" no Json recebido do site e converte para double
                    MostrarConversao(dadosView);
                    Console.WriteLine("");
                }
                catch (JsonException)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Não foi possível fazer a solicitação para o servidor.Tente novamente.");
                    Console.WriteLine("");
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Não foi possível fazer a conversão do arquivo Json.Tente novamente.");
                    Console.WriteLine("");
                }
                catch (Exception)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Não foi possível fazer a conversão, o link pode estar quebrado ou a conexão foi fechada de forma inesperada.Tente novamente.");
                    Console.WriteLine("");
                }

                variavel = dadosView.LerDados(siglasValidas.Siglas);
            }
        } 

        public JsonElement ReceberRequisicao(string url)
        {
            JsonElement root;

            using (var client = new HttpClient())
            {
                var conexao = new Uri("https://api.exchangerate.host/" + url);
                var result = client.GetAsync(conexao).Result;
                var json = result.Content.ReadAsStringAsync().Result;
                JsonDocument info = JsonDocument.Parse(json);//Converte essa string(Json) para o formato JsonDocument para que possa extrair as informações dele sem a necessidade
                                                                //de instanciar uma classe.
                root = info.RootElement;//Retornando os valores do Json

            }
            return root;
        }

        public void MostrarConversao(DadosView dados)
        {
            Console.WriteLine("");
            Console.WriteLine("{0} {1} => {2} {3:f2}", dados.MoedaOrigem.ToUpper(), dados.Valor.ToString().Replace('.',','), dados.MoedaDestino.ToUpper(), 
                                                        ValorConvertido.ToString().Replace('.',','));
            Console.WriteLine("Taxa: {0}", Taxa.ToString().Replace('.', ','));
        }
    }
}
