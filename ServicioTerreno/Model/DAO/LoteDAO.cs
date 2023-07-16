using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class LoteDAO
    {
        public static Boolean RegistrarLote(Lote loteNuevo)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                var lote = new Lote()
                {
                    NoLote = loteNuevo.NoLote,
                    Superficie = loteNuevo.Superficie,
                    IdManzana = loteNuevo.IdManzana
                };
                DBConexion.Lote.InsertOnSubmit(lote);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Boolean ModificarLote(Lote lote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Lote loteModificar = DBConexion.Lote.Where(p => p.IdLote == lote.IdLote).First();
                loteModificar.NoLote = lote.NoLote;
                loteModificar.Superficie = lote.Superficie;
                loteModificar.IdContrato = lote.IdContrato;

                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarContratosLote(int IdContrato, int IdLote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Lote loteModificar = DBConexion.Lote.Where(p => p.IdLote == IdLote).First();
                loteModificar.IdContrato = IdContrato;
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarLote(int idLote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Lote loteEliminar = DBConexion.Lote.Where(p => p.IdLote == idLote).First();
                DBConexion.Lote.DeleteOnSubmit(loteEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<Lote> ConsultarLoteManzana(int IdManzana)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Lote> lotes = new List<Lote>();
                IQueryable<Lote> Consultar = DBConexion.Lote.Where(p => p.Manzana.IdManzana == IdManzana);
                if (Consultar != null)
                {
                    foreach (Lote lote in Consultar)
                    {
                        lotes.Add(new Lote()
                        {
                            IdLote = lote.IdLote,
                            NoLote = lote.NoLote,
                            Superficie = lote.Superficie,
                            IdContrato = lote.IdContrato,
                        });
                    }
                    return lotes;
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
        public static List<Lote> ConsultarLoteContrato(int IdContrato)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Lote> lotes = new List<Lote>();
                IQueryable<Lote> Consultar = DBConexion.Lote.Where(p => p.IdContrato == IdContrato);
                if (Consultar != null)
                {
                    foreach (Lote lote in Consultar)
                    {
                        lotes.Add(new Lote()
                        {
                            IdLote = lote.IdLote,
                            NoLote = lote.NoLote,
                            Superficie = lote.Superficie,
                            IdContrato = lote.IdContrato,
                        });
                    }
                    return lotes;
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

        public static Boolean EliminarLoteContrato(int IdLote)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Lote loteEliminar = DBConexion.Lote.Where(p => p.IdLote == IdLote).First();
                loteEliminar.IdContrato = null;
                DBConexion.SubmitChanges();
                if (loteEliminar != null) 
                    return true;
                else
                    return false;
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