using Core.Application.DTOs;
using GenericStore.Domain.Entities;
using GenericStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.DTOs;
public partial class OrderDTO : BaseEntityDTO
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal Total { get; set; }

    public string Status { get; set; }

    public virtual OrderDetailDTO OrderDetail { get; set; }
}