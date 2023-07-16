using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model
{
    public class ContratoPersona
    {
        public Persona Persona { get; set; }
        public Contrato Contrato { get; set; }
    }
}