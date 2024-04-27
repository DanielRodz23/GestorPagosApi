using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestorPagosApi.Models.Entities;

public partial class ClubDeportivoContext : DbContext
{
    public ClubDeportivoContext()
    {
    }

    public ClubDeportivoContext(DbContextOptions<ClubDeportivoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Jugador> Jugador { get; set; }

    public virtual DbSet<JugadoresTemporada> JugadoresTemporada { get; set; }

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Registro> Registro { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Temporada> Temporada { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.EdadInicial).HasColumnType("int(11)");
            entity.Property(e => e.EdadTermino).HasColumnType("int(11)");
            entity.Property(e => e.NombreCategoria).HasMaxLength(50);
        });

        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.IdJugador).HasName("PRIMARY");

            entity.ToTable("jugador", tb => tb.HasComment("Tabla que contiene la información de los jugadores registrador."));

            entity.HasIndex(e => e.IdCategoria, "jugador_categoria_FK");

            entity.HasIndex(e => e.IdUsuario, "jugador_responsable_FK");

            entity.HasIndex(e => e.IdTemporada, "jugador_temporada_FK");

            entity.Property(e => e.IdJugador).HasColumnType("int(11)");
            entity.Property(e => e.Deuda).HasPrecision(10);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Exists)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.IdTemporada).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("jugador_categoria_FK");

            entity.HasOne(d => d.IdTemporadaNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdTemporada)
                .HasConstraintName("jugador_temporada_FK");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jugador_responsable_FK");
        });

        modelBuilder.Entity<JugadoresTemporada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("jugadores_temporada");

            entity.HasIndex(e => e.IdJugador, "Jugadores_Temporada_jugador_FK");

            entity.HasIndex(e => e.IdTemporada, "Jugadores_Temporada_temporada_FK");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdJugador).HasColumnType("int(11)");
            entity.Property(e => e.IdTemporada).HasColumnType("int(11)");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.JugadoresTemporada)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Jugadores_Temporada_jugador_FK");

            entity.HasOne(d => d.IdTemporadaNavigation).WithMany(p => p.JugadoresTemporada)
                .HasForeignKey(d => d.IdTemporada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Jugadores_Temporada_temporada_FK");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.IdJugador, "pago_jugador_FK");

            entity.HasIndex(e => e.IdResponsable, "pago_usuarios_FK");

            entity.Property(e => e.IdPago).HasColumnType("int(11)");
            entity.Property(e => e.CantidadPago).HasPrecision(10);
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.IdJugador).HasColumnType("int(11)");
            entity.Property(e => e.IdResponsable).HasColumnType("int(11)");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pago_jugador_FK");

            entity.HasOne(d => d.IdResponsableNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdResponsable)
                .HasConstraintName("pago_usuarios_FK");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.IdRegistro).HasName("PRIMARY");

            entity.ToTable("registro");

            entity.Property(e => e.IdRegistro)
                .HasColumnType("int(11)")
                .HasColumnName("Id_Registro");
            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.FechaEjecucion).HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(50);
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.IdRol).HasColumnType("int(11)");
            entity.Property(e => e.NombreRol).HasMaxLength(50);
        });

        modelBuilder.Entity<Temporada>(entity =>
        {
            entity.HasKey(e => e.IdTemporada).HasName("PRIMARY");

            entity.ToTable("temporada");

            entity.HasIndex(e => e.IdCategoria, "temporada_categoria_FK");

            entity.Property(e => e.IdTemporada).HasColumnType("int(11)");
            entity.Property(e => e.Costo).HasPrecision(10);
            entity.Property(e => e.Exists)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.FechaFinal).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IdCategoria)
                .HasDefaultValueSql("'1'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.TempActual)
                .IsRequired()
                .HasDefaultValueSql("'1'");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Temporada)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("temporada_categoria_FK");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuarios", tb => tb.HasComment("Esta tabla almacena la informacion del responsable de los jugadores."));

            entity.HasIndex(e => e.Usuario, "Responsable_unique").IsUnique();

            entity.HasIndex(e => e.IdRol, "usuarios_roles_FK");

            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.Correo).HasMaxLength(80);
            entity.Property(e => e.Exists)
                .IsRequired()
                .HasDefaultValueSql("'1'");
            entity.Property(e => e.IdRol)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(50);
            entity.Property(e => e.Rfc)
                .HasMaxLength(25)
                .HasColumnName("RFC");
            entity.Property(e => e.Telefono).HasMaxLength(20);
            entity.Property(e => e.Usuario).HasMaxLength(100);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_roles_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
