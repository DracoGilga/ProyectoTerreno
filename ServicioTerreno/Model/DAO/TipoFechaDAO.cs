using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class TipoFechaDAO
    {
        public static List<TipoFecha> ConsultarTipoFecha()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<TipoFecha> tipoFechas = new List<TipoFecha>();
                IQueryable<TipoFecha> consulta = DBConexion.TipoFecha;
                
                if(consulta != null)
                {
                    foreach (TipoFecha tipoFecha in consulta)
                    {
                        tipoFechas.Add(new TipoFecha()
                        {
                            IdTipoFecha = tipoFecha.IdTipoFecha,
                            Descripcion = tipoFecha.Descripcion
                        });
                    }
                    return tipoFechas;
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