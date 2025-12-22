using AutoMapper;
using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.Services
{
    public class OrderService : GenericService<GenericStoreContext, Order, OrderDTO>, IOrderService
    {
        public OrderService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            IQueryable query = _context.Orders
                .Include(x => x.OrderDetail)
                .Where(x => x.OrderId == id);

            var order = await query.ProjectToType<OrderDTO>().FirstOrDefaultAsync();

            return order;
        }
    }
}