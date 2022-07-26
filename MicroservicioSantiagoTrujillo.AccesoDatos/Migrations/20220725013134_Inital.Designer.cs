﻿// <auto-generated />
using System;
using MicroservicioSantiagoTrujillo.AccesoDatos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MicroservicioSantiagoTrujillo.AccesoDatos.Migrations
{
    [DbContext(typeof(BaseMicroServicioSantiagoTrujilloContext))]
    [Migration("20220725013134_Inital")]
    partial class Inital
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.ECliente", b =>
                {
                    b.Property<int>("ClienteIdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_CLIENTE")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClienteContrasena")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_CONTRASENA");

                    b.Property<bool>("ClienteEstado")
                        .HasColumnType("bit")
                        .HasColumnName("CMP_ESTADO");

                    b.Property<string>("PersonaDireccion")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("CMP_DIRECCION");

                    b.Property<int>("PersonaEdad")
                        .HasColumnType("int")
                        .HasColumnName("CMP_EDAD");

                    b.Property<string>("PersonaGenero")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_GENERO");

                    b.Property<string>("PersonaIdentificacion")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_IDENTIFICACION");

                    b.Property<string>("PersonaNombre")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_NOMBRE");

                    b.Property<string>("PersonaTelefono")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("CMP_TELEFONO");

                    b.HasKey("ClienteIdCliente");

                    b.ToTable("MICROST_CLIENTE");
                });

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.ECuenta", b =>
                {
                    b.Property<string>("CuentaNumero")
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_NUMERO_CUENTA");

                    b.Property<bool>("CuentaEstado")
                        .HasColumnType("bit")
                        .HasColumnName("CMP_ESTADO");

                    b.Property<int>("CuentaIdCliente")
                        .HasColumnType("int")
                        .HasColumnName("CMP_ID_CLIENTE");

                    b.Property<string>("CuentaTipo")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_TIPO");

                    b.HasKey("CuentaNumero")
                        .HasName("PK__cuentas__5138EEC71FAE4CF6");

                    b.HasIndex("CuentaIdCliente");

                    b.ToTable("MICROST_CUENTAS");
                });

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.EMovimiento", b =>
                {
                    b.Property<int>("MovimientoIdMovimiento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID_MOVIMIENTO")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("MoMovimiento")
                        .HasColumnType("MONEY")
                        .HasColumnName("CMP_MOVIMIENTO");

                    b.Property<DateTime>("MovimientoFecha")
                        .HasColumnType("DATETIME")
                        .HasColumnName("CMP_FECHA");

                    b.Property<string>("MovimientoNumeroCuenta")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_NUMERO_CUENTA");

                    b.Property<decimal>("MovimientoSaldoDisponible")
                        .HasColumnType("MONEY")
                        .HasColumnName("CMP_SALDO_DISPONIBLE");

                    b.Property<decimal>("MovimientoSaldoInicial")
                        .HasColumnType("MONEY")
                        .HasColumnName("CMP_SALDO_INICIAL");

                    b.Property<string>("MovimientoTipoMovimiento")
                        .IsRequired()
                        .HasMaxLength(30)
                        .IsUnicode(false)
                        .HasColumnType("varchar(30)")
                        .HasColumnName("CMP_TIPO_MOVIMIENTO");

                    b.HasKey("MovimientoIdMovimiento")
                        .HasName("PK__movimien__2D96FD74C19E909F");

                    b.HasIndex("MovimientoNumeroCuenta");

                    b.ToTable("MICROST_MOVIMIENTOS");
                });

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.ECuenta", b =>
                {
                    b.HasOne("MicroServicioSantiagoTrujillo.Entidades.ECliente", "CuentaIdClienteNavigation")
                        .WithMany("Cuenta")
                        .HasForeignKey("CuentaIdCliente")
                        .HasConstraintName("FK_cuentas_cuentas")
                        .IsRequired();

                    b.Navigation("CuentaIdClienteNavigation");
                });

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.EMovimiento", b =>
                {
                    b.HasOne("MicroServicioSantiagoTrujillo.Entidades.ECuenta", "MovimientoNumeroCuentaNavigation")
                        .WithMany("Movimientos")
                        .HasForeignKey("MovimientoNumeroCuenta")
                        .HasConstraintName("FK_movimientos_cuentas")
                        .IsRequired();

                    b.Navigation("MovimientoNumeroCuentaNavigation");
                });

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.ECliente", b =>
                {
                    b.Navigation("Cuenta");
                });

            modelBuilder.Entity("MicroServicioSantiagoTrujillo.Entidades.ECuenta", b =>
                {
                    b.Navigation("Movimientos");
                });
#pragma warning restore 612, 618
        }
    }
}
