using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServicioSantiagoTrujillo.Entidades
{
    public class EMovimiento
    {
        public int MovimientoIdMovimiento { get; set; }
        public string MovimientoNumeroCuenta { get; set; }
        public DateTime MovimientoFecha { get; set; }
        public string MovimientoTipoMovimiento { get; set; }
        public decimal MovimientoSaldoInicial { get; set; }
        public decimal MoMovimiento { get; set; }
        public decimal MovimientoSaldoDisponible { get; set; }
        public virtual ECuenta MovimientoNumeroCuentaNavigation { get; set; }
    }
}
