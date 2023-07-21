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
        private string endpointAddress = "http://192.168.100.7:81/Service1.svc";
        private Service1Client CreateServiceClient()
        {
            return new Service1Client(new System.ServiceModel.BasicHttpBinding(), new System.ServiceModel.EndpointAddress(endpointAddress));
        }
        public async Task<List<TipoFecha>> tipoFechas()
        {
            Service1Client servicio = CreateServiceClient();
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
            Service1Client servicio = CreateServiceClient();
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
