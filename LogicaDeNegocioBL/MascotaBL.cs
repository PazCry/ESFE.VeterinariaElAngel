using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocioBL
{
    public class MascotaBL
    {
        public int VerificarMascotaLogin(MascotaEN pMascotaEN)
        {
            return MascotaDAL.VerificarMascotaLogin(pMascotaEN);
        }

        public List<MascotaEN> MostrarMascota()
        {
            return MascotaDAL.MostrarMascota();
        }
        public static List<MascotaEN> BuscarMascota(string nombre)
        {
            return MascotaDAL.BuscarMascota(nombre);
        }

        public int GuardarMascota(MascotaEN pMascotaEN)
        {
            return MascotaDAL.AgregarMascota(pMascotaEN);
        }

        public int EliminarMascota(MascotaEN pMascotaEN)
        {
            return MascotaDAL.EliminarMascota(pMascotaEN);
        }

        public int ModificarMascota(MascotaEN pMascotaEN)
        {
            return MascotaDAL.ModificarMascota(pMascotaEN);
        }

    }
}
