using Core.Application.DTOs;

namespace GenericStore.Application.DTOs;
public partial class OrderDetailDTO : BaseEntityDTO
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }
}