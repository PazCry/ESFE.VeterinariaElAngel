using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocioBL
{
    public class UsuarioBL
    {
        private UsuarioDAL _usuarioDAL = new UsuarioDAL();

        public int VerificarUsuarioLogin(UsuarioEN pUsuarioEN)
        {
            return UsuarioDAL.VerificarUsuarioLogin(pUsuarioEN);
        }
        public List<UsuarioEN> MostrarUsuario()
        {
            return UsuarioDAL.MostrarUsuario();
        }
        public List<UsuarioEN> MostrarUsuarioDE()
        {
            return UsuarioDAL.MostrarUsuarioDE();
        }
        public static List<UsuarioEN> BuscarUsuario(string nombre)
        {
            return UsuarioDAL.BuscarUsuario(nombre);
        }
        public int GuardarUsuario(UsuarioEN pUsuarioEN)
        {
            return UsuarioDAL.AgregarUsuario(pUsuarioEN);
        }
        public int EliminarUsuario(UsuarioEN pUsuarioEN)
        {
            return UsuarioDAL.EliminarUsuario(pUsuarioEN);
        }
        public int ModificarUsuario(UsuarioEN pUsuarioEN)
        {
            return UsuarioDAL.ModificarUsuario(pUsuarioEN);
        }
        public bool CorreoExiste(string correo)
        {
            return _usuarioDAL.CorreoExiste(correo);
        }
        public bool CorreoExiste(string correo, int idUsuario)
        {
            return _usuarioDAL.CorreoExiste(correo, idUsuario);
        }

    }
}
