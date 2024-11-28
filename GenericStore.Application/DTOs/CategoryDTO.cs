#nullable disable
using Core.Application.DTOs;

namespace GenericStore.Domain.Entities;

public partial class CategoryDTO : BaseEntityDTO
{
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
}