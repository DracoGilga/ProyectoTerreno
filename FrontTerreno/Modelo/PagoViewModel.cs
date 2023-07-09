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

    }
}
