using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Modelo
{
    internal class TiposViewModel
    {
        public ObservableCollection<TipoPago> tipoPago { get; set; }
        public ObservableCollection<TipoFecha> tipoFecha { get; set;}
        
        public TiposViewModel()
        {

        }
        public async Task<List<TipoFecha>> tipoFechas()
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                TipoFecha[] consulta = await servicio.ListarTipoFechaAsync();
                if (consulta != null)
                {
                    List<TipoFecha> lista = new List<TipoFecha>(consulta);
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<List<TipoPago>> tipoPagos()
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                TipoPago[] consulta = await servicio.ListarTipoPagoAsync();
                if (consulta != null)
                {
                    List<TipoPago> lista = new List<TipoPago>(consulta);
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }
    

    }
}
