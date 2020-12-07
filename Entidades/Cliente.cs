using System;
namespace Individuos
{
    class Cliente
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }


        public Cliente()
        {

        }

        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            Cpf = cpf;
        }

        public string RetornaDados()
        {
            return "Nome..................:  " + Nome.ToUpper() +
            "\nCPF...................:  " + Convert.ToUInt64(this.Cpf).ToString(@"000\.000\.000\-00");
        }
    }
}
