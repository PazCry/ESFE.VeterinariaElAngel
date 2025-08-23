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
    public class GeneroDAL
    {
            public static List<GeneroEN> MostrarGenero()
            {
                List<GeneroEN> _Lista = new List<GeneroEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("ListarGen", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new GeneroEN
                        {
                            Id = _reader.GetByte(0),                   // TINYINT → byte
                            TipoGenero = _reader.GetString(1),           // VARCHAR(10) → string
                            FechaCreacion = _reader.GetDateTime(2)     // DATE → DateTime
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }

            public static int AgregarGenero(GeneroEN pSexoEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarGenero", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@TipoGen", pSexoEN.TipoGenero));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int EliminarGenero(GeneroEN pSexoEN)
            {
                using (IDbConnection _conn =
                    ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                    new SqlCommand("EliminarGen", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pSexoEN.Id));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int ModificarGenero(GeneroEN pSexoEN)
            {
                using (IDbConnection _conn =
                    ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando =
                        new SqlCommand("ModificarGen", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pSexoEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@TipoGen", pSexoEN.TipoGenero));
                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }


        }
    }