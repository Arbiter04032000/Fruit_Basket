using UnityEngine.Events; // Swap from Unity event to system.action

namespace JMF.Systems.EventSystem
{
    public class EventListener
    {
        public int SenderID; // Store sender
        public MyEvent funcTrigger; // Store event function to call
        public Event jmfEvent; // Stores a ref to a new event once created

        // Grab an inspector unity event and show it - can use diff event system
        public EventListener(Event e, MyEvent fTrigger, int sID)
        {
            // Set the event, sender and func
            jmfEvent = e;
            SenderID = sID;
            funcTrigger = fTrigger;

            // Take from event manager instead of inspector link
            jmfEvent = EventManager.Instance.FindEvent(jmfEvent);
            jmfEvent.OnTriggerEvent.AddListener(FuncTrigger);
        }

        // Remove listeners
        public void OnDestroy()
        {
            jmfEvent.OnTriggerEvent.RemoveListener(FuncTrigger);
        }

        // Trigger the event function
        public void FuncTrigger(Event e, int sID)
        {
            if (SenderID == sID)
            {
                funcTrigger?.Invoke(e, sID);
            }
        }
    }
}
