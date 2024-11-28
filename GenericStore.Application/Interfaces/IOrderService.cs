using Core.Application.Interfaces;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Interfaces;

public interface IOrderDetailService : IGenericService<OrderDetail, OrderDetailDTO>
{
    Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
}