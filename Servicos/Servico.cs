using Cores;

namespace Servicos
{
    class Servico
    {
        public string NomeServico { get; set; };
        public EnumCores CodigoCor { get; set; }
        public bool Encardenado { get; set; }
        public int NumeroCopias { get; set; };
        public int NumeroPaginas { get; set; };

        public Servico()
        {
            NomeServico = String.Empty;
            NumeroCopias = 0;
            NumeroPaginas = 0;
        }

        public Servico(
            EnumCores codigoCor,
            bool encardenado,
            int numeroCopias,
            int numeroPaginas
            )
        {
            CodigoCor = codigoCor;
            Encardenado = encardenado;
            NumeroCopias = numeroCopias;
            NumeroPaginas = numeroPaginas;
        }

        public double ValorColoracao()
        {
            double ValorColoracao;

            if (CodigoCor.ToString().Equals("PretoEBranco"))
            {
                ValorColoracao = 0.05;
            }
            else
            {
                ValorColoracao = 0.10;
            }
            return ValorColoracao;
        }
        public double Encadernacao()
        {
            if (Encardenado)
                return 2.0;
            else return 0.0;
        }

        public double Total()
        {
            if(NumeroPaginas > 1){
                return Encadernacao()  + (NumeroPaginas * (NumeroCopias * ValorColoracao()));
            }
            else{
                return Encadernacao()  + (NumeroCopias * ValorColoracao());
            }
        }
    }

    class Fotocopia : Servico
    {
        public Fotocopia()
        {
            NomeServico = "Fotocópia";
        }
    }

    class Impressao : Servico
    {
        public Impressao()
        {
            NomeServico = "Impressão";
        }
    }
}
