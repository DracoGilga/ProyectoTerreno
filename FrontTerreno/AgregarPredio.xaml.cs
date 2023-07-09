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
    /// Lógica de interacción para AgregarPredio.xaml
    /// </summary>
    public partial class AgregarPredio : Window
    {
        public AgregarPredio()
        {
            InitializeComponent();
        }

        private async void Btn_guardar(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(Tb_nombre.Text) && !string.IsNullOrWhiteSpace(Tb_ubicacion.Text))
            {
                TerrenoViewModel terrenoViewModel = new TerrenoViewModel();
                Predio predio = new Predio();
                predio.Nombre = Tb_nombre.Text;
                predio.Ubicacion = Tb_ubicacion.Text;
                bool resultado = await terrenoViewModel.GuardarPredio(predio);
                if (resultado)
                {
                    MessageBox.Show("Predio registrado correctamente");
                    this.Close();
                }
                else
                    MessageBox.Show("Error al registrar predio");
            }
            else
                MessageBox.Show("Favor de llenar todos los campos necesarios");
        }
    }
}
