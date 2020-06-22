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

            BasketCheck(other);

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

            // disables trigger
            this.gameObject.SetActive(false);
        }

        //  checks if the basket colour is matching the fruit
        public void BasketCheck(Collider other)
        {

            other.transform.GetChild(0).GetComponent<Renderer>().material = other.GetComponent<TagContainer>().fruitMatUsed;
            //  check if basket contains tag of other fruit
            if (GetComponentInParent<TagContainer>().fruitTag == other.tag)
            {
                // Change Both Colour To Gray
                transform.parent.GetChild(0).GetComponent<Renderer>().material = ColourManager.Instance.basketMatGood;

                // Set Basket to scored
                GetComponentInParent<TagContainer>().scored = true;

                //Increases score by 5
                GameMan.Instance.Score = 5;
                UIManager.Instance.UpdateScore();
                //Gives the player an extra ball
                GameMan.Instance.ballCount++;
                UIManager.Instance.UpdateBall();

                return;
            }
            else
            {
                // Change Both Colour To Gray
                transform.parent.GetChild(0).GetComponent<Renderer>().material = ColourManager.Instance.basketMatBad;

                // Set Basket to scored
                GetComponentInParent<TagContainer>().scored = true;

                //Increases score by 1
                GameMan.Instance.Score = 1;
                UIManager.Instance.UpdateScore();

                return;
            }
        }
    }
}
