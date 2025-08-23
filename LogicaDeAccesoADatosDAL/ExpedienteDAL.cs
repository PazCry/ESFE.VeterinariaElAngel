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
    public class ExpedienteDAL
    {
            public static List<ExpedienteEN> MostrarExpe()
            {
                List<ExpedienteEN> _Lista = new List<ExpedienteEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ListarExpediente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new ExpedienteEN
                        {
                            Id = _reader.GetByte(0),
                            IdCliente = _reader.GetByte(1),
                            IdMascota = _reader.GetByte(2),
                            Fecha = _reader.GetDateTime(3),
                            DescripcionConsulta = _reader.GetString(4),
                            Estado = _reader.GetString(5)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }
            public static List<ExpedienteEN> BuscarExpe(String Id)
            {
                List<ExpedienteEN> _Lista = new List<ExpedienteEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("BuscarExpe", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.AddWithValue("@Id", Id);

                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new ExpedienteEN
                        {
                            Id = _reader.GetByte(0),
                            IdCliente = _reader.GetByte(1),
                            IdMascota = _reader.GetByte(2),
                            Fecha = _reader.GetDateTime(3),
                            DescripcionConsulta = _reader.GetString(4),
                            Estado = _reader.GetString(5)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }

            public static int AgregarExpe(ExpedienteEN pRegistroEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarExpe", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@IdCliente", pRegistroEN.IdCliente));
                    _comando.Parameters.Add(new SqlParameter("@IdMascota", pRegistroEN.IdMascota));
                    _comando.Parameters.Add(new SqlParameter("@FechaAtencion", pRegistroEN.Fecha));
                    _comando.Parameters.Add(new SqlParameter("@DescripcionConsulta", pRegistroEN.DescripcionConsulta));
                    _comando.Parameters.Add(new SqlParameter("@Estado", pRegistroEN.Estado));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int EliminarExpe(ExpedienteEN pRegistroEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("EliminarExpe", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pRegistroEN.Id));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int ModificarExpe(ExpedienteEN pRegistroEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ModificarExpe", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pRegistroEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@IdCliente", pRegistroEN.IdCliente));
                    _comando.Parameters.Add(new SqlParameter("@IdMascota", pRegistroEN.IdMascota));
                    _comando.Parameters.Add(new SqlParameter("@FechaAtencion", pRegistroEN.Fecha));
                    _comando.Parameters.Add(new SqlParameter("@DescripcionConsulta", pRegistroEN.DescripcionConsulta));
                    _comando.Parameters.Add(new SqlParameter("@Estado", pRegistroEN.Estado));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }

            }
        }
    }