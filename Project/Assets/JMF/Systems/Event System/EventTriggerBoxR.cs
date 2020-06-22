using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JMF.Systems.EventSystem.UnityRenderer;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace JMF.Systems.EventSystem.UnityRenderer
{
    public class EventTriggerBoxR : MonoBehaviour
    {
        public UnityEvent triggerEnterFunc, triggerExitFunc;

        public void OnTriggerEnter(Collider other)
        {
            print("Trigger " + this + " firing");
            triggerEnterFunc.Invoke();

            //Triggers the disabler, and feeds it useful information
            Disabler(other.GetComponent<Transform>());
        }

        public void OnTriggerExit(Collider other)
        {
            triggerExitFunc.Invoke();
        }

        //When performed, this disables the Interactable on the other object, and this script
        public void Disabler(Transform other)
        {
            //other.transform.GetChild(0).GetComponent<Renderer>().sharedMaterial = 
            Destroy(other.GetComponent<XRGrabInteractable>());
            this.gameObject.SetActive(false);
        }
    }
}
