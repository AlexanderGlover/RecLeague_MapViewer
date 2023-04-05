using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Sprite unselectedButtonImage;
    [SerializeField] Sprite selectedButtonImage;

    [SerializeField] bool expandable = false;

    [SerializeField] GameObject[] subButtons;

    [SerializeField]
    private GameObject mSubMenuTemplate;

    private GameObject mSubMenu;

    

    public void OnClick()
    {
        //Enable button Functionality

        //Expand Sub_Menu
        if(expandable)
        {
            ToggleSubMenu();
        }
    }

    private void ToggleSubMenu()
    {
        //If no menu exists, can't be toggling off so toggle off other menus and create this one
        if(mSubMenu == null)
        {
            ToggleAllSubMenus(false);
            mSubMenu = Instantiate(mSubMenuTemplate) as GameObject;
            mSubMenu.transform.SetParent(transform);
            mSubMenu.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); //Not sure why parent sets scale to 100
            mSubMenu.GetComponent<SubMenu>().InitializeSubMenu(subButtons);
            mSubMenu.SetActive(true);
            mSubMenu.transform.parent.GetComponent<Image>().sprite = selectedButtonImage ;

            return;
        }

        bool isActive = mSubMenu.activeSelf;
        ToggleAllSubMenus(false); 

        if (!isActive)
        { 
            //Close all menus, then open the desired menu
            ToggleAllSubMenus(false); // not sure why this is going twice, need to debug to check.

            mSubMenu.SetActive(true);
            mSubMenu.transform.parent.GetComponent<Image>().sprite = selectedButtonImage;
        }
    }

    private void ToggleAllSubMenus(bool active)
    {
        GameObject[] submenuObjects = GameObject.FindGameObjectsWithTag("SubMenu");
        foreach (GameObject subMenuObj in submenuObjects)
        {
            subMenuObj.SetActive(active);
            subMenuObj.transform.parent.GetComponent<Image>().sprite = active ? selectedButtonImage : unselectedButtonImage;
        }

        //Special case to hide children
        FreeDraw.Drawable DrawLayer = FindObjectOfType(typeof(FreeDraw.Drawable)) as FreeDraw.Drawable;
        DrawLayer.ToggleColourPicker(false);
    }
}