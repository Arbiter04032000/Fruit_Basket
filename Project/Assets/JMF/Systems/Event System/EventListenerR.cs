using UnityEngine;
using UnityEngine.Events;

namespace JMF.Systems.EventSystem.UnityRenderer
{
    public class EventListenerR : MonoBehaviour
    {
        public int SenderID; // Store sender ID
        public Event jmfEvent; // Store event
        public MyEvent funcUnityTrigger; // Store event func to trigger once successful listen

        private EventListener eListener; // Will hold our new listener

        void Start()
        {
            // Create listener
            eListener = new EventListener(jmfEvent, funcUnityTrigger, SenderID);            
        }

        // Destroy listener
        void OnDestroy()
        {
            eListener.OnDestroy();
        }

        // Run the listeners mimick func
        public void FuncTrigger(Event e)
        {
            eListener.FuncTrigger(e, SenderID);
        }
    }
}
