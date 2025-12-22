using Core.Application.Interfaces;
using GenericStore.Application.DTOs;
using GenericStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.Interfaces
{
    public interface IStoreProductService : IGenericService<StoreProduct, StoreProductDTO>
    {
        Task<StoreProductDTO?> GetByIdAsync(int id);
    }
}
