namespace HealtPlus.Models
{
	public class DatosPaciente
	{
		public string? NumPaciente { get; set; }

		public string? Altura { get; set; }

		public string? Peso { get; set; }

		public string? TipoSangre { get; set; }

		public string? RitmoMin { get; set; }

		public string? RitmoMax { get; set; }

		public bool? Estatus { get; set; }

		public string? Nombre { get; set; }

		public string? PrimerApellido { get; set; }

		public string? SegundoApellido { get; set; }

		public DateOnly? FechaNacimiento { get; set; }

		public string? Telefono { get; set; }

		public string? Calle { get; set; }

		public string? Numero { get; set; }

		public string? CodigoPostal { get; set; }

		public string? Colonia { get; set; }

		public int IdEnfermero { get; set; }
	}
}
