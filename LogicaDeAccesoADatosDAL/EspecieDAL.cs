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
    public class EspecieDAL
    {
            public static List<EspecieEN> MostrarEspecie()
            {
                List<EspecieEN> _Lista = new List<EspecieEN>();
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ListarEspecie", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    IDataReader _reader = _comando.ExecuteReader();
                    while (_reader.Read())
                    {
                        _Lista.Add(new EspecieEN
                        {
                            Id = _reader.GetByte(0),
                            TipoEspecie = _reader.GetString(1),
                            FechaCreacion = _reader.GetDateTime(2)
                        });
                    }
                    _conn.Close();
                }
                return _Lista;
            }

            public static int AgregarEspecie(EspecieEN pEspecieEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("GuardarEspecie", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@TipoEspecie", pEspecieEN.TipoEspecie));


                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int EliminarEspecie(EspecieEN pEspecieEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("EliminarEspecie", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pEspecieEN.Id));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

            public static int ModificarEspecie(EspecieEN pEspecieEN)
            {
                using (IDbConnection _conn = ComunBD.ObtenerConexion(ComunBD.TipoBD.SqlServer))
                {
                    _conn.Open();
                    SqlCommand _comando = new SqlCommand("ModificarEspecie", _conn as SqlConnection);
                    _comando.CommandType = CommandType.StoredProcedure;
                    _comando.Parameters.Add(new SqlParameter("@Id", pEspecieEN.Id));
                    _comando.Parameters.Add(new SqlParameter("@TipoEspecie", pEspecieEN.TipoEspecie));
                    _comando.Parameters.Add(new SqlParameter("@FechaCreacion", pEspecieEN.FechaCreacion));

                    int resultado = _comando.ExecuteNonQuery();
                    _conn.Close();
                    return resultado;
                }
            }

        }
    }