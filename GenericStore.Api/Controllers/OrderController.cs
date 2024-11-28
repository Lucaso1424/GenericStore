﻿using AutoMapper;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenericStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAllAsync()
        {
            var orders = await orderService.GetAllAsync();

            if (orders == null)
            {
                return NotFound();
            }

            var ordersDTO = mapper.Map<IEnumerable<OrderDTO>>(orders);
            return Ok(ordersDTO);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetByIdAsync(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderDTO = mapper.Map<OrderDTO>(order);
            return Ok(orderDTO);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] OrderDTO orderDTO)
        {
            try
            {
                var entity = mapper.Map<Order>(orderDTO);
                await orderService.CreateAsync(entity);
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] OrderDTO orderDTO, int id)
        {
            try
            {
                if (orderDTO.OrderId != id)
                {
                    return BadRequest();
                }
                await orderService.UpdateAsync(id, orderDTO);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not update: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id) 
        {
            try 
            {
                var entityToDelete = await orderService.GetOrderByIdAsync(id);
                if (entityToDelete == null)
                    return NotFound();

                await orderService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
