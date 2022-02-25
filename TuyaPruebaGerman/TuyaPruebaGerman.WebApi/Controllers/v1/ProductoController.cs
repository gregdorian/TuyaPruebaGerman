using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductos;
using TuyaPruebaGerman.Application.Features.Productos.Queries.GetProductoByIdQuery;
using TuyaPruebaGerman.Application.Features.Productos.Commands.CreateProductos;
using TuyaPruebaGerman.Application.Features.Productos.Commands.UpdateProductos;
using TuyaPruebaGerman.WebApi.Extensions;
using TuyaPruebaGerman.Application.Features.Productos.Commands.DeleteProductosById;

namespace TuyaPruebaGerman.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class ProductoController : BaseApiController
    {
        /// <summary>
        /// GET: api/controller
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetProductosQuery filter)
        {
            return Ok(await Mediator.Send(filter));
        }

        /// <summary>
        /// GET api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await Mediator.Send(new GetProductoByIdQuery { Id = id }));
        }

        /// <summary>
        /// POST api/controller
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateProductoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Bulk insert fake data by specifying number of rows
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMock")]
        [Authorize]
        public async Task<IActionResult> AddMock(InsertMockProductoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Support Ngx-DataTables https://medium.com/scrum-and-coke/angular-11-pagination-of-zillion-rows-45d8533538c0
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Paged")]
        public async Task<IActionResult> Paged(PagedProductosQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// PUT api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, UpdateProductoCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// DELETE api/controller/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Policy = AuthorizationConsts.ManagerPolicy)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteProductoByIdCommand { Id = id }));
        }
    }
}
