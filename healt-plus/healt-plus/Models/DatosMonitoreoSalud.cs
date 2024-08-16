namespace healt_plus.Models
{
	public class DatosMonitoreoSalud
	{
		public int IdMonitoreo { get; set; }

		public int? IdPaciente { get; set; }

		public DateTime? FechaHora { get; set; }

		public int? RitmoCardiaco { get; set; }

		public string? Tipo { get; set; }
	}
}
