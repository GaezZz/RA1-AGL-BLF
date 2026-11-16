
namespace NFL.API.Models
{
    public class Contrato //clase del contrato para cada jugador
    {
        public int IdContrato { get; set; } //get y set para hacer que podamos leer y luego modificar los valores

        public string Player { get; set; } 

        public string Team { get; set; } 

        public string Position { get; set; }

        public bool IsActive { get; set; }

        public int YearSigned { get; set; }

        public int Years { get; set; }

        public decimal Value { get; set; }

        public decimal Guaranteed { get; set; }

        public DateTime DateOfBirth { get; set; }

        public decimal Height { get; set; }

        public decimal Weight { get; set; }

        public int? DraftOverall { get; set; }
    }
}
