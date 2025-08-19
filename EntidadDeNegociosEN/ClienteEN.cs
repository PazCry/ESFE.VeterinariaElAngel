using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadDeNegociosEN
{
    public class ClienteEN
    {
        public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Apellido { get; set; } = string.Empty;
            public long Telefono { get; set; }
            public DateTime FechaCreacion { get; set; }
    }
}
