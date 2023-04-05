using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CloseOnClickOff : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    //Detect current clicks on the GameObject (the one with the script attached)
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if(name == transform.gameObject.name)
        {
            transform.parent.gameObject.SetActive(false);
        }
        Debug.Log(name);
    }

    //Detect if clicks are no longer registering
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        Debug.Log(name + "No longer being clicked");
    }
}