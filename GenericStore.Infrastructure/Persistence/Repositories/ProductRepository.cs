using GenericStore.Domain.Entities;
using GenericStore.Infrastructure.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenericStore.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
