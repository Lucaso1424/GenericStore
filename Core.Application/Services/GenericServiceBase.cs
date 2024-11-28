using AutoMapper;
using GenericStore.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Services
{
    public partial class GenericServiceBase<TContext> where TContext : DbContext
    {
        protected readonly TContext _context;
        protected readonly IMapper _mapper;

        public GenericServiceBase(TContext context, IMapper mapper) 
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
            this._mapper = mapper ?? throw new ArgumentNullException(nameof(context)); ;
        }
    }
}