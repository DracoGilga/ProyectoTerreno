using FrontTerreno.Modelo;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }
        private async void Btn_login(object sender, RoutedEventArgs e)
        {
            UsuarioViewModel usuarioViewModel = new UsuarioViewModel();
            Usuario usuario = await usuarioViewModel.InicioSesion(Tb_usuario.Text, Tb_contrasenia.Password);
            if (usuario != null)
            {
                Menu menu = new Menu();
                menu.Show();
                this.Close();
            }
            else
                MessageBox.Show("Usuario o contraseña incorrectos");
        }
    }
}
