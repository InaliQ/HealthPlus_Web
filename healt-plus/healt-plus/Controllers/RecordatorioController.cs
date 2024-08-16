using healt_plus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealtPlus.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RecordatorioController : ControllerBase
	{
		private readonly HealtContext _baseDatos;

		public RecordatorioController(HealtContext baseDatos)
		{
			_baseDatos = baseDatos;
		}

		[HttpGet]
		[Route("getRecordatorios/{idPaciente}")]
		public async Task<ActionResult> getRecordatorios(int idPaciente)
		{

			var recordatorios = from recordatorioPorTurno in _baseDatos.RecordatorioPorTurnos
								join recordatorio in _baseDatos.Recordatorios
								on recordatorioPorTurno.IdRecordatorio equals recordatorio.IdRecordatorio
								join paciente in _baseDatos.Pacientes
								on recordatorio.IdPaciente equals paciente.IdPaciente
								where paciente.IdPaciente == idPaciente
								select new
								{
									recordatorioPorTurno.IdRecordatorio,
									recordatorioPorTurno.IdEnfermero,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPaciente,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.NumPaciente,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.IdPersonaNavigation.Nombre,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.IdPersonaNavigation.PrimerApellido,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.IdPersonaNavigation.SegundoApellido,
									recordatorioPorTurno.IdRecordatorioNavigation.Medicamento,
									recordatorioPorTurno.IdRecordatorioNavigation.CantidadMedicamento,
									recordatorioPorTurno.IdRecordatorioNavigation.FechaInicio,
									recordatorioPorTurno.IdRecordatorioNavigation.FechaFin,
									recordatorioPorTurno.IdRecordatorioNavigation.Estatus,
							};

			if (recordatorios == null)
			{
				return NotFound();
			}

			return Ok(recordatorios);
		}

		[HttpGet]
		[Route("getRecordatorios")]
		public async Task<ActionResult> getRecordatorios()
		{

			var recordatorios = from recordatorioPorTurno in _baseDatos.RecordatorioPorTurnos
								join recordatorio in _baseDatos.Recordatorios
								on recordatorioPorTurno.IdRecordatorio equals recordatorio.IdRecordatorio
								join paciente in _baseDatos.Pacientes
								on recordatorio.IdPaciente equals paciente.IdPaciente
								select new
								{
									recordatorioPorTurno.IdRecordatorio,
									recordatorioPorTurno.IdEnfermero,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPaciente,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.NumPaciente,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.IdPersonaNavigation.Nombre,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.IdPersonaNavigation.PrimerApellido,
									recordatorioPorTurno.IdRecordatorioNavigation.IdPacienteNavigation.IdPersonaNavigation.SegundoApellido,
									recordatorioPorTurno.IdRecordatorioNavigation.Medicamento,
									recordatorioPorTurno.IdRecordatorioNavigation.CantidadMedicamento,
									recordatorioPorTurno.IdRecordatorioNavigation.FechaInicio,
									recordatorioPorTurno.IdRecordatorioNavigation.FechaFin,
									recordatorioPorTurno.IdRecordatorioNavigation.Estatus,
								};

			if (recordatorios == null)
			{
				return NotFound();
			}

			return Ok(recordatorios);
		}

		[HttpPost]
		[Route("cambiarEstatus")]
		public async Task<IActionResult> cambiarEstatus(int id)
		{
			var recordatorio = await _baseDatos.Recordatorios.FirstOrDefaultAsync(r => r.IdRecordatorio == id);

			if (recordatorio == null)
			{
				return BadRequest("El recordatorio no existe");
			}

			recordatorio.Estatus = true;
			recordatorio.FechaFin = DateTime.Now;
			await _baseDatos.SaveChangesAsync();

			return Ok("Recordatorio atendido");
		}
	}
}
