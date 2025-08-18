using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocioBL
{
    public class ClienteBL
    {
        public int VerificarClienteLogin(ClienteEN pClienteEN)
        {
            return ClienteDAL.VerificarClienteLogin(pClienteEN);
        }

        public List<ClienteEN> ListarCliente()
        {
            return ClienteDAL.ListarCliente();
        }
        public static List<ClienteEN> BuscarCliente(string nombre)
        {
            return ClienteDAL.BuscarCliente(nombre);
        }

        public int GuardarCliente(ClienteEN pClienteEN)
        {

            if (ClienteDAL.TelefonoExiste(pClienteEN.Telefono.ToString()))
            {
                return -1; // El número de teléfono ya existe
            }

            int resultado = ClienteDAL.AgregarCliente(pClienteEN);
            return resultado > 0 ? 1 : 0; // 1 = éxito, 0 = error
        }
        public int EliminarCliente(ClienteEN pClienteEN)
        {
            return ClienteDAL.EliminarCliente(pClienteEN);
        }

        public int ModificarCliente(ClienteEN pClienteEN)
        {
            return ClienteDAL.ModificarCliente(pClienteEN);
        }

    }
}
