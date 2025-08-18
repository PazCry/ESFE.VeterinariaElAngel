using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocioBL
{
    public class RolBL
    {
        public int VerificarRolLogin(RolEN pRolEN)
        {
            return RolDAL.VerificarRolLogin(pRolEN);
        }

        public List<RolEN> MostrarRol()
        {
            return RolDAL.MostrarRol();
        }

        public int GuardarRol(RolEN pRolEN)
        {
            return RolDAL.AgregarRol(pRolEN);
        }

        public int EliminarRol(RolEN pRolEN)
        {
            return RolDAL.EliminarRol(pRolEN);
        }

        public int ModificarRol(RolEN pRolEN)
        {
            return RolDAL.ModificarRol(pRolEN);
        }

    }
}
