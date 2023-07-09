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
    /// Lógica de interacción para ModificarPersona.xaml
    /// </summary>
    public partial class ModificarPersona : Window
    {
        PersonaViewModel personaViewModel = new PersonaViewModel();
        public ModificarPersona()
        {
            InitializeComponent();
            BuscarPersonas();
        }

        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Tb_nombre.Text) && !string.IsNullOrWhiteSpace(Tb_apellidoP.Text)
                && !string.IsNullOrWhiteSpace(Tb_ApellidoM.Text) && !string.IsNullOrWhiteSpace(Tb_direccion.Text)
                && !string.IsNullOrWhiteSpace(Tb_telefono.Text))
            {
                Persona persona = new Persona();
                persona.IdPersona = ((Persona)Cb_BuscarPersona.SelectedItem).IdPersona;
                persona.Nombre = Tb_nombre.Text;
                persona.ApellidoPaterno = Tb_apellidoP.Text;
                persona.ApellidoMaterno = Tb_ApellidoM.Text;
                persona.Direccion = Tb_direccion.Text;
                persona.Telefono = Tb_telefono.Text;
                persona.Correo = Tb_correo.Text;

                Boolean resultado = await personaViewModel.ModificarPersona(persona);
                if (resultado)
                {
                    MessageBox.Show("Persona Modificada correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al Modificar persona");
            }
            else
                MessageBox.Show("Favor de llenar todos los campos necesarios");
        }
        private async void BuscarPersonas()
        {
            Cb_BuscarPersona.ItemTemplate= (DataTemplate)Resources["PersonaItemTemplate"];
            Cb_BuscarPersona.ItemsSource = await personaViewModel.ListaPersonas();
        }
        private void clic_persona(object sender, SelectionChangedEventArgs e)
        {
            if (Cb_BuscarPersona.SelectedItem != null)
            {
                var personaSeleccionada = (Persona)Cb_BuscarPersona.SelectedItem;
                Tb_nombre.Text = personaSeleccionada.Nombre;
                Tb_apellidoP.Text = personaSeleccionada.ApellidoPaterno;
                Tb_ApellidoM.Text = personaSeleccionada.ApellidoMaterno;
                Tb_direccion.Text = personaSeleccionada.Direccion;
                Tb_telefono.Text = personaSeleccionada.Telefono;
                Tb_correo.Text = personaSeleccionada.Correo;
            }
            else
            {
                // Limpiar los TextBox si no hay persona seleccionada
                Tb_nombre.Text = string.Empty;
                Tb_apellidoP.Text = string.Empty;
                Tb_ApellidoM.Text = string.Empty;
                Tb_direccion.Text = string.Empty;
                Tb_telefono.Text = string.Empty;
                Tb_correo.Text = string.Empty;
            }
        }

    }
}
