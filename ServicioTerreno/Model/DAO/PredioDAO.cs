using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class PredioDAO
    {
        public static Boolean RegistrarPredio(Predio predio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Predio.InsertOnSubmit(predio);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarPredio(Predio predio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Predio predioModificar = DBConexion.Predio.Where(p => p.IdPredio == predio.IdPredio).First();
                predioModificar.Nombre = predio.Nombre;
                predioModificar.Ubicacion = predio.Ubicacion;

                DBConexion.SubmitChanges();
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarPredio(int idPredio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Predio predioEliminar = DBConexion.Predio.Where(p => p.IdPredio == idPredio).First();
                DBConexion.Predio.DeleteOnSubmit(predioEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Predio> ConsultarPredio()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Predio> predios = new List<Predio>();
                IQueryable<Predio> predio = DBConexion.Predio;
                
                if (predio != null)
                {
                    foreach (Predio prediosConsulta in predio)
                    {
                        predios.Add(new Predio()
                        {
                            IdPredio = prediosConsulta.IdPredio,
                            Nombre = prediosConsulta.Nombre,
                            Ubicacion = prediosConsulta.Ubicacion
                        });
                    }
                    return predios;
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