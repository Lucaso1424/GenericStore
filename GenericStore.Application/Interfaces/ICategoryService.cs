using Core.Application.Interfaces;
using GenericStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.Interfaces
{
    public interface ICategoryService : IGenericService<Category, CategoryDTO>
    {
        Task<Category?> GetByIdAsync(int id);
    }
}