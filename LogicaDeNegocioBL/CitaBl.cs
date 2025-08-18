using EntidadDeNegociosEN;
using LogicaDeAccesoADatosDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaDeNegocioBL
{
    public class CitaBl
    {
         
        private readonly CitaDAL _citaDAL;

        public CitaBl()
        {
            _citaDAL = new CitaDAL();
        }

        public bool ExisteCitaEnFechaYHora(DateTime fecha, TimeSpan hora)
        {
            return CitaDAL.VerificarCitaPorFechaYHora(fecha, hora);
        }


        public List<CitaEN> MostrarCita()
        {
            return CitaDAL.MostrarCita();
        }
        public static List<CitaEN> BuscarCita(string Id)
        {
            return CitaDAL.BuscarCita(Id);
        }

        public int GuardarCita(CitaEN pCitaEN)
        {
            return CitaDAL.AgregarCita(pCitaEN);
        }

        public int EliminarCita(CitaEN pCitaEN)
        {
            return CitaDAL.EliminarCita(pCitaEN);
        }
        public int ModificarCita(CitaEN pCitaEN)
        {
            return CitaDAL.ModificarCita(pCitaEN);
        }
        public static CitaEN ObtenerCitaId(int id)
        {
            // Llama al método estático de la DAL directamente
            return CitaDAL.ObtenerCitaId(id);
        }

    }
}
    