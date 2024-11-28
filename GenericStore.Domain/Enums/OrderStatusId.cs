using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericStore.Domain.Enums;
public enum OrderStatusId
{
    Pending = 1,
    Shipped = 2,
    InTransit = 3,
    Completed = 4,
    Cancelled = 5
}