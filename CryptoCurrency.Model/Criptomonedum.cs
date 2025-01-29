using System;
using System.Collections.Generic;

namespace CryptoCurrency.Model;

public partial class Criptomonedum
{
    public int Id { get; set; } // Nuevo campo Id

    public string Codigo { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Symbol { get; set; }

    public string? Descripcion { get; set; }

    public bool? EsActivo { get; set; }
}
