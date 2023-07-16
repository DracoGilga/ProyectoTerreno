using ServiceReference1;
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
        public async Task<int?> GuardarContrato(Contrato contrato)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                int? resultado = await servicio.RegistrarContratoAsync(contrato);
                if (resultado != null)
                    return resultado;
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<Boolean> ModificarContrato(Contrato contrato)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.ModificarContratoAsync(contrato);
                if (resultado)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<List<ContratoPersona>> MostrarContratosPersona()
        {
            Service1Client servicio = new Service1Client();
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
    }
}
