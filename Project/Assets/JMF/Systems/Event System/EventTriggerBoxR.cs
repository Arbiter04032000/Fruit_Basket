using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JMF.Systems.EventSystem.UnityRenderer;

namespace JMF.Systems.EventSystem.UnityRenderer
{
    public class EventTriggerBoxR : MonoBehaviour
    {
        public UnityEvent triggerEnterFunc, triggerExitFunc;

        public void OnTriggerEnter(Collider other)
        {
            print("Trigger " + this + " firing");
            triggerEnterFunc.Invoke();
        }

        public void OnTriggerExit(Collider other)
        {
            triggerExitFunc.Invoke();
        }
    }
}
