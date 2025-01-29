using AutoMapper;
using CryptoCurrency.BLL.Servicios.Contrato;
using CryptoCurrency.DAL.Repositorios.Contrato;
using CryptoCurrency.DTO;
using CryptoCurrency.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.BLL.Servicios
{
    public class RolService : IRolService
    {
        private readonly IGenericRepository<Rol> _rolRepository;
        private readonly IMapper _mapper;
        public RolService(IGenericRepository<Rol> rolRepository, IMapper mapper)
        {
            _rolRepository = rolRepository;
            _mapper = mapper;
        }

        public async Task<List<RolDTO>> Lista()
        {
            try
            {
                var listaRoles = await _rolRepository.Consultar();
                return _mapper.Map<List<RolDTO>>(listaRoles.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
