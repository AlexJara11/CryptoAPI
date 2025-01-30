using AutoMapper;
using CryptoCurrency.BLL.Servicios.Contrato;
using CryptoCurrency.DAL.Repositorios.Contrato;
using CryptoCurrency.DTO;
using CryptoCurrency.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.BLL.Servicios
{
    public class CriptoMonedaService : ICriptoMonedaService
    {
        private readonly IGenericRepository<Criptomonedum> _criptomonedumRepository;
        private readonly IMapper _mapper;

        public CriptoMonedaService(IGenericRepository<Criptomonedum> criptomonedumRepository, IMapper mapper)
        {
            _criptomonedumRepository = criptomonedumRepository;
            _mapper = mapper;
        }
        public async Task<List<CriptomonedumDTO>> Lista()
        {
            try
            {
                var listaCriptomonedas = await _criptomonedumRepository.Consultar();
                return _mapper.Map<List<CriptomonedumDTO>>(listaCriptomonedas.ToList());
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<CriptomonedumDTO> Crear(CriptomonedumDTO modelo)
        {
            try
            {
                var criptoMonedaCreado = await _criptomonedumRepository.Crear(_mapper.Map<Criptomonedum>(modelo));
                if (criptoMonedaCreado == null)
                    throw new TaskCanceledException("No se pudo crear la Criptomoneda");
                return _mapper.Map<CriptomonedumDTO>(criptoMonedaCreado);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Editar(CriptomonedumDTO modelo)
        {
            try
            {
                var criptoMonedaModelo = _mapper.Map<Criptomonedum>(modelo);
                var criptoMonedaEncontrado = await _criptomonedumRepository.Obtener(criptoMoneda => criptoMoneda.Id == criptoMonedaModelo.Id);
                if (criptoMonedaEncontrado == null)
                    throw new TaskCanceledException("CriptoMondea no encontrado");
                criptoMonedaEncontrado.Codigo = criptoMonedaModelo.Codigo;
                criptoMonedaEncontrado.Nombre = criptoMonedaModelo.Nombre;
                criptoMonedaEncontrado.Symbol = criptoMonedaModelo.Symbol;
                criptoMonedaEncontrado.Descripcion = criptoMonedaModelo.Descripcion;
                criptoMonedaEncontrado.EsActivo = criptoMonedaModelo.EsActivo;
                if (!await _criptomonedumRepository.Editar(criptoMonedaEncontrado))
                    throw new TaskCanceledException("No se pudo editar la CriptoMoneda");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var criptoMonedaEncontrado = await _criptomonedumRepository.Obtener(criptoMoneda => criptoMoneda.Id == id);
                if (criptoMonedaEncontrado == null)
                    throw new TaskCanceledException("CriptoMoneda no encontrado");
                if (!await _criptomonedumRepository.Eliminar(criptoMonedaEncontrado))
                    throw new TaskCanceledException("No se pudo eliminar la CriptoMoneda");
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
