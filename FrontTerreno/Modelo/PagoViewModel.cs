using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Modelo
{
    internal class PagoViewModel
    {
        public ReadOnlyObservableCollection<Pago> pagos { get; set; }
        public PagoViewModel()
        {

        }
        public async Task<Boolean> GuardarPago(Pago pago)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                Boolean resultado = await servicio.RegistrarPagoAsync(pago);
                if (resultado != null)
                    return resultado;
                else
                    return false;
            }
            else
                return true;
        }
        public async Task<Boolean> ModificarPago(Pago pago)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                Boolean resultado = await servicio.ModificarPagoAsync(pago);
                if (resultado)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<List<Pago>> Listapagos()
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                Pago[] resultado = await servicio.ListarPagoAsync();
                if (resultado != null)
                {
                    List<Pago> lista = new List<Pago>(resultado);
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<Pago> BuscarPago (string folio)
        {
            Service1Client servicio = new Service1Client();
            if(servicio != null)
            {
                Pago resultado = await servicio.BuscarPagoAsync(folio);
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
