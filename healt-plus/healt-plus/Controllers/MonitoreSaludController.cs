using healt_plus.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace healt_plus.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MonitoreSaludController : ControllerBase
	{
		private readonly HealtContext _baseDatos;

		public MonitoreSaludController(HealtContext baseDatos)
		{
			_baseDatos = baseDatos;
		}

		[HttpPost]
		[Route("registrarMonitoreoSalud")]
		public async Task<ActionResult> RegistarMonitoreoSalud([FromBody] DatosMonitoreoSalud ms)
		{
			using (var transaccion = _baseDatos.Database.BeginTransaction())
			{
				try
				{
					var monitoreoSalud = new MonitoreoSalud
					{
						IdPaciente = ms.IdPaciente,
						FechaHora = ms.FechaHora,
						RitmoCardiaco = ms.RitmoCardiaco,
						Tipo = ms.Tipo
					};

					_baseDatos.MonitoreoSaluds.Add(monitoreoSalud);
					await _baseDatos.SaveChangesAsync();
					transaccion.Commit();
					return Ok("Monitoreo de salud registrado");
				}
				catch (Exception e)
				{
					transaccion.Rollback();
					return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
				}
			}
		}
	}
}
