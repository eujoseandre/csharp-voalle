using System;
using System.Diagnostics;
using Individuos;
using Servicos;
using Cores;

namespace Interface
{

    class Tela : TelaBase
    {

        public Cliente NovoCliente = new Cliente();
        public static int Tentativas = 3;

        public Tela()
        {

        }

        #region MENU
        public void Executar()
        {
            Principal("BEM-VINDOS(A) A PAPERHOUSE PAPELARIA!");
            Console.WriteLine("Cadastre-se para obter acesso ao nossos serviços.");

            Console.WriteLine("\nPrimeiro, digite seu nome");
            Entrada();
            NovoCliente.Nome = Console.ReadLine();

            Console.WriteLine();

            while (Tentativas > 0)
            {
                try
                {
                    Console.WriteLine("Agora o seu CPF");
                    Entrada();
                    long Cpf = Convert.ToInt64(Console.ReadLine());
                    string StringCpf = Convert.ToString(Cpf);

                    if (StringCpf.Length == 11)
                    {
                        Tentativas = 3;
                        NovoCliente.Cpf = StringCpf;
                        MensagemValida("CPF Válido!");
                        break;
                    }
                    else
                    {
                        MensagemErro("Quantidade de caracteres inválida.");
                        Tentativas--;
                    }
                }
                catch (Exception)
                {
                    MensagemErro("Erro Inesperado!");
                    Tentativas--;
                }

                if (Tentativas == 0)
                {
                    ExcessoDeErros();
                }
            }
            Menu();
        }

        public void Menu()
        {
            Console.WriteLine($"Bem-vindo(a) { NovoCliente.Nome.ToUpper() }! Agora você pode fazer seu Pedido :)\n");

            string OpcaoServico = string.Empty;

            while (Tentativas > 0)
            {
                Console.WriteLine("1 - Fotocópia\n2 - Impressão");
                Entrada();
                OpcaoServico = Console.ReadLine();

                if (OpcaoServico.Equals("1") || OpcaoServico.Equals("2"))
                {
                    break;
                }
                else
                {
                    MensagemErro("Opção inválida!");
                    Tentativas--;

                    if (Tentativas == 0)
                    {
                        ExcessoDeErros();
                    }
                }
            }

            if (OpcaoServico.Equals("1"))
            {
                Tentativas = 3;
                Fotocopia();
            }
            else
            {
                Tentativas = 3;
                Impressao();
            }
        }
        #endregion

        #region SERVICOS
        public void Fotocopia()
        {
            var novaFotocopia = new Fotocopia();

            Console.Clear();

            TituloServico("SERVIÇO DE FOTOCÓPIA");

            while (Tentativas > 0)
            {
                Console.WriteLine("\nEscolha uma coloração");
                Console.WriteLine("\n1 - Preto e Branco\n2 - Colorido");
                Entrada();
                string OpcaoServico = Console.ReadLine();

                if (OpcaoServico.Equals("1"))
                {
                    Tentativas = 3;
                    MensagemValida("Você escolheu: Preto e Branco");
                    novaFotocopia.CodigoCor = EnumCores.PretoEBranco;
                    break;
                }
                else if (OpcaoServico.Equals("2"))
                {
                    Tentativas = 3;
                    MensagemValida("Você escolheu: Colorido");
                    novaFotocopia.CodigoCor = EnumCores.Colorido;
                    break;
                }
                else
                {
                    MensagemErro("Opção inválida!");
                    Tentativas--;

                    if (Tentativas == 0)
                    {
                        ExcessoDeErros();
                    }
                }
            }
            Quantidade(novaFotocopia);
        }

