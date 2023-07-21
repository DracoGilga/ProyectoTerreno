using ServiceReference1; // Asegúrate de importar el namespace del servicio WCF
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Modelo
{
    internal class ContratoViewModel
    {
        public ObservableCollection<Contrato> contratos { get; set; }
        public ContratoViewModel()
        {

        }

        private string endpointAddress = "http://192.168.100.7:81/Service1.svc";

        private Service1Client CreateServiceClient()
        {
            return new Service1Client(new System.ServiceModel.BasicHttpBinding(), new System.ServiceModel.EndpointAddress(endpointAddress));
        }

        public async Task<int?> GuardarContrato(Contrato contrato)
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                int? resultado = await servicio.RegistrarContratoAsync(contrato);
                return resultado;
            }
            else
                return null;
        }

        public async Task<bool> ModificarContrato(Contrato contrato)
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                bool resultado = await servicio.ModificarContratoAsync(contrato);
                return resultado;
            }
            else
                return false;
        }

        public async Task<List<ContratoPersona>> MostrarContratosPersona()
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                ContratoPersona[] consulta = await servicio.ListarContratoPersonaAsync();
                if (consulta != null)
                {
                    List<ContratoPersona> lista = new List<ContratoPersona>(consulta);
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }

        public async Task<List<ContratoUnion>> DesplegarListaContratos()
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                ContratoUnion[] consulta = await servicio.ListaContratoUnionAsync();
                if (consulta != null)
                {
                    List<ContratoUnion> lista = new List<ContratoUnion>(consulta);
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
