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
    public class ClienteDAL
    {
            public static int VerificarClienteLogin(ClienteEN pClienteEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("VerificarCliente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pClienteEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Apellido", pClienteEN.Apellido));
                    _comando.Parameters.Add(new SqlParameter("@Telefono", pClienteEN.Telefono));
                    int clienteExiste = (int?)_comando.ExecuteScalar() ?? 0;
                    _conn.Close();
                    return clienteExiste;
                }
            }


            public static List<ClienteEN> ListarCliente()
            {
                List<ClienteEN> _Lista = new List<ClienteEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ListarCliente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    {
                        while (_reader.Read())
                        {
                            _Lista.Add(new ClienteEN
                            {
                                Id = _reader.GetByte(0),
                                Nombre = _reader.GetString(1),
                                Apellido = _reader.GetString(2),
                                Telefono = Convert.ToInt64(_reader.GetValue(3)), // si es varchar en la BD
                                FechaCreacion = _reader.GetDateTime(4)
                            });
                        }

                    }
                    _conn.Close();
                }
                return _Lista;
            }

            public static int AgregarCliente(ClienteEN pClienteEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarCliente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pClienteEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Apellido", pClienteEN.Apellido));
                    _comando.Parameters.Add(new SqlParameter("@Telefono", pClienteEN.Telefono));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }
            public static bool TelefonoExiste(string telefono)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ExisteTelefono", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Telefono", telefono));
                    int count = (int)_comando.ExecuteScalar();
                    _conn.Close();
                    return count > 0;
                }
            }




            public static int EliminarCliente(ClienteEN pClienteEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("EliminarCliente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pClienteEN.Id));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }
            public static List<ClienteEN> BuscarCliente(String Nombre)
            {
                List<ClienteEN> _Lista = new List<ClienteEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("BuscarCliente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.AddWithValue("@Nombre", Nombre);

                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new ClienteEN
                        {
                            Id = _reader.GetByte(0),                  // TINYINT
                            Nombre = _reader.GetString(1),
                            Apellido = _reader.GetString(2),

                            // Telefono: adaptamos según tipo
                            Telefono = long.TryParse(_reader.GetValue(3).ToString(), out long tel) ? tel : 0,

                            // FechaCreacion: validación segura
                            FechaCreacion = _reader.GetFieldType(4) == typeof(DateTime)
        ? _reader.GetDateTime(4)
        : DateTime.Parse(_reader.GetString(4))
                        });
                    }
                    _conn.Close();
                }
                return _Lista;

            }

            public static int ModificarCliente(ClienteEN pClienteEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ModificarCliente", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pClienteEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pClienteEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Apellido", pClienteEN.Apellido));
                    _comando.Parameters.Add(new SqlParameter("@Telefono", pClienteEN.Telefono));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

        }
    }
