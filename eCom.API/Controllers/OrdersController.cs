using Application.Common.Result;
using Application.DTOs.Orders;
using Application.Errors;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities.Orders;
using eCom.API.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using Application.Extentions;

namespace eCom.API.Controllers
{ 
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService
            , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDto dto)
        {
            if (dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            } 

            var order = _mapper.Map<Order>(dto); 

            if (await _orderService.AddAsync(order))
            {
                var routeValues = new
                {
                    action = nameof(GetOrderById),
                    controller = "Orders",
                    id = order.Id,

                };
                return CreatedAtRoute(routeValues, Result.Success(_mapper.Map<OrderDto>(order)));
            }
            return BadRequest(Result.Failure(OrderError.CreateFailed));
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] OrderPageParam pageParam)
        {
            var orders = await _orderService.GetOrdersAsync(pageParam);

            if (orders is null || !orders.Any())
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }
            Response.AddPaginationHeader(orders.CurrentPage, orders.PageSize, orders.TotalCount, orders.TotalPage);
            return Ok(Result.Success(_mapper.Map<List<OrderDto>>(orders)));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var order = await _orderService.GetByIdAsync(id);

            if (order is null)
            {
                return NotFound(Result.Failure(OrderError.NotFound));
            }

            return Ok(Result.Success(_mapper.Map<OrderDto>(order)));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderById(long id, [FromBody] OrderDto dto)
        {
            if (id <= 0 || dto is null)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var order = await _orderService.GetByIdAsync(id);

            if (order is null)
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            } 

            _mapper.Map(dto, order, opt => opt.AfterMap((src, des) =>
            {
                des.Id = id; 
            }));

            if (await _orderService.UpdateAsync(order))
            {
                var routeValues = new
                {
                    action = nameof(GetOrderById),
                    controller = "Orders",
                    id = order.Id,

                };
                return CreatedAtRoute(routeValues, dto);
            }
            return BadRequest(Result.Failure(ProductError.UpdateFailed(order.Code)));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Result.Failure(CommonError.InvalidRequest));
            }

            var product = await _orderService.GetByIdAsync(id);

            if (product is null)
            {
                return NotFound(Result.Failure(ProductError.NotFound));
            }

            if (await _orderService.RemoveAsync(product))
            {
                return Ok(Result.Success());
            }
            return BadRequest(Result.Failure(CommonError.UnknownError));
        }
    }
}