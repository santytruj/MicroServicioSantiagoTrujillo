using MicroservicioSantiagoTrujillo.AccesoDatos;
using MicroServicioSantiagoTrujillo.Controllers;
using MicroServicioSantiagoTrujillo.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MicroservicioSantiagoTrujillo.PruebasUnitarias
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test()
        {
            var option = new DbContextOptionsBuilder<BaseMicroServicioSantiagoTrujilloContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;


            var dbContext = new BaseMicroServicioSantiagoTrujilloContext(option);

            dbContext.Clientes.Add(new ECliente()
            {
                PersonaIdentificacion = "1234567890",
                ClienteContrasena = "12345",
                ClienteEstado = true,
                PersonaNombre = "Santiago Trujillo",
                PersonaGenero = "Masculino",
                PersonaEdad = 27,
                PersonaDireccion = "Quito",
                PersonaTelefono = "0945736789"
            });
            dbContext.Clientes.Add(new ECliente()
            {
                PersonaIdentificacion = "1234567890",
                ClienteContrasena = "12345",
                ClienteEstado = true,
                PersonaNombre = "Fernado Teran",
                PersonaGenero = "Masculino",
                PersonaEdad = 27,
                PersonaDireccion = "Quito",
                PersonaTelefono = "09999999"
            });
            _ = await dbContext.SaveChangesAsync();

            var controller = new ClientesController(dbContext);
            var result = await controller.GetClientes();

            var cliente = result.Value;
            Assert.Equal(2, cliente.Count);
        }

        [Fact]
        public async Task Test2()
        {
            var option = new DbContextOptionsBuilder<BaseMicroServicioSantiagoTrujilloContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;


            var dbContext = new BaseMicroServicioSantiagoTrujilloContext(option);

            dbContext.Movimientos.Add(new EMovimiento()
            {
                MovimientoNumeroCuenta = "11111",
                MovimientoFecha = DateTime.Now,
                MovimientoTipoMovimiento = "Deposito",
                MovimientoSaldoInicial = 1000,
                MoMovimiento = 100,
                MovimientoSaldoDisponible = 1100

            });
            dbContext.Movimientos.Add(new EMovimiento()
            {
                MovimientoNumeroCuenta = "2222",
                MovimientoFecha = DateTime.Now,
                MovimientoTipoMovimiento = "Deposito",
                MovimientoSaldoInicial = 2000,
                MoMovimiento = 200,
                MovimientoSaldoDisponible = 2200

            });
            _ = await dbContext.SaveChangesAsync();

            var controller = new MovimientosController(dbContext);
            var result = await controller.GetMovimientos();

            var cliente = result.Value;
            Assert.Equal(2, cliente.Count);
        }

    }
}
