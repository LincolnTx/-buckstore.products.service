using System.Threading.Tasks;
using buckstore.products.service.application.IntegrationEvents;

namespace buckstore.products.service.application.Adapters.MessageBroker
{
    public interface IMessageProducer<in TEvent> where TEvent : IntegrationEvent
    {
        Task Produce(TEvent message);
    }
}