using System;
namespace Interface
{

    class TelaBase
    {

        public TelaBase()
        {

        }

        public void Principal(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"\n\t{ mensagem }\n");
            Console.ResetColor();
        }
        public void TituloServico(string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"> { mensagem }");
            Console.ResetColor();
        }
        public void MensagemValida(string mensagem)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"> { mensagem }\n");
            Console.ResetColor();
        }

        public void MensagemErro(string mensagem)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"> { mensagem }\n");
            Console.ResetColor();
        }

        public void Entrada(){
            Console.Write("\n> ");
        }
    }
}