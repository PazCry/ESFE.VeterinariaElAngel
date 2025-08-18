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
    public class CitaDAL
    {
         public static CitaEN ObtenerCitaId(int id)
            {
                CitaEN cita = null;
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ObtenerCitaId", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", id));

                    IDataReader _reader = _comando.ExecuteReader();
                    if (_reader.Read())
                    {
                        cita = new CitaEN
                        {
                            IdExpediente = _reader.GetByte(0),
                            FechaCita = _reader.GetDateTime(1),
                            Hora = (TimeSpan)_reader["Hora"] // Solo esta línea cambia de método a indexador
                        };
                    }

                    _conn.Close();
                }

                return cita;
            }


            public static List<CitaEN> MostrarCita()
            {
                List<CitaEN> lista = new List<CitaEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ListarCita", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        lista.Add(new CitaEN
                        {
                            Id = _reader.GetByte(0),
                            IdExpediente = _reader.GetByte(1),
                            FechaCita = _reader.GetDateTime(2),
                            Hora = (TimeSpan)_reader["Hora"] // Solo esta línea cambia de método a indexador
                        });
                    }
                    _conn.Close();
                }
                return lista;
            }


            public static List<CitaEN> BuscarCita(string Id)
            {
                List<CitaEN> _Lista = new List<CitaEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("BuscarCita", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.AddWithValue("@Id", Id); // Ya es número

                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new CitaEN
                        {
                            Id = _reader.GetByte(0),                    // TINYINT → GetByte
                            IdExpediente = _reader.GetByte(1),                // TINYINT → GetByte
                            FechaCita = _reader.GetDateTime(2),         // DATE → GetDateTime
                            Hora = (TimeSpan)_reader.GetValue(3)        // TIME → GetValue y casteo a TimeSpan
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }
            public static int AgregarCita(CitaEN pCitaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarCita", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@IdExpe", pCitaEN.IdExpediente));
                    _comando.Parameters.Add(new SqlParameter("@FechaCita", pCitaEN.FechaCita)); // No acepta null
                    _comando.Parameters.Add(new SqlParameter("@Hora", pCitaEN.Hora));           // No acepta null

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int EliminarCita(CitaEN pCitaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("EliminarCita", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pCitaEN.Id));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int ModificarCita(CitaEN pCitaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ModificarCita", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pCitaEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@IdExpe", pCitaEN.IdExpediente));
                    _comando.Parameters.Add(new SqlParameter("@FechaCita", pCitaEN.FechaCita)); // No acepta null
                    _comando.Parameters.Add(new SqlParameter("@Hora", pCitaEN.Hora));           // No acepta null

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }
            public static bool VerificarCitaPorFechaYHora(DateTime fecha, TimeSpan hora)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();

                    SqlCommand _comando = new SqlCommand(@"  SELECT COUNT(*)   FROM Cita  WHERE CAST(FechaCita AS DATE) = @FechaCita AND Hora = @Hora", _conn as SqlConnection);

                    _comando.Parameters.Add(new SqlParameter("@FechaCita", fecha.Date));
                    _comando.Parameters.Add(new SqlParameter("@Hora", hora));

                    int count = (int)_comando.ExecuteScalar();

                    _conn.Close();
                    return count > 0;
                }
            }
        }
    }