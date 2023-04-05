using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourPickerCustom : MonoBehaviour
{
    void Update()
    {
        FreeDraw.Drawable DrawLayer = FindObjectOfType(typeof(FreeDraw.Drawable)) as FreeDraw.Drawable;
        GetComponent<Image>().color = DrawLayer.fcp.color;
    }
}
