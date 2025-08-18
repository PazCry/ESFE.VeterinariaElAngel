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
    public class UsuarioDAL
    {
            public static int VerificarUsuarioLogin(UsuarioEN usuarioEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();

                    SqlCommand _comando = new SqlCommand("VerificarUsuario", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;

                    // Se envía el correo y la contraseña tal cual (sin cifrar)
                    _comando.Parameters.AddWithValue("@Correo", usuarioEN.Correo);
                    _comando.Parameters.AddWithValue("@Contra", usuarioEN.Contra);

                    object result = _comando.ExecuteScalar();

                    int idRol = (result != null && result != DBNull.Value) ? Convert.ToInt32(result) : 0;

                    _conn.Close();
                    return idRol;
                }
            }
            public static List<UsuarioEN> MostrarUsuario()
            {
                List<UsuarioEN> _Lista = new List<UsuarioEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("ListarUsuario", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new UsuarioEN
                        {
                            Id = _reader.GetByte(0),              // TINYINT → byte
                            Nombre = _reader.GetString(1),        // VARCHAR → string
                            Apellido = _reader.GetString(2),      // VARCHAR → string
                            Correo = _reader.GetString(3),        // VARCHAR(MAX) → string
                            Contra = _reader.GetString(4),        // VARCHAR(MAX) → string
                            Telefono = _reader.GetString(5),       // BIGINT → long
                            IdRol = _reader.GetByte(6)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }

            public static List<UsuarioEN> MostrarUsuarioDE()
            {
                List<UsuarioEN> _Lista = new List<UsuarioEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("ListarUsuarioDE", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new UsuarioEN
                        {
                            Id = _reader.GetByte(0),              // TINYINT → byte
                            Nombre = _reader.GetString(1),        // VARCHAR → string
                            Apellido = _reader.GetString(2),      // VARCHAR → string
                            Correo = _reader.GetString(3),        // VARCHAR(MAX) → string
                            Contra = _reader.GetString(4),        // VARCHAR(MAX) → string
                            Telefono = _reader.GetString(5),       // BIGINT → long
                            IdRol = _reader.GetByte(6)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }
            public static List<UsuarioEN> BuscarUsuario(String Nombre)
            {
                List<UsuarioEN> _Lista = new List<UsuarioEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("BuscarUsuario", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.AddWithValue("@Nombre", Nombre);

                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new UsuarioEN
                        {
                            Id = _reader.GetByte(0),              // TINYINT → byte
                            Nombre = _reader.GetString(1),        // VARCHAR → string
                            Apellido = _reader.GetString(2),      // VARCHAR → string
                            Correo = _reader.GetString(3),        // VARCHAR(MAX) → string
                            Contra = _reader.GetString(4),        // VARCHAR(MAX) → string
                            Telefono = _reader.GetString(5),       // BIGINT → long
                            IdRol = _reader.GetByte(6)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }
            public static int AgregarUsuario(UsuarioEN pUsuarioEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarUsuario", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pUsuarioEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Apellido", pUsuarioEN.Apellido));
                    _comando.Parameters.Add(new SqlParameter("@Correo", pUsuarioEN.Correo));
                    _comando.Parameters.Add(new SqlParameter("@Contra", pUsuarioEN.Contra));
                    _comando.Parameters.Add(new SqlParameter("@Telefono", pUsuarioEN.Telefono));
                    _comando.Parameters.Add(new SqlParameter("@IdRol", pUsuarioEN.IdRol));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }

            }
            public static int EliminarUsuario(UsuarioEN pUsuarioEN)
            {
                using (IDbConnection _conn =
                    ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("EliminarUsuario", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pUsuarioEN.Id));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }
            public static int ModificarUsuario(UsuarioEN pUsuarioEN)
            {
                using (IDbConnection _conn =
                    ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                        new SqlCommand("ModificarUsuario", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pUsuarioEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pUsuarioEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Apellido", pUsuarioEN.Apellido));
                    _comando.Parameters.Add(new SqlParameter("@Correo", pUsuarioEN.Correo));
                    _comando.Parameters.Add(new SqlParameter("@Contra", pUsuarioEN.Contra));
                    _comando.Parameters.Add(new SqlParameter("@Telefono", pUsuarioEN.Telefono));
                    _comando.Parameters.Add(new SqlParameter("@IdRol", pUsuarioEN.IdRol));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }
            public bool CorreoExiste(string correo)
            {
                using (IDbConnection conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    conn.Open();

                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM Usuario WHERE DECRYPTBYPASSPHRASE('PAZECRET', Correo) = @Correo";

                        IDbDataParameter paramCorreo = cmd.CreateParameter();
                        paramCorreo.ParameterName = "@Correo";
                        paramCorreo.Value = correo;
                        cmd.Parameters.Add(paramCorreo);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            public bool CorreoExiste(string correo, int idUsuario)
            {
                using (IDbConnection conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    conn.Open();

                    using (IDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT COUNT(*) FROM Usuario WHERE DECRYPTBYPASSPHRASE('PAZECRET', Correo) = @Correo AND Id <> @IdUsuario";

                        IDbDataParameter paramCorreo = cmd.CreateParameter();
                        paramCorreo.ParameterName = "@Correo";
                        paramCorreo.Value = correo;
                        cmd.Parameters.Add(paramCorreo);

                        IDbDataParameter paramId = cmd.CreateParameter();
                        paramId.ParameterName = "@IdUsuario";
                        paramId.Value = idUsuario;
                        cmd.Parameters.Add(paramId);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }


        }
    }
