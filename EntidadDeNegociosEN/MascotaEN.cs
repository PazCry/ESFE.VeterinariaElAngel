using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadDeNegociosEN
{
    public class MascotaEN
    {
        public byte Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string Color { get; set; } = string.Empty;
            public DateTime FechaNacimiento { get; set; }
            public byte IdGenero { get; set; }
            public byte IdRaza { get; set; }
            public byte IdEspecie { get; set; }
            public DateTime FechaCreacion { get; set; }
    }
}
