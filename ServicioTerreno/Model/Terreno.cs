using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model
{
    public class Terreno
    {
        public string Nombre { get; set; }
        public string Ubicacion { get; set; }
        public int NoManzana { get; set; }
        public string NoLote { get; set; }
        public double Superficie { get; set; }
        public int? IdContrato { get; set; }
    }
}