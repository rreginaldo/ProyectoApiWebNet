namespace RESTAPI_CORE.Modelos
{
    public class Regla
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public int Dealer { get; set; }
        public int Alternativo { get; set; }
        public decimal Regla1 { get; set; }
        public decimal NOExecede { get; set; }

        public decimal valmin { get; set; }
        public decimal valmax { get; set; }


    }
}
