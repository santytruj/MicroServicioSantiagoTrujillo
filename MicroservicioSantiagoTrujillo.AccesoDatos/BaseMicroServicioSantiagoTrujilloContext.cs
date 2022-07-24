using MicroServicioSantiagoTrujillo.Entidades;
using Microsoft.EntityFrameworkCore;
using System;

namespace MicroservicioSantiagoTrujillo.AccesoDatos
{
    public partial class BaseMicroServicioSantiagoTrujilloContext : DbContext
    {

        public BaseMicroServicioSantiagoTrujilloContext(DbContextOptions<BaseMicroServicioSantiagoTrujilloContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ECliente> Clientes { get; set; }
        public virtual DbSet<ECuenta> Cuentas { get; set; }
        public virtual DbSet<EMovimiento> Movimientos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ECliente>(entity =>
            {
                entity.HasKey(e => e.ClienteIdCliente);

                entity.ToTable("MICROST_CLIENTE");

                entity.Property(e => e.ClienteIdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.ClienteContrasena)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_CONTRASENA");

                entity.Property(e => e.PersonaDireccion)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CMP_DIRECCION");

                entity.Property(e => e.PersonaEdad).HasColumnName("CMP_EDAD");

                entity.Property(e => e.ClienteEstado).HasColumnName("CMP_ESTADO");

                entity.Property(e => e.PersonaGenero)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_GENERO");

                entity.Property(e => e.PersonaIdentificacion)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_IDENTIFICACION");

                entity.Property(e => e.PersonaNombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_NOMBRE");

                entity.Property(e => e.PersonaTelefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CMP_TELEFONO");
            });

            modelBuilder.Entity<ECuenta>(entity =>
            {
                entity.HasKey(e => e.CuentaNumero)
                    .HasName("PK__cuentas__5138EEC71FAE4CF6");

                entity.ToTable("MICROST_CUENTAS");

                entity.Property(e => e.CuentaNumero)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_NUMERO_CUENTA");

                entity.Property(e => e.CuentaEstado).HasColumnName("CMP_ESTADO");

                entity.Property(e => e.CuentaIdCliente).HasColumnName("CMP_ID_CLIENTE");

                entity.Property(e => e.CuentaTipo)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_TIPO");

                entity.HasOne(d => d.CuentaIdClienteNavigation)
                    .WithMany(p => p.Cuenta)
                    .HasForeignKey(d => d.CuentaIdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cuentas_cuentas");
            });

            modelBuilder.Entity<EMovimiento>(entity =>
            {
                entity.HasKey(e => e.MovimientoIdMovimiento)
                    .HasName("PK__movimien__2D96FD74C19E909F");

                entity.ToTable("MICROST_MOVIMIENTOS");

                entity.Property(e => e.MovimientoIdMovimiento).HasColumnName("ID_MOVIMIENTO");

                entity.Property(e => e.MovimientoFecha)
                    .HasColumnType("DATETIME")
                    .HasColumnName("CMP_FECHA");

                entity.Property(e => e.MoMovimiento)
                    .HasColumnType("MONEY")
                    .HasColumnName("CMP_MOVIMIENTO");

                entity.Property(e => e.MovimientoNumeroCuenta)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_NUMERO_CUENTA");

                entity.Property(e => e.MovimientoSaldoDisponible)
                    .HasColumnType("MONEY")
                    .HasColumnName("CMP_SALDO_DISPONIBLE");

                entity.Property(e => e.MovimientoSaldoInicial)
                    .HasColumnType("MONEY")
                    .HasColumnName("CMP_SALDO_INICIAL");

                entity.Property(e => e.MovimientoTipoMovimiento)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CMP_TIPO_MOVIMIENTO");

                entity.HasOne(d => d.MovimientoNumeroCuentaNavigation)
                    .WithMany(p => p.Movimientos)
                    .HasForeignKey(d => d.MovimientoNumeroCuenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_movimientos_cuentas");
            });
        }
    }

}

