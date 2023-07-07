﻿using System;
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
                IQueryable<Persona> Consulta = DBConexion.Persona;
                if(Consulta != null)
                {
                    foreach (Persona persona in Consulta)
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

        public static DataClassesTerrenosDataContext GetConexion()
        {
            return new DataClassesTerrenosDataContext(global::System.Configuration.
                ConfigurationManager.ConnectionStrings["TerrenosConnectionString"].ConnectionString);
        }
    }
}