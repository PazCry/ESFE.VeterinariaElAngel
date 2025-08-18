using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntidadDeNegociosEN;

namespace LogicaDeAccesoADatosDAL
{
    public class CitaCalendarioDAL
    {
            public static List<CitaCalendarioEN> ObtenerCitasConClientes()
            {
                List<CitaCalendarioEN> listaCitas = new List<CitaCalendarioEN>();

                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();

                    string query = @"
           SELECT 
               c.FechaCita,
               c.Hora,
               cl.Nombre,
               cl.Apellido
           FROM Cita c
           INNER JOIN Cliente cl ON c.IdExpe = cl.Id";

                    using (SqlCommand _comando = new SqlCommand(query, _conn as SqlConnection))
                    {
                        using (IDataReader _reader = _comando.ExecuteReader())
                        {
                            while (_reader.Read())
                            {
                                DateTime fecha = _reader.GetDateTime(0);
                                TimeSpan hora = (TimeSpan)_reader.GetValue(1);
                                string nombre = _reader.GetString(2);
                                string apellido = _reader.GetString(3);

                                listaCitas.Add(new CitaCalendarioEN
                                {
                                    FechaCita = fecha,
                                    Hora = hora,
                                    NombreCliente = $"{nombre} {apellido}"
                                });
                            }
                        }
                    }
                }

                return listaCitas;
            }

        }
    }