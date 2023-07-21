using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Modelo
{
    internal class PersonaViewModel
    {
        public ObservableCollection<Persona> personas { get; set; }
        public PersonaViewModel()
        {

        }
        private string endpointAddress = "http://192.168.100.7:81/Service1.svc";
        private Service1Client CreateServiceClient()
        {
            return new Service1Client(new System.ServiceModel.BasicHttpBinding(), new System.ServiceModel.EndpointAddress(endpointAddress));
        }
        public async Task<Boolean> GuardarPersona(Persona persona)
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                bool resultado = await servicio.RegistrarPersonaAsync(persona);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> ModificarPersona(Persona persona)
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                bool resultado = await servicio.ModificarPersonaAsync(persona);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<Boolean> EliminarPersona(int idPersona)
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                bool resultado = await servicio.EliminarPersonaAsync(idPersona);
                if (resultado)
                    return resultado;
                else
                    return false;
            }
            else
                return false;
        }
        public async Task<List<Persona>> ListaPersonas()
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                Persona[] resultadoArray = await servicio.ListarPersonaAsync();
                if (resultadoArray != null)
                {
                    List<Persona> resultado = resultadoArray.ToList();
                    return resultado;
                }
                else
                    return null;
            }
            else
                return null;
        }

    }
}
