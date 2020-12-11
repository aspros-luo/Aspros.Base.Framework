using System.Linq;

namespace Infrastructure.Domain.Core
{
    public interface IRepository<out TAggregateRoot> where TAggregateRoot : class, IAggregateRoot
    {
        IQueryable<TAggregateRoot> GetAll();
    }
}