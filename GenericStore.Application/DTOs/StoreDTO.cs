using Core.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Application.DTOs;
public partial class StoreDTO : BaseEntityDTO
{
    public int StoreId { get; set; }

    public string Name { get; set; }

    public string Location { get; set; }

    public virtual IEnumerable<StoreProductDTO>? StoreProducts { get; set; }
}