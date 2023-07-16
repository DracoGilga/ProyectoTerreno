using System;
using System.Collections.Generic;
using System.Linq;

namespace ServicioTerreno.Model.DAO
{
    public class PersonaDAO
    {
        public static Boolean RegistrarPersona(Persona persona)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                DBConexion.Persona.InsertOnSubmit(persona);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean ModificarPersona(Persona persona)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Persona personaModificar = DBConexion.Persona.Where(p => p.IdPersona == persona.IdPersona).First();
                personaModificar.Nombre = persona.Nombre;
                personaModificar.ApellidoPaterno = persona.ApellidoPaterno;
                personaModificar.ApellidoMaterno = persona.ApellidoMaterno;
                personaModificar.Direccion = persona.Direccion;
                personaModificar.Telefono = persona.Telefono;
                personaModificar.Correo = persona.Correo;

                DBConexion.SubmitChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean EliminarPersona(int idPersona)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Persona personaEliminar = DBConexion.Persona.Where(p => p.IdPersona == idPersona).First();
                DBConexion.Persona.DeleteOnSubmit(personaEliminar);
                DBConexion.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Persona ObtenerPersona(int idPersona)
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                Persona persona = DBConexion.Persona.Where(p => p.IdPersona == idPersona).First();
                return persona;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static List<Persona> ObtenerPersonas()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<Persona> personas = new List<Persona>();
                IQueryable<Persona> consulta = DBConexion.Persona.OrderBy(p => p.Nombre)
                                                              .ThenBy(p => p.ApellidoPaterno)
                                                              .ThenBy(p => p.ApellidoMaterno);
                if (consulta != null)
                {
                    foreach (Persona persona in consulta)
                    {
                        personas.Add(new Persona()
                        {
                            IdPersona = persona.IdPersona,
                            Nombre = persona.Nombre,
                            ApellidoPaterno = persona.ApellidoPaterno,
                            ApellidoMaterno = persona.ApellidoMaterno,
                            Direccion = persona.Direccion,
                            Telefono = persona.Telefono,
                            Correo = persona.Correo
                        });
                    }
                    return personas;
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
        public static List<ContratoPersona> ObtenerPersonasContrato()
        {
            try
            {
                DataClassesTerrenosDataContext DBConexion = GetConexion();
                List<ContratoPersona> resoltado = new List<ContratoPersona>();
                var consulta = from persona in DBConexion.Persona
                               join contrato in DBConexion.Contrato on persona.IdPersona equals contrato.IdCliente
                               orderby persona.Nombre, persona.ApellidoPaterno, persona.ApellidoMaterno
                               select new
                               {
                                   Persona = persona,
                                   Contrato = contrato
                               };
                if (consulta != null)
                {
                    foreach (var persona in consulta)
                    {
                        resoltado.Add(new ContratoPersona()
                        {
                            Persona = new Persona()
                            {
                                IdPersona = persona.Persona.IdPersona,
                                Nombre = persona.Persona.Nombre,
                                ApellidoPaterno = persona.Persona.ApellidoPaterno,
                                ApellidoMaterno = persona.Persona.ApellidoMaterno,
                                Direccion = persona.Persona.Direccion,
                                Telefono = persona.Persona.Telefono,
                                Correo = persona.Persona.Correo
                            },
                            Contrato = new Contrato()
                            {
                                IdContrato = persona.Contrato.IdContrato,
                                TipoPago = persona.Contrato.TipoPago,
                                Costo = persona.Contrato.Costo,
                                IdCliente = persona.Contrato.IdCliente,
                                Testigo1 = persona.Contrato.Testigo1,
                                Testigo2 = persona.Contrato.Testigo2,
                                IdVendedor = persona.Contrato.IdVendedor,
                                FechaContrato = persona.Contrato.FechaContrato,
                                IdTipoFecha = persona.Contrato.IdTipoFecha,
                            }
                        });
                    }
                    return resoltado;
                }
                else
                    return null;
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