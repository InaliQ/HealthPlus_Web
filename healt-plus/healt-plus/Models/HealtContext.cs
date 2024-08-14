using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace healt_plus.Models;

public partial class HealtContext : DbContext
{
    public HealtContext()
    {
    }

    public HealtContext(DbContextOptions<HealtContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alertum> Alerta { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Enfermero> Enfermeros { get; set; }

    public virtual DbSet<HistoricoHorarioEnfermero> HistoricoHorarioEnfermeros { get; set; }

    public virtual DbSet<HistoricoPacienteEnfermero> HistoricoPacienteEnfermeros { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<LoteProducto> LoteProductos { get; set; }

    public virtual DbSet<MonitoreoSalud> MonitoreoSaluds { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<PacienteEnfermero> PacienteEnfermeros { get; set; }

    public virtual DbSet<PacientePadecimiento> PacientePadecimientos { get; set; }

    public virtual DbSet<Padecimiento> Padecimientos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Recordatorio> Recordatorios { get; set; }

    public virtual DbSet<RecordatorioPorTurno> RecordatorioPorTurnos { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=HIROMI; Initial Catalog=healt; user id=sa; password=root;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alertum>(entity =>
        {
            entity.HasKey(e => e.IdAlerta).HasName("PK__alerta__D0995427DE232A00");

            entity.ToTable("alerta");

            entity.Property(e => e.IdAlerta).HasColumnName("idAlerta");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Alerta)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__alerta__idPacien__44FF419A");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__885457EEBDED9366");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cliente__idServi__60A75C0F");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cliente__idUsuar__5FB337D6");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.IdDoctor).HasName("PK__doctor__418956C39A2F2937");

            entity.ToTable("doctor");

            entity.Property(e => e.IdDoctor).HasColumnName("idDoctor");
            entity.Property(e => e.Cedula)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.NumDoctor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("num_doctor");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__doctor__idHorari__4AB81AF0");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__doctor__idPerson__49C3F6B7");
        });

