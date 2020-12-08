using System;
using System.Diagnostics;
using Entities;
using Services;
using Colors;

namespace Interfaces
{

    class ConsoleInterface : ConsoleStyle
    {

        public Client NewClient;
        public static int Attempts { get; set; }

        public ConsoleInterface()
        {
            Attempts = 3;
            NewClient = new Client();
        }

        public void Start()
        {

            MainTitle("BEM-VINDOS(A) A PAPERHOUSE PAPELARIA!");
            Console.WriteLine("Cadastre-se para obter acesso ao nossos serviços.");

            Console.WriteLine("\nPrimeiro, digite seu nome");
            Imput();
            NewClient.Name = Console.ReadLine();

            Console.WriteLine();

            while (Attempts > 0)
            {
                try
                {
                    Console.WriteLine("Agora digite seu CPF");
                    Imput();
                    long Cpf = Int64.Parse(Console.ReadLine());

                    string StringCpf = Convert.ToString(Cpf);

                    if (StringCpf.Length == 11)
                    {
                        Attempts = 3;
                        NewClient.Cpf = StringCpf;
                        ValidMessage($"Tudo certinho { NewClient.Name.ToUpper() }! Seu cadastro foi realizado.");
                        break;
                    }
                    else if (StringCpf.Length < 11) throw new Exception("Limite de caracteres menor que o permitido.");
                    else throw new Exception("Limite de caracteres excedido.");
                }

                catch (FormatException)
                {
                    Attempts--;
                    ErrorMessage("CPF Inválido!");
                }
                catch (Exception ex)
                {
                    Attempts--;
                    ErrorMessage(ex.Message);
                }

                if (Attempts == 0) ErrorOverage();
            }
            Menu();
        }

        public void Menu()
        {

            Console.WriteLine("Escolha o tipo de serviço\n");

            string Option = string.Empty;

            while (Attempts > 0)
            {
                Console.WriteLine("1 - Fotocópia\n2 - Impressão");
                Imput();
                Option = Console.ReadLine();

                if (Option.Equals("1") || Option.Equals("2"))
                {
                    Attempts = 3;
                    break;
                }

                else
                {
                    Attempts--;
                    ErrorMessage("Opção inválida!");

                    if (Attempts == 0) ErrorOverage();
                }
            }

            if (Option.Equals("1")) Photocopy();
            else Printing();
        }

        public void Photocopy()
        {

            var NewPhotocopy = new Photocopy();

            Console.Clear();

            ServiceTitle("SERVIÇO DE FOTOCÓPIA");

            while (Attempts > 0)
            {
                Console.WriteLine("\nEscolha uma coloração");
                Console.WriteLine("\n1 - Preto e Branco\n2 - Colorido");
                Imput();
                string Option = Console.ReadLine();

                if (Option.Equals("1"))
                {
                    Attempts = 3;
                    ValidMessage("Você escolheu: Preto e Branco");
                    NewPhotocopy.Coloring = EnumColors.PretoEBranco;
                    break;
                }
                else if (Option.Equals("2"))
                {
                    Attempts = 3;
                    ValidMessage("Você escolheu: Colorido");
                    NewPhotocopy.Coloring = EnumColors.Colorido;
                    break;
                }
                else
                {
                    Attempts--;
                    ErrorMessage("Opção inválida!");

                    if (Attempts == 0) ErrorOverage();
                }
            }
            Amount(NewPhotocopy);
        }

        public void Printing()
        {

            var NewPrinting = new Printing();

            Console.Clear();

            ServiceTitle("SERVIÇO DE IMPRESSÃO");

            while (Attempts > 0)
            {
                Console.WriteLine("\nEscolha uma coloração");
                Console.WriteLine("\n1 - Preto e Branco\n2 - Colorido");
                Imput();
                string Option = Console.ReadLine();

                if (Option.Equals("1"))
                {
                    Attempts = 3;
                    ValidMessage("Você escolheu: Preto e Branco");
                    NewPrinting.Coloring = EnumColors.PretoEBranco;
                    break;
                }
                else if (Option.Equals("2"))
                {
                    Attempts = 3;
                    ValidMessage("Você escolheu: Colorido");
                    NewPrinting.Coloring = EnumColors.Colorido;
                    break;
                }
                else
                {
                    Attempts--;
                    ErrorMessage("Opção inválida!");

                    if (Attempts == 0) ErrorOverage();
                }
            }
            Amount(NewPrinting);
        }

        public void Amount(Service product)
        {

            ServiceTitle("QUANTIDADE");
            Console.WriteLine();

            while (Attempts > 0)
            {
                try
                {
                    Console.WriteLine("Número de cópias");
                    Imput();
                    product.NumberCopies = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("\nNúmero de páginas");
                    Imput();
                    product.NumberPages = Convert.ToInt32(Console.ReadLine());

                    if (product.NumberCopies <= 0)
                    {
                        product.NumberCopies = 0;
                        product.NumberPages = 0;
                    }
                    else if (product.NumberPages <= 0)
                    {
                        product.NumberPages = 0;
                    }

                    Attempts = 3;
                    break;
                }
                catch (FormatException)
                {
                    Attempts--;
                    ErrorMessage("Entrada de dados incorreta.");

                    if (Attempts == 0) ErrorOverage();
                }
            }

            if (product.ServiceName.Equals("Impressão")) PrintingExtraInfo(product);
            else Binding(product, "Fotos");
        }

