using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TuyaPruebaGerman.Application.Features.Clientes.Queries;

namespace TuyaPruebaGerman.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ClientesController : BaseApiController
    {
        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetClientesQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }
    }
}
