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

            var order = await query.ProjectToType<OrderDTO>().AsNoTracking().FirstOrDefaultAsync();

            return order;
        }

        public async Task UpdateByIdAsync(OrderDTO dto, int id)
        {
            var order = await _context.Orders.FindAsync(id) ?? throw new KeyNotFoundException($"Order with id {id} not found.");

            try
            {
                order.OrderId = dto.OrderId;
                order.OrderDate = dto.OrderDate;
                order.Status = dto.Status;
                order.OrderDetail = dto.OrderDetail?.Adapt<OrderDetail>();
                order.UserId = dto.UserId;

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the order.", ex);
            }
        }
    }
}