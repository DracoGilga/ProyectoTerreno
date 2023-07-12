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
    }
}
