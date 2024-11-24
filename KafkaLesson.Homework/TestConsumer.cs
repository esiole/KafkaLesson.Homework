using Dodo.Kafka.Consumer;

namespace KafkaLesson.Homework;

public class TestConsumer : IConsumer<TestEvent>
{
    public async Task Consume(TestEvent ev, CancellationToken ct)
    {
        EventStorage.Instance.AddConsumedEvent(ev);

        var prevProcessedEvent = EventStorage.Instance
            .GetProcessedEvent()
            .FirstOrDefault(processedEvent => processedEvent.Id == ev.Id && processedEvent.Version >= ev.Version);

        if (prevProcessedEvent == null)
        {
            EventStorage.Instance.AddProcessedEvent(ev);
        }

        await Task.CompletedTask;
    }
}