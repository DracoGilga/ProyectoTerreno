using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model
{
    public class ContratoUnion
    {
        public Contrato Contrato { get; set; }
        public Persona Persona { get; set; }
        public List<Terreno> Terreno { get; set; }
        public double? Saldo { get; set; }
        public string Descripcion { get; set; }
    }
}