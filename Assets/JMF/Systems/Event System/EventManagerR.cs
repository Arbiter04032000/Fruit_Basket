using System;
using UnityEngine;

namespace JMF.Systems.EventSystem.UnityRenderer
{
    [System.Serializable]
    public class EventManagerR : MonoBehaviour
    {
        // VARS
        public Transform eventGOarray;
        private int currentID;

        //--SINGLETON--
        public static EventManagerR Instance;

        // On unity game start
        void Awake()
        {
            Instance = this;

            foreach (Transform t in eventGOarray)
            {
                Event e = t.GetComponent<Event>();
                EventManager.Instance.AddEvent(e);
            }
        }

        // Set id
        public void SetID(int id)
        {
            currentID = id;
        }

        // Send event
        public void TriggerEvent(Event e)
        {
            EventManager.Instance.TriggerEvent(e, currentID);
        }
    }
}
