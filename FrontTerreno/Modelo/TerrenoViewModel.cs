using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Modelo
{
    internal class TerrenoViewModel
    {
        public ObservableCollection<Predio> predios { get; set; }
        public ObservableCollection<Manzana> manzanas { get; set; }
        public ObservableCollection<Lote> lotes { get; set; }

        public TerrenoViewModel()
        {

        }
        //agregar
        public async Task<Boolean> GuardarPredio(Predio predio)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.RegistrarPredioAsync(predio);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> GuardarManzana(Manzana manzana)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.RegistrarManzanaAsync(manzana);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> GuardarLote(Lote lote)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.RegistrarLoteAsync(lote);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }

        //modificar
        public async Task<Boolean> ModificarPredio(Predio predio)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.ModificarPredioAsync(predio);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> ModificarManzana(Manzana manzana)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.ModificarManzanaAsync(manzana);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> ModificarLote(Lote lote)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.ModificarLoteAsync(lote);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }

        //eliminar
        public async Task<Boolean> EliminarPredio(int idPredio)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.EliminarPredioAsync(idPredio);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> EliminarManzana(int idManzana)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.EliminarManzanaAsync(idManzana);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> EliminarLote(int idLote)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                bool resultado = await servicio.EliminarLoteAsync(idLote);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }

        //consultar
        public async Task<List<Predio>> ListaPredios()
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                Predio[] consulta = await servicio.ListarPredioAsync();
                if (consulta != null)
                {
                    List<Predio> lista = new List<Predio>(consulta);
                    return lista;
                }  
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<List<Manzana>> ListaManzanas(int idPredio)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                Manzana[] consulta = await servicio.ListarManzanaAsync(idPredio);
                if (consulta != null)
                {
                    List<Manzana> lista = new List<Manzana>(consulta);
                    return lista;
                }
                else
                    return null;
            }
            else
                return null;
        }
        public async Task<List<Lote>> ListaLotes(int idManzana)
        {
            Service1Client servicio = new Service1Client();
            if (servicio != null)
            {
                Lote[] consulta = await servicio.ListarLoteManzanaAsync(idManzana);
                if (consulta != null)
                {
                    List<Lote> lista = new List<Lote>(consulta);
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
