using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadDeNegociosEN
{
    public class ExpedienteEN
    {
            public byte Id { get; set; }
            public byte IdCliente { get; set; }
            public byte IdMascota { get; set; }
            public string DescripcionConsulta { get; set; }
            public string Estado { get; set; }
            public DateTime Fecha { get; set; }
    }
}
