using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntidadDeNegociosEN
{
    public class EspecieEN
    {
        public byte Id { get; set; }
            public string TipoEspecie { get; set; } = string.Empty;
            public DateTime FechaCreacion { get; set; }
    }
}
