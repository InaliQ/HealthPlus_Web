using Azure.Core;
using healt_plus.Models;
using HealtPlus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealtPlus.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PacienteController : ControllerBase
	{
		private readonly HealtContext _baseDatos;

		public PacienteController(HealtContext baseDatos)
		{
			_baseDatos = baseDatos;
		}

		[HttpGet]
		[Route("getPacientes/{idEnfermero}")]
		public async Task<ActionResult> getPacienteEnfermero(int idEnfermero)
		{
			var pacienteEnfermeroBD = from paciente in _baseDatos.Pacientes
									join pacienteEnfermero in _baseDatos.PacienteEnfermeros
									on paciente.IdPaciente equals pacienteEnfermero.IdPaciente
									join enfermero in _baseDatos.Enfermeros
									on pacienteEnfermero.IdEnfermero equals enfermero.IdEnfermero
									where pacienteEnfermero.IdEnfermero == idEnfermero
									select new
									{
										paciente.IdPaciente,
										paciente.NumPaciente,
										paciente.Altura,
										paciente.Peso,
										paciente.TipoSangre,
										paciente.Estatus,
										paciente.IdPersona,
										paciente.IdPersonaNavigation.Nombre,
										paciente.IdPersonaNavigation.PrimerApellido,
										paciente.IdPersonaNavigation.SegundoApellido,
										enfermero.NumEnfermero,
										enfermero.Titulo
									};
			return Ok(pacienteEnfermeroBD);
		}

		[HttpGet]
		[Route("getPacientes")]
		public async Task<ActionResult> getPacientes()
		{
			var pacientes = from paciente in _baseDatos.Pacientes
							select new
							{
								paciente.IdPaciente,
								paciente.NumPaciente,
								paciente.Altura,
								paciente.Peso,
								paciente.TipoSangre,
								paciente.Estatus,
								paciente.IdPersona,
								paciente.IdPersonaNavigation.Nombre,
								paciente.IdPersonaNavigation.PrimerApellido,
								paciente.IdPersonaNavigation.SegundoApellido,
							};

			return Ok(pacientes);
		}


		[HttpPost]
		[Route("agregarPaciente")]
		public async Task<ActionResult> AgregarPaciente(DatosPaciente paciente)
		{
			using (var transaction = _baseDatos.Database.BeginTransaction())
			{
				try
				{
					var persona = new Persona
					{
						Nombre = paciente.Nombre,
						PrimerApellido = paciente.PrimerApellido,
						SegundoApellido = paciente.SegundoApellido,
						FechaNacimiento = paciente.FechaNacimiento,
						Telefono = paciente.Telefono,
						Calle = paciente.Calle,
						Numero = paciente.Numero,
						CodigoPostal = paciente.CodigoPostal,
						Colonia = paciente.Colonia
					};

					_baseDatos.Personas.Add(persona);
					await _baseDatos.SaveChangesAsync();

					var nuevoPaciente = new Paciente
					{
						NumPaciente = paciente.NumPaciente,
						Altura = paciente.Altura,
						Peso = paciente.Peso,
						TipoSangre = paciente.TipoSangre,
						RitmoMin = paciente.RitmoMin,
						RitmoMax = paciente.RitmoMax,
						Estatus = paciente.Estatus,
						IdPersona = persona.IdPersona
					};

					_baseDatos.Pacientes.Add(nuevoPaciente);
					await _baseDatos.SaveChangesAsync();

					var pacienteEnfermero = new PacienteEnfermero
					{
						IdPaciente = nuevoPaciente.IdPaciente,
						IdEnfermero = paciente.IdEnfermero
					};

					_baseDatos.PacienteEnfermeros.Add(pacienteEnfermero);
					await _baseDatos.SaveChangesAsync();

					transaction.Commit();

					return Ok(nuevoPaciente);
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return StatusCode(500, "Error interno del servidor");
				}
			}
		}
	}
}
