using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocioBL
{
    public class GeneroBL
    {
        public List<GeneroEN> MostrarGenero()
        {
            return GeneroDAL.MostrarGenero();
        }

        public int GuardarGenero(GeneroEN pSexoEN)
        {
            return GeneroDAL.AgregarGenero(pSexoEN);
        }

        public int EliminarGenero(GeneroEN pSexoEN)
        {
            return GeneroDAL.EliminarGenero(pSexoEN);
        }

        public int ModificarGenero(GeneroEN pSexoEN)
        {
            return GeneroDAL.ModificarGenero(pSexoEN);
        }

    }
}
