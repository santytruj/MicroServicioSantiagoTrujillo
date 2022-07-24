using MicroservicioSantiagoTrujillo.AccesoDatos;
using MicroServicioSantiagoTrujillo.Entidades;
using MicroServicioSantiagoTrujillo.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServicioSantiagoTrujillo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly BaseMicroServicioSantiagoTrujilloContext _context;

        public CuentasController(BaseMicroServicioSantiagoTrujilloContext context)
        {
            _context = context;
        }

        // GET: api/Cuentas
        [HttpGet]
        public async Task<ActionResult<List<ECuenta>>> GetCuentas()
        {
            return await _context.Cuentas.ToListAsync();
        }

        // GET: api/Cuentas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ECuenta>> GetCuenta(string id)
        {
            var cuenta = await _context.Cuentas.FindAsync(id);

            if (cuenta == null)
            {
                return NotFound();
            }

            return cuenta;
        }

        // PUT: api/Cuentas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Respuesta> PutCuenta(string id, ECuenta cuenta)
        {
            Respuesta respuesta = new Respuesta();
            if (id != cuenta.CuentaNumero)
            {
                respuesta.Mensaje = "la cuenta no existe";
                return respuesta;
            }

            _context.Entry(cuenta).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                respuesta.Mensaje = "Cuenta modificado Correctamente";
                respuesta.Resultado = true;
                return respuesta;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!CuentaExists(id))
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

        // POST: api/Cuentas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Respuesta>> PostCuenta(ECuenta cuenta)
        {
            Respuesta respuesta = new Respuesta();
            if (CuentaExists(cuenta.CuentaNumero))
            {
                respuesta.Mensaje = "cuenta no se puedo modificado Correctamente";
                respuesta.StatusSuccess = false;
                return respuesta;
            }

            try
            {
                await _context.SaveChangesAsync();
                respuesta.Mensaje = "Cuenta se creo Correctamente";
                respuesta.StatusSuccess = true;
                respuesta.Resultado = cuenta;
                return respuesta;
            }
            catch (DbUpdateException ex)
            {
                if (CuentaExists(cuenta.CuentaNumero))
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

        // DELETE: api/Cuentas/5
        [HttpDelete("{id}")]
        public async Task<Respuesta> DeleteCuenta(string id)
        {
            Respuesta respuesta = new Respuesta();
            var cuenta = await _context.Cuentas.FindAsync(id);
            if (cuenta == null)
            {
                respuesta.Mensaje = "No se puedo eliminar la cuenta";
                return respuesta;
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();
            respuesta.Mensaje = "Cuenta eliminado";
            respuesta.Resultado = false;
            return respuesta;
        }

        private bool CuentaExists(string id)
        {
            return _context.Cuentas.Any(e => e.CuentaNumero == id);
        }
    }
}