        public void Impressao()
        {
            var novaImpressao = new Impressao();

            Console.Clear();

            TituloServico("SERVIÇO DE IMPRESSÃO");

            while (Tentativas > 0)
            {
                Console.WriteLine("\nEscolha uma coloração");
                Console.WriteLine("\n1 - Preto e Branco\n2 - Colorido");
                Entrada();
                string OpcaoServico = Console.ReadLine();

                if (OpcaoServico.Equals("1"))
                {
                    Tentativas = 3;
                    MensagemValida("Você escolheu: Preto e Branco");
                    novaImpressao.CodigoCor = EnumCores.PretoEBranco;
                    break;
                }
                else if (OpcaoServico.Equals("2"))
                {
                    Tentativas = 3;
                    MensagemValida("Você escolheu: Colorido");
                    novaImpressao.CodigoCor = EnumCores.Colorido;
                    break;
                }
                else
                {
                    MensagemErro("Opção inválida!");
                    Tentativas--;

                    if (Tentativas == 0)
                    {
                        ExcessoDeErros();
                    }
                }
            }
            Quantidade(novaImpressao);
        }
        #endregion

        #region QUANTIDADES
        public void Quantidade(Servico service)
        {
            TituloServico("QUANTIDADE");

            while (Tentativas > 0)
            {
                try
                {
                    Console.WriteLine("\nNúmero de cópias");
                    Entrada();
                    service.NumeroCopias = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\nNúmero de páginas");
                    Entrada();
                    service.NumeroPaginas = Convert.ToInt32(Console.ReadLine());

                    if (service.NumeroCopias <= 0)
                    {
                        service.NumeroCopias = 0;
                        service.NumeroPaginas = 0;
                    }
                    else if (service.NumeroPaginas <= 0)
                    {
                        service.NumeroPaginas = 0;
                    }

                    Tentativas = 3;
                    break;
                }
                catch (Exception)
                {
                    MensagemErro("Erro Inesperado! Insira os dados novamente.");
                    Tentativas--;

                    if (Tentativas == 0)
                    {
                        ExcessoDeErros();
                    }
                }
            }
            if (service.NomeServico.Equals("Impressão"))
                ImpressaoAdicionais(service);
            else
                Encadernacao(service, "Fotos");
        }

        public void ImpressaoAdicionais(Servico service)
        {
            Console.WriteLine();
            TituloServico("INFORMAÇÃO ADICIONAL PARA IMPRESSÃO");
            Console.WriteLine();

            int v1 = 0, v2 = 0;

            if (service.NumeroPaginas > 1 && service.NumeroCopias > 0)
            {
                while (Tentativas > 0)
                {
                    try
                    {
                        Console.WriteLine($"Digite o Intervalo de Impressão: [1 - {service.NumeroPaginas}]");
                        Entrada();
                        string Intervalo = Console.ReadLine().Replace(" ", "");
                        string[] Campos = Intervalo.Split('-');

                        v1 = Convert.ToInt32(Campos[0]);
                        v2 = Convert.ToInt32(Campos[1]);

                        if (v1 > 0 && v1 <= service.NumeroPaginas && v2 >= v1 && v2 <= service.NumeroPaginas)
                        {
                            break;
                        }
                        else
                        {
                            MensagemErro("Erro na inserção dos valores!");
                            Tentativas--;
                        }
                    }
                    catch (Exception)
                    {
                        MensagemErro("Erro Inesperado! Insira os dados novamente.");
                        Tentativas--;
                    }
                    if (Tentativas == 0)
                    {
                        ExcessoDeErros();
                    }
                }

                MensagemValida("Intervalo de Impressão: de " + v1 + " até " + v2);

                int Paginas = 1;
                for (int i = v1; i < v2; i++)
                {
                    Paginas++;
                }
                service.NumeroPaginas = Paginas;
            }
            else
            {
                MensagemValida("Para esse serviço, não se aplica INTERVALO DE IMPRESSÃO.");
            }

            Console.WriteLine("Nome do Arquivo: ");
            Entrada();
            String NomeArquivo = Console.ReadLine();
            Console.WriteLine("");

            Tentativas = 3;
            Encadernacao(service, NomeArquivo);
        }

