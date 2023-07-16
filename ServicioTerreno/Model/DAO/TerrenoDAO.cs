using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class TerrenoDAO
    {
        public static List<Terreno> ListaTerrenos()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();

                IQueryable<Terreno> consulta = from lote in DBConexion.Lote
                                               join manzana in DBConexion.Manzana on lote.IdManzana equals manzana.IdManzana
                                               join predio in DBConexion.Predio on manzana.IdPredio equals predio.IdPredio
                                               select new Terreno
                                               {
                                                   Nombre = predio.Nombre,
                                                   Ubicacion = predio.Ubicacion,
                                                   NoManzana = manzana.NoManzana,
                                                   NoLote = lote.NoLote,
                                                   Superficie = lote.Superficie,
                                                   IdContrato = lote.IdContrato
                                               };

                List<Terreno> resultados = consulta.ToList();

                return resultados;
            }
            catch (Exception)
            {
                return null; 
            }
        }
        public static List<Terreno> ListaTerrenosContrato(int idContrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();

                IQueryable<Terreno> consulta = from lote in DBConexion.Lote
                                               join manzana in DBConexion.Manzana on lote.IdManzana equals manzana.IdManzana
                                               join predio in DBConexion.Predio on manzana.IdPredio equals predio.IdPredio
                                               where lote.IdContrato == idContrato
                                               select new Terreno
                                               {
                                                   IdLote = lote.IdLote,
                                                   Nombre = predio.Nombre,
                                                   Ubicacion = predio.Ubicacion,
                                                   NoManzana = manzana.NoManzana,
                                                   NoLote = lote.NoLote,
                                                   Superficie = lote.Superficie,
                                                   IdContrato = lote.IdContrato
                                               };

                List<Terreno> resultados = consulta.ToList();

                return resultados;
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