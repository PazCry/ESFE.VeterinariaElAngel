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
    public class MascotaDAL
    {
         public static int VerificarMascotaLogin(MascotaEN pMascotaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("VerificarMascota", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pMascotaEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Color", pMascotaEN.Color));
                    _comando.Parameters.Add(new SqlParameter("@FechaNaci", pMascotaEN.FechaNacimiento));
                    _comando.Parameters.Add(new SqlParameter("@IdSexo", pMascotaEN.IdGenero));
                    _comando.Parameters.Add(new SqlParameter("@IdRaza", pMascotaEN.IdRaza));
                    _comando.Parameters.Add(new SqlParameter("@IdEspecie", pMascotaEN.IdEspecie));
                    _comando.Parameters.Add(new SqlParameter("@FechaCreacion", pMascotaEN.FechaCreacion));
                    int mascotaExiste = (int?)_comando.ExecuteScalar() ?? 0;
                    _conn.Close();
                    return mascotaExiste;
                }
            }

            public static List<MascotaEN> MostrarMascota()
            {
                List<MascotaEN> _Lista = new List<MascotaEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("ListarMascota", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new MascotaEN
                        {
                            Id = _reader.GetByte(0),
                            Nombre = _reader.GetString(1),
                            Color = _reader.GetString(2),
                            FechaNacimiento = _reader.GetDateTime(3),
                            IdGenero = _reader.GetByte(4),
                            IdRaza = _reader.GetByte(5),
                            IdEspecie = _reader.GetByte(6),
                            FechaCreacion = _reader.GetDateTime(7)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }
            public static List<MascotaEN> BuscarMascota(String Nombre)
            {
                List<MascotaEN> _Lista = new List<MascotaEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("BuscarMascota", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.AddWithValue("@Nombre", Nombre); // Agrega este parámetro

                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new MascotaEN
                        {
                            Id = _reader.GetByte(0),
                            Nombre = _reader.GetString(1),
                            Color = _reader.GetString(2),
                            FechaNacimiento = _reader.GetDateTime(3),
                            IdGenero = _reader.GetByte(4),
                            IdRaza = _reader.GetByte(5),
                            IdEspecie = _reader.GetByte(6),
                            FechaCreacion = _reader.GetDateTime(7)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }

            public static int AgregarMascota(MascotaEN pMascotaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarMascota", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pMascotaEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Color", pMascotaEN.Color));
                    _comando.Parameters.Add(new SqlParameter("@FechaNaci", pMascotaEN.FechaNacimiento));
                    _comando.Parameters.Add(new SqlParameter("@IdGen", pMascotaEN.IdGenero));
                    _comando.Parameters.Add(new SqlParameter("@IdRaza", pMascotaEN.IdRaza));
                    _comando.Parameters.Add(new SqlParameter("@IdEspecie", pMascotaEN.IdEspecie));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int EliminarMascota(MascotaEN pMascotaEN)
            {
                using (IDbConnection _conn =
                    ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("EliminarMascota", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pMascotaEN.Id));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int ModificarMascota(MascotaEN pMascotaEN)
            {
                using (IDbConnection _conn =
                    ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                        new SqlCommand("ModificarMascota", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pMascotaEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@Nombre", pMascotaEN.Nombre));
                    _comando.Parameters.Add(new SqlParameter("@Color", pMascotaEN.Color));
                    _comando.Parameters.Add(new SqlParameter("@FechaNaci", pMascotaEN.FechaNacimiento));
                    _comando.Parameters.Add(new SqlParameter("@IdGen", pMascotaEN.IdGenero));
                    _comando.Parameters.Add(new SqlParameter("@IdRaza", pMascotaEN.IdRaza));
                    _comando.Parameters.Add(new SqlParameter("@IdEspecie", pMascotaEN.IdEspecie));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

        }
    }
