﻿using System;

namespace MicroServicioSantiagoTrujillo.Utils
{
    public class MovimientosCliente
    {
        public DateTime Fecha { get; set; }
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string NumeroCuenta { get; set; }
        public string Tipo { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }
        public bool Estado { get; set; }
    }
}