        public void Encadernacao(Servico service, string nome)
        {
            Console.Clear();

            TituloServico("SERVIÇO DE ENCADERNAÇÃO");

            while (Tentativas > 0)
            {
                Console.WriteLine("\nGostaria de Encardenar?");
                Console.WriteLine("\n1 - Sim\n2 - Não");
                Entrada();
                string OpcaoServico = Console.ReadLine();

                if (OpcaoServico.Equals("1"))
                {
                    Tentativas = 3;
                    service.Encardenado = true;
                    break;
                }
                else if (OpcaoServico.Equals("2"))
                {
                    Tentativas = 3;
                    service.Encardenado = false;
                    break;
                }
                else
                {
                    MensagemErro("Opção Inválida!");
                    Tentativas--;

                    if (Tentativas == 0)
                    {
                        ExcessoDeErros();
                    }
                }
            }
            Pedido(service, nome);
        }
        #endregion

        #region RESUMO PEDIDOS
        public void Pedido(Servico Service, string nome)
        {
            Console.Clear();

            Principal("RESUMO DO PEDIDO");
            Console.WriteLine("\nDADOS DO CLIENTE:\n" + NovoCliente.RetornaDados());
            Console.WriteLine("\nDADOS DO PEDIDO:");
            Console.WriteLine("Serviço...............: {0, 15}", Service.NomeServico);
            Console.WriteLine("Tipo de Tinta.........: {0, 15}", Service.CodigoCor);
            Console.WriteLine("Valor da Coloração....: {0, 15:c}", Service.ValorColoracao());
            Console.WriteLine("Valor Encadernação....: {0, 15:c}", Service.Encadernacao());
            Console.WriteLine("Nº de cópias..........: {0, 15}", Service.NumeroCopias);
            Console.WriteLine("Nº de páginas.........: {0, 15}", Service.NumeroPaginas);
            Console.WriteLine("Nome do Arquivo.......: {0, 15}", nome);
            Console.WriteLine("TOTAL.................: {0, 15:c}", Service.Total());


            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(@"C:\Projetos\DesafioFinalC#\NotaFiscal.txt", true))
            {

                sw.WriteLine("\tNOTAL FISCAL");
                sw.WriteLine("\nDADOS DO CLIENTE:\n" + NovoCliente.RetornaDados());
                sw.WriteLine("\nDADOS DO PEDIDO");
                sw.WriteLine("Serviço...............: {0, 15}", Service.NomeServico);
                sw.WriteLine("Tipo de Tinta.........: {0, 15}", Service.CodigoCor);
                sw.WriteLine("Valor da Coloração....: {0, 15:c}", Service.ValorColoracao());
                sw.WriteLine("Valor Encadernação....: {0, 15:c}", Service.Encadernacao());
                sw.WriteLine("Nº de cópias..........: {0, 15}", Service.NumeroCopias);
                sw.WriteLine("Nº de páginas.........: {0, 15}", Service.NumeroPaginas);
                sw.WriteLine("Nome do Arquivo.......: {0, 15}", nome);
                sw.WriteLine("TOTAL.................: {0, 15:c}", Service.Total());
                sw.Close();
            }
        }
        #endregion

        public void ExcessoDeErros()
        {
            Console.Clear();

            TituloServico("Número de tentativas excedido!");

            Console.WriteLine("1 - Reiniciar o Sistema\n2 - Encerrar o Sistema");
            Entrada();

            switch (Console.ReadLine())
            {
                case "1":
                    Tentativas = 3;
                    Executar();
                    break;
                case "2":
                    MensagemErro("Programa Encerrado.");
                    Process.GetCurrentProcess().Kill();
                    break;
                default:
                    MensagemErro("Opção Inválida! O Sistema será encerrado.");
                    Process.GetCurrentProcess().Kill();
                    break;
            }
        }
    }
}