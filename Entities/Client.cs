using System;
namespace Entities
{

    class Client
    {

        public string Name { get; set; }
        public string Cpf { get; set; }


        public Client()
        {

        }

        public Client(string name, string cpf)
        {
            Name = name;
            Cpf = cpf;
        }

        public string ReturnClientData()
        {
            return "Nome..................:  " + Name.ToUpper() +
            "\nCPF...................:  " + Convert.ToUInt64(this.Cpf).ToString(@"000\.000\.000\-00");
        }
    }
}