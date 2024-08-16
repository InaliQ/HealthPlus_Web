using healt_plus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealtPlus.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlertaController : ControllerBase
	{
		private readonly HealtContext _baseDatos;

		public AlertaController(HealtContext baseDatos)
		{
			_baseDatos = baseDatos;
		}

		[HttpGet]
		[Route("getAlertas")]
		public async Task<ActionResult> getAlertas()
		{
			var alertas = from alerta in _baseDatos.Alerta
						  select new
						  {
							  alerta.IdAlerta,
							  alerta.IdPaciente,
							  alerta.FechaHora,
							  alerta.Descripcion,
							  alerta.IdPacienteNavigation.IdPersonaNavigation.Nombre,
							  alerta.IdPacienteNavigation.NumPaciente,
						  };

			return Ok(alertas);
		}

		[HttpGet]
		[Route("getAlerta/{idPaciente}")]
		public async Task<ActionResult> getAlerta(int idPaciente)
		{

			var alerta = from alert in _baseDatos.Alerta
						  where alert.IdPacienteNavigation.IdPaciente == idPaciente
						  select new
						  {
							  alert.IdAlerta,
							  alert.IdPaciente,
							  alert.FechaHora,
							  alert.Descripcion,
							  alert.IdPacienteNavigation.IdPersonaNavigation.Nombre,
							  alert.IdPacienteNavigation.NumPaciente,
						  };
			if (alerta == null)
			{
				return NotFound();
			}

			return Ok(alerta);
		}

	}
}