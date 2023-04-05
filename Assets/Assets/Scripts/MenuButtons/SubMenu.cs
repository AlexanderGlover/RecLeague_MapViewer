using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenu : MonoBehaviour
{
    [SerializeField] GameObject backgroundSprite;
    [SerializeField] GameObject outlineObject;

    [SerializeField] GameObject indicatorParent;
    [SerializeField] GameObject indicatorBackgroundSprite;
    [SerializeField] GameObject indicatorOutlineObject;

    [SerializeField] Vector2 borderSize;
    [SerializeField] Vector2 paddingSize;
    [SerializeField] float betweenButtons;
    Vector2 menuContainerSize;

    public void InitializeSubMenu(GameObject[] buttons)
    {
        InstantiateBox(buttons.Length);
        InstantiateButtons(buttons);
    }

    private void InstantiateBox(int numButtons)
    {
        //--------------- Sizing of main box ---------------
        Vector2 buttonSize = transform.parent.gameObject.GetComponent<RectTransform>().rect.size;
        transform.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(buttonSize.x * 1.5f, 0.0f);
        

        menuContainerSize = Vector2.Scale(buttonSize, new Vector3(1.0f, numButtons, 1.0f)) + paddingSize*2.0f + new Vector2(0.0f, (numButtons - 1) * betweenButtons);

        backgroundSprite.GetComponent<RectTransform>().sizeDelta = menuContainerSize;
        backgroundSprite.GetComponent<RectTransform>().anchoredPosition = new Vector3(borderSize.x, -borderSize.y, 0.0f);
        outlineObject.GetComponent<RectTransform>().sizeDelta = menuContainerSize + (borderSize * 2.0f);

        transform.gameObject.GetComponent<RectTransform>().sizeDelta = menuContainerSize + (borderSize * 2.0f);

        //--------------- Sizing of indicator ---------------
        //place parent

        Vector2 indicatorOutlineSize = indicatorOutlineObject.GetComponent<RectTransform>().rect.size;

        indicatorParent.GetComponent<RectTransform>().sizeDelta = new Vector3(Mathf.Sqrt(Mathf.Pow(indicatorOutlineSize.x, 2.0f) * 2.0f) - borderSize.x, Mathf.Sqrt(Mathf.Pow(indicatorOutlineSize.x, 2.0f) * 2.0f) - borderSize.x, 0.0f);
        indicatorParent.GetComponent<RectTransform>().anchoredPosition = new Vector3(borderSize.x, -buttonSize.y / 2.0f, 0.0f);

        indicatorBackgroundSprite.GetComponent<RectTransform>().sizeDelta = new Vector3(indicatorOutlineSize.x - borderSize.x, indicatorOutlineSize.y - borderSize.y, 0.0f);
    }

    private void InstantiateButtons(GameObject[] buttons)
    {
        Vector2 buttonSize = transform.parent.gameObject.GetComponent<RectTransform>().rect.size;

        int index = 0;
        foreach(GameObject buttonInfo in buttons)
        {
            GameObject buttonObj = Instantiate(buttonInfo) as GameObject;
            buttonObj.transform.SetParent(this.transform);
            buttonObj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); //Not sure why parent sets scale to 100

            buttonObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.0f, -(buttonSize.y + betweenButtons) * index - paddingSize.y - borderSize.y);

            index++;
        }
    }
}
