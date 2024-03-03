﻿using System;
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

    public virtual DbSet<Pago> Pago { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Temporada> Temporada { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=labsystec.net;user=labsyste_clubDep;database=clubDeportivo;password=8We0ds?20", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.11.3-mariadb"));

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
            entity.Property(e => e.NombreCategoria).HasMaxLength(50);
        });

        modelBuilder.Entity<Jugador>(entity =>
        {
            entity.HasKey(e => e.IdJugador).HasName("PRIMARY");

            entity.ToTable("jugador", tb => tb.HasComment("Tabla que contiene la información de los jugadores registrador."));

            entity.HasIndex(e => e.IdUsuario, "jugador_responsable_FK");

            entity.HasIndex(e => e.IdTemporada, "jugador_temporada_FK");

            entity.Property(e => e.IdJugador).HasColumnType("int(11)");
            entity.Property(e => e.Deuda).HasPrecision(10);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.IdTemporada).HasColumnType("int(11)");
            entity.Property(e => e.IdUsuario).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdTemporadaNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdTemporada)
                .HasConstraintName("jugador_temporada_FK");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Jugador)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("jugador_responsable_FK");
        });

        modelBuilder.Entity<Pago>(entity =>
        {
            entity.HasKey(e => e.IdPago).HasName("PRIMARY");

            entity.ToTable("pago");

            entity.HasIndex(e => e.IdJugador, "pago_jugador_FK");

            entity.Property(e => e.IdPago).HasColumnType("int(11)");
            entity.Property(e => e.CantidadPago).HasPrecision(10);
            entity.Property(e => e.FechaPago).HasColumnType("datetime");
            entity.Property(e => e.IdJugador).HasColumnType("int(11)");

            entity.HasOne(d => d.IdJugadorNavigation).WithMany(p => p.Pago)
                .HasForeignKey(d => d.IdJugador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pago_jugador_FK");
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
            entity.Property(e => e.FechaFinal).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Temporada)
                .HasForeignKey(d => d.IdCategoria)
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
                .HasMaxLength(64)
                .IsFixedLength();
            entity.Property(e => e.IdRol)
                .HasDefaultValueSql("'2'")
                .HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(50);
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