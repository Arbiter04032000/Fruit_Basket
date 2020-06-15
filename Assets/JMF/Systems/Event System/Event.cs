using System;
using UnityEngine;
using UnityEngine.Events;

namespace JMF.Systems.EventSystem
{
    public class Event : MonoBehaviour
    {
        [HideInInspector]
        public MyEvent OnTriggerEvent; // Stores event func 

        private int SenderID; // Ref to sender

        // Trigger the event
        public void TriggerEvent(Event e, int i)
        {
            OnTriggerEvent?.Invoke(e, i);
        }
    }
}
