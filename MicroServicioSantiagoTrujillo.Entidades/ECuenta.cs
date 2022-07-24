using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicioSantiagoTrujillo.Entidades
{
    public class ECuenta
    {
        public ECuenta()
        {
            Movimientos = new HashSet<EMovimiento>();
        }

        public string CuentaNumero { get; set; }
        public int CuentaIdCliente { get; set; }
        public string CuentaTipo { get; set; }
        public bool CuentaEstado { get; set; }

        public virtual ECliente CuentaIdClienteNavigation { get; set; }
        public virtual ICollection<EMovimiento> Movimientos { get; set; }
    }
}
