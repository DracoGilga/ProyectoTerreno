using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class ManzanaDAO
    {
        public static Boolean RegistrarManzana(Manzana manzana)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Manzana.InsertOnSubmit(manzana);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarManzana(Manzana manzana)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Manzana manzanaModificar = DBConexion.Manzana.Where(p => p.IdManzana == manzana.IdManzana).First();
                manzanaModificar.NoManzana = manzana.NoManzana;
                manzanaModificar.IdPredio = manzana.IdPredio;

                DBConexion.SubmitChanges();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarManzana(int idManzana)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Manzana manzanaEliminar = DBConexion.Manzana.Where(p => p.IdManzana == idManzana).First();
                DBConexion.Manzana.DeleteOnSubmit(manzanaEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Manzana> ConsultarManzana(int IdPredio)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Manzana> manzanas = new List<Manzana>();
                IQueryable<Manzana> consulta = DBConexion.Manzana.Where(p => p.IdPredio == IdPredio);
                if(consulta != null)
                {
                    foreach (Manzana pago in consulta)
                    {
                        manzanas.Add(new Manzana()
                        {
                            IdManzana = pago.IdManzana,
                            NoManzana = pago.NoManzana,
                            IdPredio = pago.IdPredio
                        });
                    }
                    return manzanas;
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