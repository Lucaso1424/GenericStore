using Microsoft.EntityFrameworkCore;

namespace Core.Application.Services
{
    public partial class GenericServiceBase<TContext> where TContext : DbContext
    {
        protected readonly TContext _context;

        public GenericServiceBase(TContext context) 
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}