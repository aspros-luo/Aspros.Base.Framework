using System.Linq;
using Infrastructure.Interfaces.Core.Interface;

namespace Infrastructure.Domain.Core
{
    public abstract class BaseRepository<TAggregateRoot> : IRepository<TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        private readonly IQueryable<TAggregateRoot> Entities;

        protected BaseRepository(IDbContext dbContext)
        {
            Entities = dbContext.Set<TAggregateRoot>();
        }

        public IQueryable<TAggregateRoot> GetAll()
        {
            return Entities;
        }

    }
}