using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ObjectLocationInfo
{
    public void SetLocation(Vector3 setLocation) { location = setLocation; }
    public void SetSize(Vector3 setLocation) { location = setLocation; }

    public Vector3 GetLocation() { return location; }
    public Vector3 GetSize() { return size; }

    Vector3 location;
    Vector3 size;
}

public static class Tools
{
    public static ObjectLocationInfo ScreenToWorldLocationInfo(Camera camera, Vector3 screenPosition)
    {
        ObjectLocationInfo locationObj = new ObjectLocationInfo();

        Vector3 objLocation = camera.ScreenToWorldPoint(screenPosition);
        Vector3 objSize = new Vector3();

        //GetObj location relative
        

        locationObj.SetLocation(objLocation);
        locationObj.SetSize(objSize);

        return locationObj;
    }
}
