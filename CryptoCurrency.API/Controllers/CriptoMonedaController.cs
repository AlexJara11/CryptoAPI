using CryptoCurrency.API.Utilidad;
using CryptoCurrency.BLL.Servicios.Contrato;
using CryptoCurrency.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrency.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CriptoMonedaController : ControllerBase
    {
        private readonly ICriptoMonedaService _criptoMonedaService;

        public CriptoMonedaController(ICriptoMonedaService criptoMonedaService)
        {
            _criptoMonedaService = criptoMonedaService;
        }
        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var rsp = new Response<List<CriptomonedumDTO>>();
            try
            {
                rsp.status = true;
                rsp.value = await _criptoMonedaService.Lista();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar([FromBody] CriptomonedumDTO producto)
        {
            var rsp = new Response<CriptomonedumDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _criptoMonedaService.Crear(producto);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }

        [HttpPut]
        [Route("Editar")]
        public async Task<IActionResult> Editar([FromBody] CriptomonedumDTO producto)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _criptoMonedaService.Editar(producto);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
        [HttpDelete]
        [Route("Eliminar/{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var rsp = new Response<bool>();
            try
            {
                rsp.status = true;
                rsp.value = await _criptoMonedaService.Eliminar(id);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
