using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicioTerreno.Model.DAO
{
    public class ManzanaDAO
    {
        public Boolean RegistrarManzana(Manzana manzana)
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
        public Boolean ModificarManzana(Manzana manzana)
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
        public Boolean EliminarManzana(int idManzana)
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


        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}