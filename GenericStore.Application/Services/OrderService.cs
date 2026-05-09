using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;
namespace GenericStore.Application.Services
{
    public class OrderService : GenericService<GenericStoreContext, Order, OrderDTO>, IOrderService
    {
        public OrderService(GenericStoreContext context) : base(context)
        {
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            IQueryable<Order> query = _context.Orders
                .Include(x => x.OrderDetail)
                .Where(x => x.OrderId == id);

            var order = await query.ProjectToType<OrderDTO>().FirstOrDefaultAsync();

            return order;
        }
    }
}