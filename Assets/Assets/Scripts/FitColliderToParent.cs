using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FitColliderToParent : MonoBehaviour
{
    private Camera camera;
    private Vector2 parentSize = new Vector2();

    private void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newParentSize = transform.parent.GetComponent<RectTransform>().rect.size;
        if(parentSize != newParentSize)
        {
            //resize child;
            ObjectLocationInfo newLocation = Tools.ScreenToWorldLocationInfo(camera, GetComponent<RectTransform>().position);
                
            GetComponent<BoxCollider2D>().offset = newLocation.GetLocation() - transform.parent.GetComponent<RectTransform>().position;
            parentSize = newParentSize;
        }
    }
}
