using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model
{
    public class PagosUnion
    {
        public TipoPago TipoPago { get; set; }
        public Pago Pago { get; set; }
        public Contrato Contrato { get; set; }
        public Persona Persona { get; set; }
    }
}