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
        public async Task<Usuario> InicioSesion(string usuario, string contrasena)
        {
            Service1Client servicio = new Service1Client();
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
