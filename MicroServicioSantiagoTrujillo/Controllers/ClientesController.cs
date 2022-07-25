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
    public class ClientesController : ControllerBase
    {

        private readonly BaseMicroServicioSantiagoTrujilloContext _context;

        public ClientesController(BaseMicroServicioSantiagoTrujilloContext context) => _context = context;

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<List<ECliente>>> GetClientes()
        {
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ECliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            return cliente == null ? NotFound() : cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<Respuesta> PutCliente(int id, ECliente cliente)
        {
            Respuesta respuesta = new Respuesta();
            if (id != cliente.ClienteIdCliente)
            {
                respuesta.Mensaje = "Cliente ya existe con la identificacion ingresada";
                return respuesta;
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                respuesta.Mensaje = "Cliente modificado Correctamente";
                respuesta.Resultado = true;
                return respuesta;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ClienteExists(cliente.PersonaIdentificacion))
                {
                    respuesta.Mensaje = ex.Message;
                    return respuesta;
                }

                respuesta.Mensaje = ex.Message;
                return respuesta;
                
            }

        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<Respuesta> PostCliente(ECliente cliente)
        {
            Respuesta respuesta = new Respuesta();
            if (ClienteExists(cliente.PersonaIdentificacion))
            {
                respuesta.Mensaje = "Cliente no se puedo modificado Correctamente";
                respuesta.StatusSuccess = false;
                return respuesta;
            }
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            respuesta.Mensaje = "Cliente se creo Correctamente";
            respuesta.StatusSuccess = true;
            respuesta.Resultado = cliente;

            return respuesta;
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<Respuesta> DeleteCliente(int id)
        {
            Respuesta respuesta = new Respuesta();
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                respuesta.Mensaje = "Cliente no se puedo eliminar";
                respuesta.Resultado = false;
                return respuesta;
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            respuesta.Mensaje = "Cliente eliminado";
            respuesta.Resultado = false;
            return respuesta;
        }

        private bool ClienteExists(string id) => _context.Clientes.Any(e => e.PersonaIdentificacion == id);
    }
}
