using Core.Application.Interfaces;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.Interfaces
{
    public interface IProductService : IGenericService<Product, ProductDTO>
    {
        Task<ProductDTO?> GetByIdAsync(int id);
    }
}