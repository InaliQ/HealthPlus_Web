using healt_plus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealtPlus.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LogController : ControllerBase
	{
		private readonly HealtContext _baseDatos;

		public LogController(HealtContext baseDatos)
		{
			_baseDatos = baseDatos;
		}

		[HttpPost]
		[Route("in")]
		public async Task<ActionResult> logIn([FromQuery] string usuario, [FromQuery] string contrasenia)
		{
			try
			{
				var usuarios = await _baseDatos.Usuarios
												.Include(u => u.IdPersonaNavigation)
												.ThenInclude(p => p.Enfermeros) // Include Enfermeros
												.Where(u => u.Usuario1 == usuario && u.Contrasenia == contrasenia)
												.Select(u => new
												{
													u.IdUsuario,
													u.Usuario1,
													u.IdPersonaNavigation.IdPersona,
													u.IdPersonaNavigation.Nombre,
													u.IdPersonaNavigation.PrimerApellido,
													u.IdPersonaNavigation.SegundoApellido,
													Enfermero = u.IdPersonaNavigation.Enfermeros.Select(e => new
													{
														e.IdEnfermero,
														e.Titulo,
														e.NumEnfermero
													}).FirstOrDefault() // Select Enfermero information
												})
												.FirstOrDefaultAsync();

				if (usuarios == null)
				{
					return NotFound("Usuario o contraseña incorrectos");
				}
				else
				{
					return Ok(usuarios);
				}
			}
			catch (Exception ex)
			{
				// Manejo de errores genéricos
				return StatusCode(500, new { message = "Ocurrió un error en el servidor. Por favor, inténtelo de nuevo más tarde.", details = ex.Message });
			}
		}


	}
}
