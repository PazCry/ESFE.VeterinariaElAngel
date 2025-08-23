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
    public class RazaDAL
    {
            public static List<RazaEN> MostrarRazaEN()
            {
                List<RazaEN> _Lista = new List<RazaEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ListarRaza", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new RazaEN
                        {
                            Id = _reader.GetByte(0),
                            TipoRaza = _reader.GetString(1),
                            FechaCreacion = _reader.GetDateTime(2)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }
        public static List<RazaEN> BuscarRaza(string tipoRaza)
        {
            List<RazaEN> lista = new List<RazaEN>();

            using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
            {
                _conn.Open();
                SqlCommand _comando = new SqlCommand("BuscarRaza", _conn as SqlConnection);
                _comando.CommandType = CommandType.StoredProcedure;
                _comando.Parameters.Add(new SqlParameter("@TipoRaza", tipoRaza));

                using (SqlDataReader dr = _comando.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        RazaEN raza = new RazaEN
                        {
                            Id = Convert.ToByte(dr["Id"]),
                            TipoRaza = dr["TipoRaza"].ToString(),
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"])
                        };

                        lista.Add(raza);
                    }
                }

                _conn.Close();
            }

            return lista;
        }

        public static int AgregarRaza(RazaEN pRazaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarRaza", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@TipoRaza", pRazaEN.TipoRaza));


                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int EliminarRaza(RazaEN pRazaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("EliminarRaza", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pRazaEN.Id));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int ModificarRaza(RazaEN pRazaEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ModificarRaza", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pRazaEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@TipoRaza", pRazaEN.TipoRaza));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

        }
    }