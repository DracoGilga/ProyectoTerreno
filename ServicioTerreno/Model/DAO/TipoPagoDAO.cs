using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class TipoPagoDAO
    {
        public static List<TipoPago> ConsultarTipoPago()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<TipoPago> tipoPagos = new List<TipoPago>();
                IQueryable<TipoPago> consulta = DBConexion.TipoPago;
                
                if(consulta != null)
                {
                    foreach (TipoPago tipoPago in consulta)
                    {
                        tipoPagos.Add(new TipoPago()
                        {
                            IdTipoPago = tipoPago.IdTipoPago,
                            Descripcion = tipoPago.Descripcion
                        });
                    }
                    return tipoPagos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}