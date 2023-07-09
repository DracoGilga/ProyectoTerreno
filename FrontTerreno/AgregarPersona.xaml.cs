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

namespace FrontTerreno
{
    /// <summary>
    /// Lógica de interacción para AgregarPersona.xaml
    /// </summary>
    public partial class AgregarPersona : Window
    {
        public AgregarPersona()
        {
            InitializeComponent();
        }

        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(Tb_nombre.Text) && !string.IsNullOrWhiteSpace(Tb_apellidoP.Text) 
                && !string.IsNullOrWhiteSpace(Tb_apellidoM.Text) && !string.IsNullOrWhiteSpace(Tb_direccion.Text) 
                && !string.IsNullOrWhiteSpace(Tb_telefono.Text))
            {
                Persona persona = new Persona();
                persona.Nombre = Tb_nombre.Text;
                persona.ApellidoPaterno = Tb_apellidoP.Text;
                persona.ApellidoMaterno = Tb_apellidoM.Text;
                persona.Direccion = Tb_direccion.Text;
                persona.Telefono = Tb_telefono.Text;
                persona.Correo = Tb_correo.Text;

                PersonaViewModel personaViewModel = new PersonaViewModel();
                Boolean resultado = await personaViewModel.GuardarPersona(persona);
                if (resultado)
                {
                    MessageBox.Show("Persona registrada correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al registrar persona");
            }
            else
                MessageBox.Show("Favor de llenar todos los campos necesarios");
            
        }
    }
}
