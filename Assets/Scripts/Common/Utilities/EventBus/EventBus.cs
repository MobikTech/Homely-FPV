using System;
using System.Collections.Generic;
using System.Linq;

namespace FpvDroneSimulator.Common.Utilities.EventBus
{
    public class EventBus : IDisposable
    {
        private readonly Dictionary<Type, List<IEventReceiver>> _subscribers = new Dictionary<Type, List<IEventReceiver>>();

        public void Subscribe<TEvent>(IEventReceiver<TEvent> eventReceiver) where TEvent : struct, IEvent
        {
            Type eventType = typeof(TEvent);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers.Add(eventType, new List<IEventReceiver>());
            }
            
            _subscribers[eventType].Add(eventReceiver);
        }
        
        public void Unsubscribe<TEvent>(IEventReceiver<TEvent> eventReceiver) where TEvent : struct, IEvent
        {
            Type eventType = typeof(TEvent);
            if (!_subscribers.ContainsKey(eventType))
            {
                throw new MissingMemberException("Cannot unsubscribe event that was not subscribed");
            }
            
            _subscribers[eventType].Remove(eventReceiver);
        }

        public void Raise<TEvent>(TEvent @event) where TEvent : struct, IEvent
        {
            Type eventType = typeof(TEvent);
            if (!_subscribers.ContainsKey(eventType))
            {
                _subscribers.Add(eventType, new List<IEventReceiver>());
            }
            
            foreach (var eventReceiver in _subscribers[eventType].ToList())
            {
                ((IEventReceiver<TEvent>)eventReceiver).OnEventHappened(@event);
            }
        }

        public void Dispose()
        {
            _subscribers.Values.ToList().ForEach(receiverList => receiverList.Clear());
            _subscribers.Clear();
        }
    }
}