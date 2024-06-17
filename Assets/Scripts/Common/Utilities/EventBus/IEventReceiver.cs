namespace FpvDroneSimulator.Common.Utilities.EventBus
{
    public interface IEventReceiver
    {
    }

    public interface IEventReceiver<in TEvent> : IEventReceiver where TEvent : struct, IEvent
    {
        public void OnEventHappened(TEvent @event);
    }
}