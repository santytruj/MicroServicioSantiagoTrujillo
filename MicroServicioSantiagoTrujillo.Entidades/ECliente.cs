using System;
using System.Collections.Generic;

namespace MicroServicioSantiagoTrujillo.Entidades
{
    public class ECliente : EPersona
    {
        public ECliente()
        {
            Cuenta = new HashSet<ECuenta>();
        }
        public int ClienteIdCliente { get; set; }
        public string ClienteContrasena { get; set; }
        public bool ClienteEstado { get; set; }
        public virtual ICollection<ECuenta> Cuenta { get; set; }
    }
}
