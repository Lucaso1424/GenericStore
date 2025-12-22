using Core.Application.Interfaces;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;

namespace GenericStore.Application.Interfaces;

public interface IOrderService : IGenericService<Order, OrderDTO>
{
    Task<OrderDTO?> GetOrderByIdAsync(int id);
}