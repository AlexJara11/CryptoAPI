using CryptoCurrency.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.BLL.Servicios.Contrato
{
    public interface ICriptoMonedaService
    {
        Task<List<CriptomonedumDTO>> Lista();
        Task<CriptomonedumDTO> Crear(CriptomonedumDTO modelo);
        Task<bool> Editar(CriptomonedumDTO modelo);
        Task<bool> Eliminar(int id);
    }
}
