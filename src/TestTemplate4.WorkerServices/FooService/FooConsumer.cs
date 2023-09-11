using System.Threading.Tasks;
using MassTransit;
using TestTemplate4.Core.Events;

namespace TestTemplate4.WorkerServices.FooService
{
    public class FooConsumer : IConsumer<IFooEvent>
    {
        public Task Consume(ConsumeContext<IFooEvent> context) =>
            Task.CompletedTask;
    }
}
