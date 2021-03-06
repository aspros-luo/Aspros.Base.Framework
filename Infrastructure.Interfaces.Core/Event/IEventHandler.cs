using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Core.Event
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task Handle(T @event);
    }
}