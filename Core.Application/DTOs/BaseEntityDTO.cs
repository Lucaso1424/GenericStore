using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.DTOs
{
    public class BaseEntityDTO
    {
        [NotMapped]
        public string TrackingState { get; set; }
    }
}