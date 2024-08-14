using healt_plus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace healt_plus.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealtController : ControllerBase
    {

        //Variable de contexto de base de datos
        private readonly HealtContext _baseDatos;

        ///GENERAMOS CONSTRUCTOR
        public HealtController(HealtContext baseDatos) { this._baseDatos = baseDatos; }


        //---------------------------------------- AUTH -------------------------------------------

        //METODO GET (LOGIIN)
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin request)
        {
            var usuario = await _baseDatos.Usuarios
                .FirstOrDefaultAsync(u => u.Usuario1 == request.user && u.Contrasenia == request.contrasenia);

            if (usuario == null)
            {
                return Unauthorized(new { isSuccess = false, message = "Usuario o contraseña incorrectos" });
            }
            var userName = usuario.Usuario1;
            // Genera un token de ejemplo
            var token = GenerateJwtToken(usuario);
            return Ok(new { isSuccess = true, token, userName });
        }

        private string GenerateJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("esta_es_mi_secret_key_yes_mucho_mas_grande");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", usuario.IdUsuario.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        // --------------------------------------- HORARIOS ----------------------------------------
        //METODO GET (OBTIENE HORARIOS)
        [HttpGet]
        [Route("ListarHorarios")]
        public async Task<IActionResult> ListarHorarios()
        {
            var horarios = from h in _baseDatos.Horarios
                           select new { idHorario = h.IdHorario, horaInicio = h.HoraInicio, horaFin = h.HoraFin };

            return Ok(horarios);
        }

        //POST AGREGAR HORARIO
        [HttpPost]
        [Route("AgregarHorario")]
        public async Task<IActionResult> AgregarHorario([FromBody] Horario request)
        {
            var horario = new Horario
            {
                HoraInicio = request.HoraInicio,
                HoraFin = request.HoraFin,
            };

            _baseDatos.Horarios.Add(horario);
            await _baseDatos.SaveChangesAsync();

            return Ok(horario);

        }

        //PUT MODIFICAR HORARIO
        [HttpPut]
        [Route("ModificarHorario/{id:int}")]
        public async Task<IActionResult> ModificarHorario(int id, [FromBody] Horario request)
        {
            var horarioExiste = await _baseDatos.Horarios.FindAsync(id);
            if (horarioExiste == null)
            {
                return NotFound("Horario no encontrado");
            }

            horarioExiste.HoraInicio = request.HoraInicio;
            horarioExiste.HoraFin = request.HoraFin;

            await _baseDatos.SaveChangesAsync();

            return Ok(horarioExiste);
        }

        //DELETE ELIMINAR HORARIO  
        [HttpDelete]
        [Route("EliminarHorarioo/{id:int}")]
        public async Task<IActionResult> EliminarHorarioo(int id)
        {
            var horarioExiste = await _baseDatos.Horarios.FindAsync(id);
            if (horarioExiste == null)
            {
                return NotFound("Horario no encontrado");
            }

            _baseDatos.Horarios.Remove(horarioExiste);
            await _baseDatos.SaveChangesAsync();

            return Ok(horarioExiste);
        }


        // --------------------------------------- ENFERMEROS --------------------------------

        //METODO GET BUSCAR ENFERMERO POR NOMBRE
        [HttpGet]
        [Route("BuscarEPorNombre")]
        public async Task<IActionResult> BuscarEPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest("vacia");
            }

            var enfermeros = from en in _baseDatos.Enfermeros
                              join pe in _baseDatos.Personas on en.IdPersona equals pe.IdPersona
                              join ho in _baseDatos.Horarios on en.IdHorario equals ho.IdHorario
                             where pe.Nombre.Contains(nombre)
                              select new
                              {
                                  idEnfermero = en.IdEnfermero,
                                  Nombre = pe.Nombre,
                                  primerApellido = pe.PrimerApellido,
                                  segundoApellido = pe.SegundoApellido,
                                  telefono = pe.Telefono,
                                  fechaNacimiento = pe.FechaNacimiento,
                                  calle = pe.Calle,
                                  numero = pe.Numero,
                                  colonia = pe.Colonia,
                                  codigoPostal = pe.CodigoPostal,
                                  titulo = en.Titulo,
                                  numEnfermero = en.NumEnfermero,
                                  horaInicio = ho.HoraInicio,
                                  horaFin = ho.HoraFin,
                                  idHorario = ho.IdHorario
                              };

            if (!enfermeros.Any())
            {
                return NotFound("No hay.");
            }

            return Ok(enfermeros);
        }

        //METODO GET (OBTIENE ENFERMEROS)
        [HttpGet]
        [Route("ListarEnfermeros")]
        public async Task<IActionResult> ListarEnfermeros()
        {
            var enfermreros = from en in _baseDatos.Enfermeros
                            join pe in _baseDatos.Personas on en.IdPersona equals pe.IdPersona
                            join ho in _baseDatos.Horarios on en.IdHorario equals ho.IdHorario
                            select new {
                               idEnfermero = en.IdEnfermero, 
                               Nombre = pe.Nombre,
                               primerApellido = pe.PrimerApellido,
                               segundoApellido = pe.SegundoApellido,
                               telefono = pe.Telefono,
                               fechaNacimiento = pe.FechaNacimiento,
                               calle = pe.Calle, numero = pe.Numero, colonia = pe.Colonia,
                               codigoPostal = pe.CodigoPostal,
                               titulo = en.Titulo, numEnfermero = en.NumEnfermero,
                               horaInicio = ho.HoraInicio, horaFin = ho.HoraFin,
                               idHorario = ho.IdHorario
                            };

            return Ok(enfermreros);
        }

        //POST AGREGAR ENFERMEROS
        [HttpPost]
        [Route("AgregarEnfermero")]
        public async Task<IActionResult> AgregarEnfermero([FromBody] PersonaEnfermero request)
        {
            using (var transaction = _baseDatos.Database.BeginTransaction())
            {
                try
                {
                    var persona = new Persona
                    {
                        Nombre = request.Nombre,
                        PrimerApellido = request.PrimerApellido,
                        SegundoApellido = request.SegundoApellido,
                        Telefono = request.Telefono,
                        FechaNacimiento = request.FechaNacimiento,
                        Calle = request.Calle,
                        Numero = request.Numero,
                        CodigoPostal = request.CodigoPostal,
                        Colonia = request.Colonia
                    };

                    _baseDatos.Personas.Add(persona);
                    await _baseDatos.SaveChangesAsync(); 

                    var enfermero = new Enfermero
                    {
                        Titulo = request.Titulo,
                        NumEnfermero = request.NumEnfermero,
                        IdPersona = persona.IdPersona, 
                        IdHorario = request.IdHorario
                    };

                    _baseDatos.Enfermeros.Add(enfermero);
                    await _baseDatos.SaveChangesAsync();


                    transaction.Commit(); 

                    return Ok(enfermero); 
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Agrega registro de error
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }

            }
        }

        // PUT MODIFICAR ENFERMERO
        [HttpPut]
        [Route("ModificarEnfermero/{id:int}")]
        public async Task<IActionResult> ModificarEnfermero(int id, [FromBody] PersonaEnfermero request)
        {
            if (request == null)
            {
                return BadRequest("Datos inválidos.");
            }

            using (var transaction = await _baseDatos.Database.BeginTransactionAsync())
            {
                try
                {
                    var enfermeroExistente = await _baseDatos.Enfermeros
                        .Include(e => e.IdPersonaNavigation)
                        .Include(e => e.IdHorarioNavigation)
                        .FirstOrDefaultAsync(e => e.IdEnfermero == id);

                    if (enfermeroExistente == null)
                    {
                        return NotFound("Enfermero no encontrado.");
                    }

                    var personaExistente = enfermeroExistente.IdPersonaNavigation;
                    personaExistente.Nombre = request.Nombre;
                    personaExistente.PrimerApellido = request.PrimerApellido;
                    personaExistente.SegundoApellido = request.SegundoApellido;
                    personaExistente.Telefono = request.Telefono;
                    personaExistente.FechaNacimiento = request.FechaNacimiento;
                    personaExistente.Calle = request.Calle;
                    personaExistente.Numero = request.Numero;
                    personaExistente.CodigoPostal = request.CodigoPostal;
                    personaExistente.Colonia = request.Colonia;

                    enfermeroExistente.NumEnfermero = request.NumEnfermero;
                    enfermeroExistente.Titulo = request.Titulo;

                    enfermeroExistente.IdHorario = request.IdHorario;

                    await _baseDatos.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(enfermeroExistente);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }



        //------------------------------------- PACIENTES ----------------------------------------//

        //METODO GET BUSCAR PACIENTE POR NOMBRE
        [HttpGet]
        [Route("BuscarPorNombre")]
        public async Task<IActionResult> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return BadRequest("vacia");
            }

            var pacientes = from pa in _baseDatos.Pacientes
                            join pe in _baseDatos.Personas on pa.IdPersona equals pe.IdPersona
                            join pad in _baseDatos.PacientePadecimientos on pa.IdPaciente equals pad.IdPaciente
                            join pade in _baseDatos.Padecimientos on pad.IdPadecimiento equals pade.IdPadecimiento
                            where pe.Nombre.Contains(nombre)
                            select new
                            {
                                idPaciente = pa.IdPaciente,
                                Nombre = pe.Nombre,
                                primerApellido = pe.PrimerApellido,
                                segundoApellido = pe.SegundoApellido,
                                telefono = pe.Telefono,
                                fecha_nac = pe.FechaNacimiento,
                                calle = pe.Calle,
                                numero = pe.Numero,
                                colonia = pe.Colonia,
                                codigoPostal = pe.CodigoPostal,
                                numPaciente = pa.NumPaciente,
                                pa.Altura,
                                pa.Peso,
                                pa.TipoSangre,
                                nombrePadecimiento = pade.Nombre,
                                idPadecimiento = pade.IdPadecimiento,
                                estatus = pa.Estatus
                            };

            
            if (!pacientes.Any())
            {
                return NotFound("No hay.");
            }

            return Ok(pacientes);
        }

        //METODO GET BUSCAR POR ESTATUS
        [HttpGet]
        [Route("BuscarXEstatus")]
        public async Task<IActionResult> BuscarXEstatus(bool estatus)
        {
            var pacientes = from pa in _baseDatos.Pacientes
                            join pe in _baseDatos.Personas on pa.IdPersona equals pe.IdPersona
                            join pad in _baseDatos.PacientePadecimientos on pa.IdPaciente equals pad.IdPaciente
                            join pade in _baseDatos.Padecimientos on pad.IdPadecimiento equals pade.IdPadecimiento
                            where pa.Estatus == estatus
                            select new
                            {
                                idPaciente = pa.IdPaciente,
                                Nombre = pe.Nombre,
                                primerApellido = pe.PrimerApellido,
                                segundoApellido = pe.SegundoApellido,
                                telefono = pe.Telefono,
                                calle = pe.Calle,
                                numero = pe.Numero,
                                colonia = pe.Colonia,
                                codigoPostal = pe.CodigoPostal,
                                numPaciente = pa.NumPaciente,
                                pa.Altura,
                                pa.Peso,
                                pa.TipoSangre,
                                nombrePadecimiento = pade.Nombre,
                                idPadecimiento = pade.IdPadecimiento,
                                estatus = pa.Estatus
                            };

            if (!pacientes.Any())
            {
                return NotFound("No hay");
            }

            return Ok(pacientes);
        }


        //METODO GET (OBTIENE PASCIENTES)
        [HttpGet]
        [Route("ListarPacientes")]
        public async Task<IActionResult> ListarPacientes()
        {
            var pacientes = from pa in _baseDatos.Pacientes
                            join pe in _baseDatos.Personas on pa.IdPersona equals pe.IdPersona
                            join pad in _baseDatos.PacientePadecimientos on pa.IdPaciente equals pad.IdPaciente
                            join pade in _baseDatos.Padecimientos on pad.IdPadecimiento equals pade.IdPadecimiento
                            select new
                            {
                                idPaciente = pa.IdPaciente,
                                Nombre = pe.Nombre,
                                primerApellido = pe.PrimerApellido,
                                segundoApellido = pe.SegundoApellido,
                                telefono = pe.Telefono,
                                fechaNacimiento = pe.FechaNacimiento, 
                                calle = pe.Calle,
                                numero = pe.Numero,
                                colonia = pe.Colonia,
                                codigoPostal = pe.CodigoPostal,
                                numPaciente = pa.NumPaciente,
                                pa.Altura,
                                pa.Peso,
                                pa.TipoSangre,
                                pa.RitmoMax,
                                pa.RitmoMin,
                                nombrePadecimiento = pade.Nombre,
                                idPadecimiento = pade.IdPadecimiento,
                                estatus = pa.Estatus
                            };

            return Ok(pacientes);
        }

        //POST AGREGAR PACIENTES
        [HttpPost]
        [Route("AgregarPaciente")]
        public async Task<IActionResult> AgregarPaciente([FromBody] PersonaPaciente request)
        {
            using (var transaction = _baseDatos.Database.BeginTransaction())
            {
                try
                {
                    // Agregar persona
                    var persona = new Persona
                    { Nombre = request.Nombre, PrimerApellido = request.PrimerApellido,
                        SegundoApellido = request.SegundoApellido, Telefono = request.Telefono,
                        FechaNacimiento = request.FechaNacimiento,
                        Calle = request.Calle, Numero = request.Numero,
                        CodigoPostal = request.CodigoPostal,Colonia = request.Colonia
                    };

                    _baseDatos.Personas.Add(persona);
                    await _baseDatos.SaveChangesAsync();

                    // Agregar paciente
                    var paciente = new Paciente
                    { NumPaciente = request.NumPaciente, Altura = request.Altura,
                        Peso = request.Peso, TipoSangre = request.TipoSangre,
                        Estatus = request.Estatus, IdPersona = persona.IdPersona,
                        RitmoMin = request.RitmoMin, RitmoMax = request.RitmoMax
                    };

                    _baseDatos.Pacientes.Add(paciente);
                    await _baseDatos.SaveChangesAsync();


                    // Agregar PacientePadecimiento
                    var pacientePadecimiento = new PacientePadecimiento
                    {IdPaciente = paciente.IdPaciente, IdPadecimiento = request.IdPadecimiento};

                    _baseDatos.PacientePadecimientos.Add(pacientePadecimiento);
                    await _baseDatos.SaveChangesAsync();

                    transaction.Commit();

                    return Ok(paciente);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                   
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        // PUT MODIFICAR PACIENTE
        [HttpPut]
        [Route("ModificarPaciente/{id:int}")]
        public async Task<IActionResult> ModificarPaciente(int id, [FromBody] PersonaPaciente request)
        {
            if (request == null)
            {
                return BadRequest("Datos inválidos.");
            }

            using (var transaction = await _baseDatos.Database.BeginTransactionAsync())
            {
                try
                {
                    var pacienteExistente = await _baseDatos.Pacientes
                        .Include(p => p.IdPersonaNavigation)
                        .Include(p => p.PacientePadecimientos)
                        .FirstOrDefaultAsync(p => p.IdPaciente == id);

                    if (pacienteExistente == null)
                    {
                        return NotFound("Paciente no encontrado.");
                    }

                    var personaExistente = pacienteExistente.IdPersonaNavigation;
                    personaExistente.Nombre = request.Nombre;
                    personaExistente.PrimerApellido = request.PrimerApellido;
                    personaExistente.SegundoApellido = request.SegundoApellido;
                    personaExistente.Telefono = request.Telefono;
                    personaExistente.FechaNacimiento = request.FechaNacimiento;
                    personaExistente.Calle = request.Calle;
                    personaExistente.Numero = request.Numero;
                    personaExistente.CodigoPostal = request.CodigoPostal;
                    personaExistente.Colonia = request.Colonia;

                    pacienteExistente.NumPaciente = request.NumPaciente;
                    pacienteExistente.Altura = request.Altura;
                    pacienteExistente.Peso = request.Peso;
                    pacienteExistente.TipoSangre = request.TipoSangre;
                    pacienteExistente.RitmoMin = request.RitmoMin;  
                    pacienteExistente.RitmoMax = request.RitmoMax;
                    pacienteExistente.Estatus = request.Estatus;

                    // Actualizar información de los padecimientos
                    var pacientePadecimientoExistente = pacienteExistente.PacientePadecimientos.FirstOrDefault();
                    if (pacientePadecimientoExistente != null){
                        pacientePadecimientoExistente.IdPadecimiento = request.IdPadecimiento;
                    }
                    else
                    {
                        var nuevoPacientePadecimiento = new PacientePadecimiento
                        {
                            IdPaciente = pacienteExistente.IdPaciente,
                            IdPadecimiento = request.IdPadecimiento
                        };
                        _baseDatos.PacientePadecimientos.Add(nuevoPacientePadecimiento);
                    }

                    await _baseDatos.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(pacienteExistente);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        // PUT MODIFICAR ESTATUS PACIENTE
        [HttpPut]
        [Route("ModificarEstatusPaciente/{id:int}")]
        public async Task<IActionResult> ModificarEstatusPaciente(int id, [FromBody] PersonaPaciente request)
        {
           
            using (var transaction = await _baseDatos.Database.BeginTransactionAsync())
            {
                try
                {
                    var pacienteExistente = await _baseDatos.Pacientes
                        .FirstOrDefaultAsync(p => p.IdPaciente == id);

                    if (pacienteExistente == null)
                    {
                        return NotFound("Paciente no encontrado.");
                    }

                    pacienteExistente.Estatus = request.Estatus;

                    await _baseDatos.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Ok(pacienteExistente);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }
        }

        // GET NUM PACIENTES POR PADECIMIENTO
        [HttpGet]
        [Route("PacientesXPadecimiento")]
        public async Task<IActionResult> PacientesXPadecimiento()
        {
            var pacientes = from pa in _baseDatos.PacientePadecimientos
                           group pa by pa.IdPadecimiento into g
                           select new
                           {
                               PadecimientoId = g.Key,
                               PadecimientoNombre = (from p in _baseDatos.Padecimientos
                                                     where p.IdPadecimiento == g.Key
                                                     select p.Nombre).FirstOrDefault(),
                               CantidadPacientes = g.Count()
                           };

            return Ok(pacientes);

        }

        //POST AGREGAR PACIENTE A ENFERMERO
        [HttpPost]
        [Route("AgregarPacienteEnfermero")]
        public async Task<IActionResult> AgregarPacienteEnfermero([FromBody] PacienteEnfermero request)
        {
            var pe = new PacienteEnfermero
            {
                IdPaciente = request.IdPaciente,
                IdEnfermero = request.IdEnfermero
            };

            _baseDatos.PacienteEnfermeros.Add(pe);
            await _baseDatos.SaveChangesAsync();

            return Ok(pe);

        }

        // GET VER PACIENTES POR ENFERMERO
        [HttpGet]
        [Route("VerPacientesXEnfermero/{idEnfermero}")]
        public async Task<IActionResult> VerPacientesXEnfermero(int idEnfermero)
        {
            var pacientes = from pa in _baseDatos.PacienteEnfermeros
                            where pa.IdEnfermero == idEnfermero
                            join p in _baseDatos.Pacientes on pa.IdPaciente equals p.IdPaciente
                            join pe in _baseDatos.Personas on p.IdPersona equals pe.IdPersona
                            join pepa in _baseDatos.PacientePadecimientos on p.IdPaciente equals pepa.IdPaciente
                            join pad in _baseDatos.Padecimientos on pepa.IdPadecimiento equals pad.IdPadecimiento
                            select new
                            {
                                idPaciente = p.IdPaciente,
                                numPaciente = p.NumPaciente,
                                nombre = pe.Nombre + ' ' + pe.PrimerApellido + ' ' + pe.SegundoApellido,
                                tipoSangre = p.TipoSangre,
                                peso = p.Peso,
                                altura = p.Altura,
                                nombrePadecimiento = pad.Nombre,
                                estatus = p.Estatus,
                            };

            return Ok(pacientes);
        }



        // ----------------------------------------- PADECIMIENTOS ------------------------------------------

        //METODO GET (OBTIENE PADECIMIENTOS)
        [HttpGet]
        [Route("ListarPadecimientos")]
        public async Task<IActionResult> ListarPadecimientos()
        {
            var padecimientos = from p in _baseDatos.Padecimientos
                                select new { IdPadecimiento = p.IdPadecimiento, Nombre = p.Nombre };

            return Ok(padecimientos);
        }

        //POST AGREGAR PADECIMIENTOS
        [HttpPost]
        [Route("AgregarPadecimientos")]
        public async Task<IActionResult> AgregarPadecimientos([FromBody] Padecimiento request){
            var padecimiento = new Padecimiento{
                Nombre = request.Nombre
            };

            _baseDatos.Padecimientos.Add(padecimiento);
            await _baseDatos.SaveChangesAsync();                    

            return Ok(padecimiento);

        }

        //PUT MODIFICAR PADECIMIENTOS
        [HttpPut]
        [Route("ModificarPadecimiento/{id:int}")]
        public async Task<IActionResult> ModificarPadecimiento(int id, [FromBody] Padecimiento request){
            var padecimientoExistente = await _baseDatos.Padecimientos.FindAsync(id);
            if (padecimientoExistente == null)
            {
                return NotFound("Padecimiento no encontrado");
            }

            padecimientoExistente.Nombre = request.Nombre;

            await _baseDatos.SaveChangesAsync();

            return Ok(padecimientoExistente);
        }

        //DELETE ELIMINAR PADECIMIENTO  
        [HttpDelete]
        [Route("EliminarPadecimiento/{id:int}")]
        public async Task<IActionResult> EliminarPadecimiento(int id)
        {
            var padecimientoExistente = await _baseDatos.Padecimientos.FindAsync(id);
            if (padecimientoExistente == null)
            {
                return NotFound("Padecimiento no encontrado");
            }

            _baseDatos.Padecimientos.Remove(padecimientoExistente);
            await _baseDatos.SaveChangesAsync();

            return Ok(padecimientoExistente);
        }



        [HttpGet]
        [Route("RitmoDia/{idPaciente}/{fecha}")]
        public async Task<IActionResult> RitmoDia(int idPaciente, DateTime fecha)
        {
            var resultados = await _baseDatos.MonitoreoSaluds
                .Where(mo => mo.IdPaciente == idPaciente && EF.Functions.DateDiffDay(mo.FechaHora, fecha) == 0)
                .Select(mo => new
                {
                    mo.RitmoCardiaco,
                    mo.FechaHora
                })
                .ToListAsync(); // Ejecuta la consulta

            return Ok(resultados);
        }





        // GET PACIENTES POR EDAD
        [HttpGet]
        [Route("PacientesPorEdad")]
        public async Task<IActionResult> PacientesPorEdad(){
            var ahora = DateTime.Today; 
            var pacientes = from pa in _baseDatos.Pacientes
                            join pe in _baseDatos.Personas on pa.IdPersona equals pe.IdPersona
                            select new { FechaNacimiento = pe.FechaNacimiento };

            var pacientesList = await pacientes.ToListAsync();

            var pacientesPorEdad = pacientesList
                .GroupBy(p => CalcularEdad((DateOnly)p.FechaNacimiento))
                .Select(g => new
                {
                    Edad = g.Key,
                    CantidadPacientes = g.Count(),
                    Pacientes = g.ToList()
                });
            return Ok(pacientesPorEdad);
        }

        // Método para calcular la edad
        private int CalcularEdad(DateOnly fechaNacimiento){
            var ahora = DateOnly.FromDateTime(DateTime.Today);
            int edad = ahora.Year - fechaNacimiento.Year;

            if (ahora < fechaNacimiento.AddYears(edad)){
                edad--;
            }

            return edad;
        }



    }


}






public class PersonaPaciente
        {
            public string? Nombre { get; set; }
            public string? PrimerApellido { get; set; }
            public string? SegundoApellido { get; set; }
            public string? Telefono { get; set; }
            public DateOnly? FechaNacimiento { get; set; }
            public string? Calle { get; set; }
            public string? Numero { get; set; }
            public string? CodigoPostal { get; set; }
            public string? Colonia { get; set; }
            public string? NumPaciente { get; set; }
            public string? Altura { get; set; }
            public string? Peso { get; set; }
            public string? TipoSangre { get; set; }
            public string? RitmoMax { get; set; }
            public string? RitmoMin { get; set; }
            public bool Estatus { get; set; }
            public int IdPadecimiento { get; set; }
        }


        public class PersonaEnfermero
        {
            public string Nombre { get; set; }
            public string PrimerApellido { get; set; }
            public string SegundoApellido { get; set; }
            public string Telefono { get; set; }
            public DateOnly? FechaNacimiento { get; set; }
            public string Calle { get; set; }
            public string Numero { get; set; }
            public string CodigoPostal { get; set; }
            public string Colonia { get; set; }
            public string Titulo { get; set; }
            public string NumEnfermero { get; set; }
            public int IdHorario { get; set; }
        }

        public class UsuarioLogin{
            public string user { get; set; }
            public string contrasenia { get; set; }
        }





    

