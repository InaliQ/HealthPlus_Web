using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace healt_plus.Migrations
{
    /// <inheritdoc />
    public partial class actualizarbd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "horario",
                columns: table => new
                {
                    idHorario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    hora_inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    hora_fin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__horario__DE60F33ABFBBDE37", x => x.idHorario);
                });

            migrationBuilder.CreateTable(
                name: "padecimiento",
                columns: table => new
                {
                    idPadecimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__padecimi__D21C34317EFEF763", x => x.idPadecimiento);
                });

            migrationBuilder.CreateTable(
                name: "persona",
                columns: table => new
                {
                    idPersona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    primer_apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    segundo_apellido = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telefono = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    calle = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    numero = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    codigo_postal = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    colonia = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__persona__A478814116DC655E", x => x.idPersona);
                });

            migrationBuilder.CreateTable(
                name: "servicio",
                columns: table => new
                {
                    idServicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: true),
                    precio = table.Column<double>(type: "float", nullable: true),
                    fecha_pago = table.Column<DateTime>(type: "datetime", nullable: true),
                    estatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__servicio__CEB9811931F2D7E3", x => x.idServicio);
                });

            migrationBuilder.CreateTable(
                name: "doctor",
                columns: table => new
                {
                    idDoctor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cedula = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    num_doctor = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: false),
                    idHorario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__doctor__418956C332497D0B", x => x.idDoctor);
                    table.ForeignKey(
                        name: "FK__doctor__idHorari__4AB81AF0",
                        column: x => x.idHorario,
                        principalTable: "horario",
                        principalColumn: "idHorario");
                    table.ForeignKey(
                        name: "FK__doctor__idPerson__49C3F6B7",
                        column: x => x.idPersona,
                        principalTable: "persona",
                        principalColumn: "idPersona");
                });

            migrationBuilder.CreateTable(
                name: "enfermero",
                columns: table => new
                {
                    idEnfermero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    num_enfermero = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: false),
                    idHorario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__enfermer__A823C618021FFC13", x => x.idEnfermero);
                    table.ForeignKey(
                        name: "FK__enfermero__idHor__4E88ABD4",
                        column: x => x.idHorario,
                        principalTable: "horario",
                        principalColumn: "idHorario");
                    table.ForeignKey(
                        name: "FK__enfermero__idPer__4D94879B",
                        column: x => x.idPersona,
                        principalTable: "persona",
                        principalColumn: "idPersona");
                });

            migrationBuilder.CreateTable(
                name: "paciente",
                columns: table => new
                {
                    idPaciente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    num_paciente = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    altura = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    peso = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    tipo_sangre = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    estatus = table.Column<bool>(type: "bit", nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__paciente__F48A08F23CA962BC", x => x.idPaciente);
                    table.ForeignKey(
                        name: "FK__paciente__idPers__3B75D760",
                        column: x => x.idPersona,
                        principalTable: "persona",
                        principalColumn: "idPersona");
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    idUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    contrasenia = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idPersona = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__usuario__645723A6FB9FF7FF", x => x.idUsuario);
                    table.ForeignKey(
                        name: "FK__usuario__idPerso__5AEE82B9",
                        column: x => x.idPersona,
                        principalTable: "persona",
                        principalColumn: "idPersona");
                });

            migrationBuilder.CreateTable(
                name: "lote_producto",
                columns: table => new
                {
                    idLote = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    precio = table.Column<double>(type: "float", nullable: true),
                    cantidad = table.Column<int>(type: "int", nullable: true),
                    modelo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    precio_lote = table.Column<double>(type: "float", nullable: true),
                    idServicio = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__lote_pro__1B91FFCBCFFBF0DD", x => x.idLote);
                    table.ForeignKey(
                        name: "FK__lote_prod__idSer__6383C8BA",
                        column: x => x.idServicio,
                        principalTable: "servicio",
                        principalColumn: "idServicio");
                });

            migrationBuilder.CreateTable(
                name: "historico_horario_enfermero",
                columns: table => new
                {
                    idHistorico = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idEnfermero = table.Column<int>(type: "int", nullable: false),
                    idHorario = table.Column<int>(type: "int", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__historic__4712CB725B643371", x => x.idHistorico);
                    table.ForeignKey(
                        name: "FK__historico__idEnf__5165187F",
                        column: x => x.idEnfermero,
                        principalTable: "enfermero",
                        principalColumn: "idEnfermero");
                    table.ForeignKey(
                        name: "FK__historico__idHor__52593CB8",
                        column: x => x.idHorario,
                        principalTable: "horario",
                        principalColumn: "idHorario");
                });

            migrationBuilder.CreateTable(
                name: "alerta",
                columns: table => new
                {
                    idAlerta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaciente = table.Column<int>(type: "int", nullable: true),
                    fecha_hora = table.Column<DateTime>(type: "datetime", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__alerta__D09954271E2F4244", x => x.idAlerta);
                    table.ForeignKey(
                        name: "FK__alerta__idPacien__44FF419A",
                        column: x => x.idPaciente,
                        principalTable: "paciente",
                        principalColumn: "idPaciente");
                });

            migrationBuilder.CreateTable(
                name: "monitoreo_salud",
                columns: table => new
                {
                    idMonitoreo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaciente = table.Column<int>(type: "int", nullable: true),
                    fecha_hora = table.Column<DateTime>(type: "datetime", nullable: true),
                    ritmo_cardiaco = table.Column<int>(type: "int", nullable: true),
                    tipo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__monitore__69E8E0BF18412C05", x => x.idMonitoreo);
                    table.ForeignKey(
                        name: "FK__monitoreo__idPac__4222D4EF",
                        column: x => x.idPaciente,
                        principalTable: "paciente",
                        principalColumn: "idPaciente");
                });

            migrationBuilder.CreateTable(
                name: "paciente_padecimiento",
                columns: table => new
                {
                    idPacientePadecimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idPaciente = table.Column<int>(type: "int", nullable: false),
                    idPadecimiento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__paciente__3AE7E25428566CC2", x => x.idPacientePadecimiento);
                    table.ForeignKey(
                        name: "FK__paciente___idPac__3E52440B",
                        column: x => x.idPaciente,
                        principalTable: "paciente",
                        principalColumn: "idPaciente");
                    table.ForeignKey(
                        name: "FK__paciente___idPad__3F466844",
                        column: x => x.idPadecimiento,
                        principalTable: "padecimiento",
                        principalColumn: "idPadecimiento");
                });

            migrationBuilder.CreateTable(
                name: "recordatorio",
                columns: table => new
                {
                    idRecordatorio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    medicamento = table.Column<string>(type: "text", nullable: true),
                    cantidad_medicamento = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    idPaciente = table.Column<int>(type: "int", nullable: false),
                    fecha_inicio = table.Column<DateTime>(type: "datetime", nullable: true),
                    fecha_fin = table.Column<DateTime>(type: "datetime", nullable: true),
                    estatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__recordat__D132AA4273EF3229", x => x.idRecordatorio);
                    table.ForeignKey(
                        name: "FK__recordato__idPac__5535A963",
                        column: x => x.idPaciente,
                        principalTable: "paciente",
                        principalColumn: "idPaciente");
                });

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    idCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUsuario = table.Column<int>(type: "int", nullable: false),
                    idServicio = table.Column<int>(type: "int", nullable: false),
                    estatus = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cliente__885457EE7B3759F6", x => x.idCliente);
                    table.ForeignKey(
                        name: "FK__cliente__idServi__60A75C0F",
                        column: x => x.idServicio,
                        principalTable: "servicio",
                        principalColumn: "idServicio");
                    table.ForeignKey(
                        name: "FK__cliente__idUsuar__5FB337D6",
                        column: x => x.idUsuario,
                        principalTable: "usuario",
                        principalColumn: "idUsuario");
                });

            migrationBuilder.CreateTable(
                name: "recordatorio_por_turno",
                columns: table => new
                {
                    idRecordatorio = table.Column<int>(type: "int", nullable: false),
                    idEnfermero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK__recordato__idEnf__571DF1D5",
                        column: x => x.idEnfermero,
                        principalTable: "enfermero",
                        principalColumn: "idEnfermero");
                    table.ForeignKey(
                        name: "FK__recordato__idRec__5812160E",
                        column: x => x.idRecordatorio,
                        principalTable: "recordatorio",
                        principalColumn: "idRecordatorio");
                });

            migrationBuilder.CreateIndex(
                name: "IX_alerta_idPaciente",
                table: "alerta",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_idServicio",
                table: "cliente",
                column: "idServicio");

            migrationBuilder.CreateIndex(
                name: "IX_cliente_idUsuario",
                table: "cliente",
                column: "idUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_idHorario",
                table: "doctor",
                column: "idHorario");

            migrationBuilder.CreateIndex(
                name: "IX_doctor_idPersona",
                table: "doctor",
                column: "idPersona");

            migrationBuilder.CreateIndex(
                name: "IX_enfermero_idHorario",
                table: "enfermero",
                column: "idHorario");

            migrationBuilder.CreateIndex(
                name: "IX_enfermero_idPersona",
                table: "enfermero",
                column: "idPersona");

            migrationBuilder.CreateIndex(
                name: "IX_historico_horario_enfermero_idEnfermero",
                table: "historico_horario_enfermero",
                column: "idEnfermero");

            migrationBuilder.CreateIndex(
                name: "IX_historico_horario_enfermero_idHorario",
                table: "historico_horario_enfermero",
                column: "idHorario");

            migrationBuilder.CreateIndex(
                name: "IX_lote_producto_idServicio",
                table: "lote_producto",
                column: "idServicio");

            migrationBuilder.CreateIndex(
                name: "IX_monitoreo_salud_idPaciente",
                table: "monitoreo_salud",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_paciente_idPersona",
                table: "paciente",
                column: "idPersona");

            migrationBuilder.CreateIndex(
                name: "IX_paciente_padecimiento_idPaciente",
                table: "paciente_padecimiento",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_paciente_padecimiento_idPadecimiento",
                table: "paciente_padecimiento",
                column: "idPadecimiento");

            migrationBuilder.CreateIndex(
                name: "IX_recordatorio_idPaciente",
                table: "recordatorio",
                column: "idPaciente");

            migrationBuilder.CreateIndex(
                name: "IX_recordatorio_por_turno_idEnfermero",
                table: "recordatorio_por_turno",
                column: "idEnfermero");

            migrationBuilder.CreateIndex(
                name: "IX_recordatorio_por_turno_idRecordatorio",
                table: "recordatorio_por_turno",
                column: "idRecordatorio");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_idPersona",
                table: "usuario",
                column: "idPersona");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerta");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "doctor");

            migrationBuilder.DropTable(
                name: "historico_horario_enfermero");

            migrationBuilder.DropTable(
                name: "lote_producto");

            migrationBuilder.DropTable(
                name: "monitoreo_salud");

            migrationBuilder.DropTable(
                name: "paciente_padecimiento");

            migrationBuilder.DropTable(
                name: "recordatorio_por_turno");

            migrationBuilder.DropTable(
                name: "usuario");

            migrationBuilder.DropTable(
                name: "servicio");

            migrationBuilder.DropTable(
                name: "padecimiento");

            migrationBuilder.DropTable(
                name: "enfermero");

            migrationBuilder.DropTable(
                name: "recordatorio");

            migrationBuilder.DropTable(
                name: "horario");

            migrationBuilder.DropTable(
                name: "paciente");

            migrationBuilder.DropTable(
                name: "persona");
        }
    }
}
