using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    //VARS
    private XRNode xrHandNode = XRNode.RightHand; //Only functional input
    private List<InputDevice> deviceList = new List<InputDevice>(); //"All" input devices
    private InputDevice deviceActual; //Input device that is registering inputs

    private bool isTriggerPressed, isPrimaryTouched, isPrimaryPressed, isMenuPressed;

    //Event hooks
    public UnityEvent onTriggerPressed, onPrimaryTouched, onPrimaryTouchedExit, onPrimaryPressed, onMenuPressed;

    //SINGLETON
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    void Awake()
    {
        instance = this;
        GetDevices();
    }

    void GetDevices()
    {
        InputDevices.GetDevicesAtXRNode(xrHandNode, deviceList); //Gets all connected devices
        if(deviceList.Count > 0)
            deviceActual = deviceList[0]; //And assigns the first one to be the tracked device
    }

    void GetInput()
    {
        List<InputFeatureUsage> features = new List<InputFeatureUsage>(); //Generate inputfeature list
        deviceActual.TryGetFeatureUsages(features); //Store all device features in said list

        if (deviceActual.TryGetFeatureValue(CommonUsages.triggerButton, out isTriggerPressed) && isTriggerPressed) //Check if feature changes value and stores the value
        {
            onTriggerPressed?.Invoke(); //Invoke event if feature is pressed
        }

        if (deviceActual.TryGetFeatureValue(CommonUsages.primary2DAxisTouch, out isPrimaryTouched) && isPrimaryTouched) //Check if feature changes value and stores the value
        {
            onPrimaryTouched?.Invoke(); //Invoke event if feature is pressed
        }
        else
        {
            onPrimaryTouchedExit?.Invoke(); //Invoke event when feature is no longer pressed
        }

        if (deviceActual.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out isPrimaryPressed) && isPrimaryPressed) //Check if feature changes value and stores the value
        {
            onPrimaryPressed?.Invoke(); //Invoke event if feature is pressed
        }

        if (deviceActual.TryGetFeatureValue(CommonUsages.menuButton, out isMenuPressed) && isMenuPressed) //Check if feature changes value and stores the value
        {
            onMenuPressed?.Invoke(); //Invoke event if feature is pressed
        }
    }

    public void FindInput()
    {
        List<InputFeatureUsage> features = new List<InputFeatureUsage>(); //Generate inputfeature list
        deviceActual.TryGetFeatureUsages(features); //Store all device features in said list

        

        foreach (InputFeatureUsage feature in features)
        {
            if (feature.type == typeof(bool))
            {
                bool flag;
                if (deviceActual.TryGetFeatureValue(feature.As<bool>(), out flag) && flag && string.Compare(feature.name, "IsTracked") != 0)
                {
                    UIManager.Instance.PrintUI(feature.name + "<color=#00ffffff>" + flag.ToString() + "</color>");
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }
}
