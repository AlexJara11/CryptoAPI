using AutoMapper;
using CryptoCurrency.BLL.Servicios.Contrato;
using CryptoCurrency.DAL.Repositorios.Contrato;
using CryptoCurrency.DTO;
using CryptoCurrency.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoCurrency.BLL.Servicios
{
    public class CriptoMonedaService : ICriptoMonedaService
    {
        private readonly IGenericRepository<Criptomonedum> _criptomonedumRepository;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public CriptoMonedaService(IGenericRepository<Criptomonedum> criptomonedumRepository, IMapper mapper, HttpClient httpClient)
        {
            _criptomonedumRepository = criptomonedumRepository;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public async Task<List<CriptomonedumDTO>> Lista()
        {
            try
            {
                // Obtener ambas listas
                var listaApiCriptoMonedas = await ObtenerCriptomonedasDeApi();
                var listaCriptomonedas = await _criptomonedumRepository.Consultar();

                // Crear un diccionario para las criptomonedas de la API
                var diccionarioCriptomonedas = listaApiCriptoMonedas.ToDictionary(c => c.Codigo);

                // Recorrer la lista de criptomonedas del repositorio
                foreach (var cripto in listaCriptomonedas)
                {
                    // Si el registro está en el diccionario, reemplazarlo
                    if (diccionarioCriptomonedas.ContainsKey(cripto.Codigo))
                    {
                        diccionarioCriptomonedas[cripto.Codigo] = _mapper.Map<CriptomonedumDTO>(cripto);
                    }
                    // Si no está en el diccionario, agregarlo
                    else
                    {
                        diccionarioCriptomonedas.Add(cripto.Codigo, _mapper.Map<CriptomonedumDTO>(cripto));
                    }
                }

                //// Convertir el diccionario en una lista combinada
                //var listaCombinada = diccionarioCriptomonedas.Values.ToList();
                // Convertir el diccionario en una lista combinada y tomar los primeros 5 registros
                var listaCombinada = diccionarioCriptomonedas.Values.Take(5).ToList();

                return listaCombinada;
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
                var criptoMonedaModelo = _mapper.Map<Criptomonedum>(modelo);
                var criptoMonedaEncontrado = await _criptomonedumRepository.Obtener(criptoMoneda => criptoMoneda.Codigo == criptoMonedaModelo.Codigo);
                if (criptoMonedaEncontrado != null)
                    throw new TaskCanceledException("Código CriptoMondea ya existe");
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
                if(criptoMonedaEncontrado == null)
                {
                    var criptoMonedaCreado = await _criptomonedumRepository.Crear(_mapper.Map<Criptomonedum>(modelo));
                    return true;
                }

                //if (criptoMonedaEncontrado == null)
                //    throw new TaskCanceledException("CriptoMondea no encontrado");
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

        public async Task<bool> Eliminar(string codigoId)
        {
            try
            {
                var criptoMonedaEncontrado = await _criptomonedumRepository.Obtener(criptoMoneda => criptoMoneda.Codigo == codigoId);
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
        #region Método para cosumir api suministrada
        public async Task<List<CriptomonedumDTO>> ObtenerCriptomonedasDeApi()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://api.coingecko.com/api/v3/coins/list");
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var criptomonedasApi = JsonConvert.DeserializeObject<List<CoinDTO>>(responseBody);

                var criptomonedasDto = criptomonedasApi.Select(c => new CriptomonedumDTO
                {
                    Codigo = c.Id,
                    Symbol = c.Symbol,
                    Nombre = c.Name,
                    EsActivo = 1 // Asumiendo que todas las criptomonedas de la API están activas
                }).ToList();

                return criptomonedasDto;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener criptomonedas de la API", ex);
            }
        }
        #endregion
    }
}
