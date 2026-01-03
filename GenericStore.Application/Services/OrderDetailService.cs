using AutoMapper;
using Core.Application.Services;
using GenericStore.Application.DTOs;
using GenericStore.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace GenericStore.Application.Services;

public class OrderDetailService : GenericService<GenericStoreContext, OrderDetail, OrderDetailDTO>, IOrderDetailService
{
    public OrderDetailService(GenericStoreContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public async Task<OrderDetailDTO?> GetOrderDetailByIdAsync(int id)
    {
        IQueryable<OrderDetail> query = _context.OrderDetails.Where(x => x.OrderId == id);
        var orderDetail = await query.ProjectToType<OrderDetailDTO>().FirstOrDefaultAsync();
        return orderDetail;
    }
}