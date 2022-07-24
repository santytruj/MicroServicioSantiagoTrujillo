using MicroservicioSantiagoTrujillo.AccesoDatos;
using MicroServicioSantiagoTrujillo.Entidades;
using MicroServicioSantiagoTrujillo.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicioSantiagoTrujillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly BaseMicroServicioSantiagoTrujilloContext _context;

        public MovimientosController(BaseMicroServicioSantiagoTrujilloContext context)
        {
            _context = context;
        }

        // GET: api/Movimientos
        [HttpGet]
        public async Task<ActionResult<List<EMovimiento>>> GetMovimientos()
        {
            return await _context.Movimientos.ToListAsync();
        }

        // GET: api/Movimientos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EMovimiento>> GetMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);

            if (movimiento == null)
            {
                return NotFound();
            }

            return movimiento;
        }

        // PUT: api/Movimientos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Respuesta> PutMovimiento(int id, EMovimiento movimiento)
        {
            Respuesta respuesta = new Respuesta();
            if (id != movimiento.MovimientoIdMovimiento)
            {
                respuesta.Mensaje = "No se puedo eliminar la cuenta";
                return respuesta;
            }

            _context.Entry(movimiento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                respuesta.Mensaje = "Movimiento eliminado";
                respuesta.Resultado = false;
                return respuesta;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!MovimientoExists(id))
                {
                    respuesta.Mensaje = ex.Message;
                    return respuesta;
                }
                else
                {
                    respuesta.Mensaje = ex.Message;
                    return respuesta;
                }
            }
        }

        // POST: api/Movimientos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostMovimiento(EMovimiento movimiento)
        {
            Respuesta respuesta = new Respuesta();
            movimiento.MovimientoFecha = DateTime.Now;
            #region Validacion campos vacios
            if (movimiento.MoMovimiento <= 0)
            {
                respuesta.StatusSuccess = true;
                respuesta.Mensaje = "No se permite valores 0";
                return respuesta;
            }
            #endregion

            EMovimiento ultimoMovimiento = await _context.Movimientos.Where(x => x.MovimientoNumeroCuenta == movimiento.MovimientoNumeroCuenta).OrderByDescending(x => x.MovimientoFecha).FirstOrDefaultAsync();
            if (ultimoMovimiento != null)
            {
                #region Validacion de Cupos
                if (string.Equals(movimiento.MovimientoTipoMovimiento, "Debito"))
                {
                    if (!ValidarCupos(movimiento.MovimientoNumeroCuenta, movimiento.MoMovimiento).StatusSuccess)
                    {
                        return respuesta;
                    }
                }
                #endregion
                #region Validacion de Saldos y Registro de movimientos
                if (ultimoMovimiento.MovimientoSaldoDisponible < Math.Abs(movimiento.MoMovimiento))
                {
                    respuesta.StatusSuccess = true;
                    respuesta.Mensaje = "Saldo no disponible";
                }
                else
                {
                    movimiento.MovimientoSaldoInicial = ultimoMovimiento.MovimientoSaldoDisponible;
                    if (string.Equals(movimiento.MovimientoTipoMovimiento, "Debito"))
                    {
                        movimiento.MovimientoSaldoDisponible = ultimoMovimiento.MovimientoSaldoDisponible - movimiento.MoMovimiento;
                        movimiento.MoMovimiento = Math.Abs(movimiento.MoMovimiento) * (-1);
                    }
                    else
                        movimiento.MovimientoSaldoDisponible = ultimoMovimiento.MovimientoSaldoDisponible + movimiento.MoMovimiento;
                    respuesta.StatusSuccess = true;
                    _context.Movimientos.Add(movimiento);
                    await _context.SaveChangesAsync();
                }
                #endregion
            }
            else
            {
                movimiento.MovimientoSaldoDisponible = ultimoMovimiento.MovimientoSaldoDisponible + movimiento.MoMovimiento;
                respuesta.StatusSuccess = true;
                _context.Movimientos.Add(movimiento);
                await _context.SaveChangesAsync();
            }

            respuesta.Resultado = movimiento;
            return respuesta;
        }

        // DELETE: api/Movimientos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovimiento(int id)
        {
            var movimiento = await _context.Movimientos.FindAsync(id);
            if (movimiento == null)
            {
                return NotFound();
            }

            _context.Movimientos.Remove(movimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovimientoExists(int id)
        {
            return _context.Movimientos.Any(e => e.MovimientoIdMovimiento == id);
        }

        [HttpPost("{strNumeroCuenta}&{decMontoTransaccion}")]
        public Respuesta ValidarCupos(string strNumeroCuenta, decimal decMontoTransaccion)
        {
            Respuesta respuesta = new Respuesta();
            DateTime diaActual = DateTime.Now;

            string dia = diaActual.ToString("dd-MM-yyyy");
            DateTime InicioDeDia = DateTime.ParseExact(dia, "dd-MM-yyyy", null);
            DateTime FinalDeDia = DateTime.ParseExact(dia + " 23:59:59", "dd-MM-yyyy HH:mm:ss", null);

            decimal valoresRetiro = _context.Movimientos
                .Where(x => x.MovimientoNumeroCuenta == strNumeroCuenta
                && x.MovimientoFecha >= InicioDeDia
                && x.MovimientoFecha <= FinalDeDia
                && x.MovimientoTipoMovimiento == "Debito")
                .Sum(a => a.MoMovimiento);

            decimal totalsupuesto = Math.Abs(valoresRetiro) + Math.Abs(decMontoTransaccion);

            respuesta.Resultado = Math.Abs(totalsupuesto);
            if (totalsupuesto > 1000)
            {
                respuesta.Mensaje = "Cupo diario excedido";
                respuesta.StatusSuccess = false;
                respuesta.Resultado = Math.Abs(valoresRetiro);
            }
            else
                respuesta.StatusSuccess = true;
            return respuesta;
        }
        [HttpGet("{strIdentificacion}&{FechaInicio}&{FechaFin}")]
        public async Task<Respuesta> PostPMovimientosfechas(string strIdentificacion, string FechaInicio, string FechaFin)
        {
            List<EMovimiento> lstPMovimiento = new List<EMovimiento>();
            Respuesta respuesta = new Respuesta();
            try
            {
                respuesta.Resultado = await _context.Movimientos.Select(x => new MovimientosCliente
                {
                    Fecha = x.MovimientoFecha,
                    Nombre = x.MovimientoNumeroCuentaNavigation.CuentaIdClienteNavigation.PersonaNombre,
                    NumeroCuenta = x.MovimientoNumeroCuentaNavigation.CuentaNumero,
                    Tipo = x.MovimientoNumeroCuentaNavigation.CuentaTipo,
                    SaldoInicial = x.MovimientoSaldoInicial,
                    Estado = x.MovimientoNumeroCuentaNavigation.CuentaIdClienteNavigation.ClienteEstado,
                    Movimiento = x.MoMovimiento,
                    SaldoDisponible = x.MovimientoSaldoDisponible,
                    Identificacion = x.MovimientoNumeroCuentaNavigation.CuentaIdClienteNavigation.PersonaIdentificacion,

                }).Where(s => s.Identificacion == strIdentificacion && s.Fecha >= Convert.ToDateTime(FechaInicio) && s.Fecha <= Convert.ToDateTime(FechaFin)).ToListAsync();
                respuesta.StatusSuccess = true;
            }
            catch (Exception e)
            {
                respuesta.Mensaje = e.StackTrace;
            }
            return respuesta;
        }
    }
}
