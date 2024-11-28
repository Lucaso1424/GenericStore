using Core.Application.DTOs;

namespace GenericStore.Application.DTOs;
public partial class StoreProductDTO : BaseEntityDTO
{
    public int StoreProductId { get; set; }

    public int StoreId { get; set; }

    public int ProductId { get; set; }

    public int Stock { get; set; }

    public int MinimumStock { get; set; }
}