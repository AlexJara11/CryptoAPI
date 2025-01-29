using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.DTO
{
    public class CriptomonedumDTO
    {
        public int Id { get; set; } // Nuevo campo Id

        public string Codigo { get; set; } = null!;

        public string? Nombre { get; set; }

        public string? Symbol { get; set; }

        public string? Descripcion { get; set; }

        public int? EsActivo { get; set; }
    }
}