        modelBuilder.Entity<Enfermero>(entity =>
        {
            entity.HasKey(e => e.IdEnfermero).HasName("PK__enfermer__A823C61821A48199");

            entity.ToTable("enfermero");

            entity.Property(e => e.IdEnfermero).HasColumnName("idEnfermero");
            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.NumEnfermero)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("num_enfermero");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.Enfermeros)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__enfermero__idHor__4E88ABD4");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Enfermeros)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__enfermero__idPer__4D94879B");
        });

        modelBuilder.Entity<HistoricoHorarioEnfermero>(entity =>
        {
            entity.HasKey(e => e.IdHistorico).HasName("PK__historic__4712CB7278D39175");

            entity.ToTable("historico_horario_enfermero");

            entity.Property(e => e.IdHistorico).HasColumnName("idHistorico");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdEnfermero).HasColumnName("idEnfermero");
            entity.Property(e => e.IdHorario).HasColumnName("idHorario");

            entity.HasOne(d => d.IdEnfermeroNavigation).WithMany(p => p.HistoricoHorarioEnfermeros)
                .HasForeignKey(d => d.IdEnfermero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historico__idEnf__5165187F");

            entity.HasOne(d => d.IdHorarioNavigation).WithMany(p => p.HistoricoHorarioEnfermeros)
                .HasForeignKey(d => d.IdHorario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historico__idHor__52593CB8");
        });

        modelBuilder.Entity<HistoricoPacienteEnfermero>(entity =>
        {
            entity.HasKey(e => e.IdHistorico).HasName("PK__historic__4712CB723F8D10E3");

            entity.ToTable("historico_paciente_enfermero");

            entity.Property(e => e.IdHistorico).HasColumnName("idHistorico");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdEnfermero).HasColumnName("idEnfermero");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

            entity.HasOne(d => d.IdEnfermeroNavigation).WithMany(p => p.HistoricoPacienteEnfermeros)
                .HasForeignKey(d => d.IdEnfermero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historico__idEnf__6B24EA82");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.HistoricoPacienteEnfermeros)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__historico__idPac__6A30C649");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.IdHorario).HasName("PK__horario__DE60F33ADEE83DE3");

            entity.ToTable("horario");

            entity.Property(e => e.IdHorario).HasColumnName("idHorario");
            entity.Property(e => e.HoraFin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hora_fin");
            entity.Property(e => e.HoraInicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hora_inicio");
        });

        modelBuilder.Entity<LoteProducto>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("PK__lote_pro__1B91FFCB7591CB96");

            entity.ToTable("lote_producto");

            entity.Property(e => e.IdLote).HasColumnName("idLote");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.Modelo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modelo");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.PrecioLote).HasColumnName("precio_lote");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.LoteProductos)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__lote_prod__idSer__6383C8BA");
        });

        modelBuilder.Entity<MonitoreoSalud>(entity =>
        {
            entity.HasKey(e => e.IdMonitoreo).HasName("PK__monitore__69E8E0BF754423DB");

            entity.ToTable("monitoreo_salud");

            entity.Property(e => e.IdMonitoreo).HasColumnName("idMonitoreo");
            entity.Property(e => e.FechaHora)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.RitmoCardiaco).HasColumnName("ritmo_cardiaco");
            entity.Property(e => e.Tipo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.MonitoreoSaluds)
                .HasForeignKey(d => d.IdPaciente)
                .HasConstraintName("FK__monitoreo__idPac__4222D4EF");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__paciente__F48A08F2209EBEEB");

            entity.ToTable("paciente");

            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.Altura)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("altura");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.NumPaciente)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("num_paciente");
            entity.Property(e => e.Peso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("peso");
            entity.Property(e => e.RitmoMax)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ritmo_max");
            entity.Property(e => e.RitmoMin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ritmo_min");
            entity.Property(e => e.TipoSangre)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tipo_sangre");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__paciente__idPers__3B75D760");
        });

        modelBuilder.Entity<PacienteEnfermero>(entity =>
        {
            entity.HasKey(e => e.IdPacienteEnfermero).HasName("PK__paciente__53C80F2D60928801");

            entity.ToTable("paciente_enfermero");

            entity.Property(e => e.IdPacienteEnfermero).HasColumnName("idPacienteEnfermero");
            entity.Property(e => e.IdEnfermero).HasColumnName("idEnfermero");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

            entity.HasOne(d => d.IdEnfermeroNavigation).WithMany(p => p.PacienteEnfermeros)
                .HasForeignKey(d => d.IdEnfermero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__paciente___idEnf__6754599E");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.PacienteEnfermeros)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__paciente___idPac__66603565");
        });

        modelBuilder.Entity<PacientePadecimiento>(entity =>
        {
            entity.HasKey(e => e.IdPacientePadecimiento).HasName("PK__paciente__3AE7E25494FC61F0");

            entity.ToTable("paciente_padecimiento");

            entity.Property(e => e.IdPacientePadecimiento).HasColumnName("idPacientePadecimiento");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.IdPadecimiento).HasColumnName("idPadecimiento");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.PacientePadecimientos)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__paciente___idPac__3E52440B");

            entity.HasOne(d => d.IdPadecimientoNavigation).WithMany(p => p.PacientePadecimientos)
                .HasForeignKey(d => d.IdPadecimiento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__paciente___idPad__3F466844");
        });

        modelBuilder.Entity<Padecimiento>(entity =>
        {
            entity.HasKey(e => e.IdPadecimiento).HasName("PK__padecimi__D21C3431C8A022C8");

            entity.ToTable("padecimiento");

            entity.Property(e => e.IdPadecimiento).HasColumnName("idPadecimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__persona__A478814124E55C44");

            entity.ToTable("persona");

            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Calle)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("calle");
            entity.Property(e => e.CodigoPostal)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("codigo_postal");
            entity.Property(e => e.Colonia)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("colonia");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Numero)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("numero");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<Recordatorio>(entity =>
        {
            entity.HasKey(e => e.IdRecordatorio).HasName("PK__recordat__D132AA421BEEBC4E");

            entity.ToTable("recordatorio");

            entity.Property(e => e.IdRecordatorio).HasColumnName("idRecordatorio");
            entity.Property(e => e.CantidadMedicamento)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cantidad_medicamento");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");
            entity.Property(e => e.Medicamento)
                .HasColumnType("text")
                .HasColumnName("medicamento");

            entity.HasOne(d => d.IdPacienteNavigation).WithMany(p => p.Recordatorios)
                .HasForeignKey(d => d.IdPaciente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recordato__idPac__5535A963");
        });

        modelBuilder.Entity<RecordatorioPorTurno>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("recordatorio_por_turno");

            entity.Property(e => e.IdEnfermero).HasColumnName("idEnfermero");
            entity.Property(e => e.IdRecordatorio).HasColumnName("idRecordatorio");

            entity.HasOne(d => d.IdEnfermeroNavigation).WithMany()
                .HasForeignKey(d => d.IdEnfermero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recordato__idEnf__571DF1D5");

            entity.HasOne(d => d.IdRecordatorioNavigation).WithMany()
                .HasForeignKey(d => d.IdRecordatorio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recordato__idRec__5812160E");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__servicio__CEB98119CDEC50B9");

            entity.ToTable("servicio");

            entity.Property(e => e.IdServicio).HasColumnName("idServicio");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.FechaFin)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio)
                .HasColumnType("datetime")
                .HasColumnName("fecha_inicio");
            entity.Property(e => e.FechaPago)
                .HasColumnType("datetime")
                .HasColumnName("fecha_pago");
            entity.Property(e => e.Precio).HasColumnName("precio");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__usuario__645723A6554F64B2");

            entity.ToTable("usuario");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contrasenia");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.IdPersona).HasColumnName("idPersona");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__usuario__idPerso__5AEE82B9");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
