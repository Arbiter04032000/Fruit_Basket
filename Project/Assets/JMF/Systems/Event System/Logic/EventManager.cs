using System;
using System.Collections.Generic;
using UnityEngine.Events;
using JMF.Systems.Utilities;

namespace JMF.Systems.EventSystem
{
    [System.Serializable]
    public class MyEvent : UnityEvent<Event, int> { }

    [System.Serializable]
    public class EventManager
    {
        //--VARS--

        private static EventManager instance = new EventManager();
        public static EventManager Instance { get { return instance; } }

        private List<Event> events = new List<Event>();
        private Event currentEvent;

        // Add events
        public void AddEvent(Event e)
        {
            Instance.events.Add(e);
        }

        // Send event to Trigger on current Event
        public void TriggerEvent(Event e, int sID)
        {
            currentEvent = e;
            currentEvent.TriggerEvent(e, sID);
        }

        // Find event for Listeners
        public Event FindEvent(Event e)
        {
            foreach (Event ee in events)
            {
                if ( ee == e)
                    return e;
            }
            return null;
        }
    }
}
