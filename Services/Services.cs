using Colors;

namespace Services
{

    class Service
    {

        public string ServiceName { get; set; }
        public EnumColors Coloring { get; set; }
        public bool Binding { get; set; }
        public int NumberCopies { get; set; }
        public int NumberPages { get; set; }

        public Service()
        {

        }

        public Service(
            EnumColors coloring,
            bool binding,
            int numberCopies,
            int numberPages
            )
        {
            Coloring = coloring;
            Binding = binding;
            NumberCopies = numberCopies;
            NumberPages = numberPages;
        }

        public double ReturnColoringPrice()
        {

            double ColoringPrice;

            if (Coloring.ToString().Equals("PretoEBranco"))
                ColoringPrice = 0.05;
            else
                ColoringPrice = 0.10;
            return ColoringPrice;
        }
        public double ReturnBinding()
        {
            if (Binding)
                return 2.0;
            else
                return 0.0;
        }

        public double Total()
        {

            if (NumberPages > 1)
                return ReturnBinding() + (NumberPages * (NumberCopies * ReturnColoringPrice()));
            else
                return ReturnBinding() + (NumberCopies * ReturnColoringPrice());
        }
    }

    class Photocopy : Service
    {
        public Photocopy()
        {
            ServiceName = "Fotocópia";
        }
    }

    class Printing : Service
    {
        public Printing()
        {
            ServiceName = "Impressão";
        }
    }
}