        public void PrintingExtraInfo(Service product)
        {
            Console.WriteLine();
            ServiceTitle("INFORMAÇÃO ADICIONAL PARA IMPRESSÃO");
            Console.WriteLine();

            int Starting = 0, Ending = 0;

            if (product.NumberPages > 1 && product.NumberCopies > 0)
            {
                while (Attempts > 0)
                {
                    try
                    {
                        Console.WriteLine($"Digite o Intervalo de Impressão: [1 - { product.NumberPages }]");
                        Imput();
                        string Break = Console.ReadLine().Replace(" ", "");
                        string[] Pieces = Break.Split('-');

                        Starting = Convert.ToInt32(Pieces[0]);
                        Ending = Convert.ToInt32(Pieces[1]);

                        if (Starting > 0 && Starting <= product.NumberPages && Ending >= Starting && Ending <= product.NumberPages)
                            break;

                        else throw new Exception("Os valores não devem ultrapassar o valor previamente estipulado acima.");
                    }
                    catch (FormatException)
                    {
                        ErrorMessage("Entrada de dados incorreta.");
                    }
                    catch (Exception ex)
                    {
                        Attempts--;
                        ErrorMessage(ex.Message);
                    }
                    if (Attempts == 0) ErrorOverage();
                }

                ValidMessage("Intervalo de Impressão: de " + Starting + " até " + Ending);

                int Pages = 1;
                for (int i = Starting; i < Ending; i++)
                {
                    Pages++;
                }
                product.NumberPages = Pages;
            }
            else
            {
                ValidMessage("Para esse serviço, não se aplica INTERVALO DE IMPRESSÃO.");
            }

            Console.WriteLine("Nome do Arquivo: ");
            Imput();
            String FileName = Console.ReadLine().Replace(" ", "_");
            Console.WriteLine("");

            Attempts = 3;
            Binding(product, FileName);
        }

        public void Binding(Service product, string name)
        {

            Console.Clear();

            ServiceTitle("SERVIÇO DE ENCADERNAÇÃO");

            while (Attempts > 0)
            {
                Console.WriteLine("\nGostaria de Encardenar?");
                Console.WriteLine("\n1 - Sim\n2 - Não");
                Imput();
                string Option = Console.ReadLine();

                if (Option.Equals("1"))
                {
                    Attempts = 3;
                    product.Binding = true;
                    break;
                }
                else if (Option.Equals("2"))
                {
                    Attempts = 3;
                    product.Binding = false;
                    break;
                }
                else
                {
                    Attempts--;
                    ErrorMessage("Opção Inválida!");

                    if (Attempts == 0) ErrorOverage();
                }
            }
            Purchase(product, name);
        }

        public void Purchase(Service product, string name)
        {

            MainTitle("RESUMO DO PEDIDO");
            Console.WriteLine("\nDADOS DO CLIENTE:\n" + NewClient.ReturnClientData());
            Console.WriteLine("\nDADOS DO PEDIDO:");
            Console.WriteLine("Serviço...............: {0, 15}", product.ServiceName);
            Console.WriteLine("Tipo de Tinta.........: {0, 15}", product.Coloring);
            Console.WriteLine("Valor da Coloração....: {0, 15:c}", product.ReturnColoringPrice());
            Console.WriteLine("Valor Encadernação....: {0, 15:c}", product.ReturnBinding());
            Console.WriteLine("Nº de cópias..........: {0, 15}", product.NumberCopies);
            Console.WriteLine("Nº de páginas.........: {0, 15}", product.NumberPages);
            Console.WriteLine("Nome do Arquivo.......: {0, 15}", name);
            Console.WriteLine("TOTAL.................: {0, 15:c}", product.Total());

            using (System.IO.StreamWriter sw = new System.IO.StreamWriter($@"C:\wamp64\www\GitHub\csharp-voalle\{ name }.txt", false))
            {

                sw.WriteLine("\tNOTAL FISCAL");
                sw.WriteLine("\nDADOS DO CLIENTE:\n" + NewClient.ReturnClientData());
                sw.WriteLine("\nDADOS DO PEDIDO");
                sw.WriteLine("Serviço...............: {0, 15}", product.ServiceName);
                sw.WriteLine("Tipo de Tinta.........: {0, 15}", product.Coloring);
                sw.WriteLine("Valor da Coloração....: {0, 15:c}", product.ReturnColoringPrice());
                sw.WriteLine("Valor Encadernação....: {0, 15:c}", product.ReturnBinding());
                sw.WriteLine("Nº de cópias..........: {0, 15}", product.NumberCopies);
                sw.WriteLine("Nº de páginas.........: {0, 15}", product.NumberPages);
                sw.WriteLine("Nome do Arquivo.......: {0, 15}", name);
                sw.WriteLine("TOTAL.................: {0, 15:c}", product.Total());
                sw.Close();
            }
            Process.GetCurrentProcess().Kill();
        }

        public void ErrorOverage()
        {

            Console.Clear();

            ServiceTitle("Número de tentativas excedido!");

            Console.WriteLine("\n1 - Reiniciar o Sistema\n2 - Encerrar o Sistema");
            Imput();

            switch (Console.ReadLine())
            {
                case "1":
                    Attempts = 3;
                    Start();
                    break;
                case "2":
                    ErrorMessage("Programa Encerrado.");
                    Process.GetCurrentProcess().Kill();
                    break;
                default:
                    ErrorMessage("Opção Inválida! O Sistema será encerrado.");
                    Process.GetCurrentProcess().Kill();
                    break;
            }
        }
    }
}