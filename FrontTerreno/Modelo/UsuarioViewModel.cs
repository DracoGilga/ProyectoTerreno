using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontTerreno.Modelo
{
    internal class UsuarioViewModel
    {
        public ObservableCollection<Usuario> usuarios { get; set; }
        public UsuarioViewModel()
        {

        }
        private string endpointAddress = "http://192.168.100.7:81/Service1.svc";
        private Service1Client CreateServiceClient()
        {
            return new Service1Client(new System.ServiceModel.BasicHttpBinding(), new System.ServiceModel.EndpointAddress(endpointAddress));
        }
        public async Task<Usuario> InicioSesion(string usuario, string contrasena)
        {
            Service1Client servicio = CreateServiceClient();
            if (servicio != null)
            {
                Usuario user = await servicio.LoginAsync(usuario, contrasena);
                if (user != null)
                    return user;
                else
                    return null;
            }
            else
                return null;
        }

    }
}
