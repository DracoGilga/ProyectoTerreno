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
    /// Lógica de interacción para EliminarPersona.xaml
    /// </summary>
    public partial class EliminarPersona : Window
    {
        PersonaViewModel personaViewModel = new PersonaViewModel();
        public EliminarPersona()
        {
            InitializeComponent();
            BuscarPersonas();
        }

        private async void Btn_Eliminar(object sender, RoutedEventArgs e)
        {
            if (Cb_BuscarPersona.SelectedIndex != -1)
            {
                int idPersona = ((Persona)Cb_BuscarPersona.SelectedItem).IdPersona;
                bool resultado = await personaViewModel.EliminarPersona(idPersona);
                if (resultado)
                {
                    MessageBox.Show("Persona eliminada correctamente");
                    
                }
                else
                    MessageBox.Show("No se pudo eliminar la persona");
                LimpiarCampos();
                BuscarPersonas();
            }
            else
                MessageBox.Show("Favor de seleccionar una persona");
        }

        private async void BuscarPersonas()
        {
            Cb_BuscarPersona.ItemTemplate = (DataTemplate)Resources["PersonaItemTemplate"];
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
                LimpiarCampos();
            }
        }
        private void LimpiarCampos()
        {
            Tb_nombre.Text = string.Empty;
            Tb_apellidoP.Text = string.Empty;
            Tb_ApellidoM.Text = string.Empty;
            Tb_direccion.Text = string.Empty;
            Tb_telefono.Text = string.Empty;
            Tb_correo.Text = string.Empty;
        }
    }
}
