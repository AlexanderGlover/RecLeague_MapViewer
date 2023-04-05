using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderMenuAnim : MonoBehaviour
{
    [SerializeField]
    GameObject[] menuObjects;

    GameObject currentMenu = null;
    int currentIndex = -1;

    public void ShowHideMenu(Button menuButton)
    {
        GameObject menu = null;
        int index = -1;
        foreach (GameObject obj in menuObjects)
        {
            index++;
            if(obj.name.Remove(0,3) == menuButton.name)
            {
                menu = obj;
                break;
            }
        }
        
        if(menu == null)
        {
            return;
        }

        //Open new panel if none open
        if(currentMenu == null)
        {
            bool completed = SetAnimVariables(menu, true, 0);
            if (completed)
            {
                currentMenu = menu;
                currentIndex = index;
            }
        }
        //Close menu if same button selected
        else if(menu == currentMenu)
        {
            bool completed = SetAnimVariables(menu, false, 0);
            if (completed)
            {
                currentMenu = null;
                currentIndex = -1;
            }
        }
        else
        {
            if(menu==currentMenu)
            {
                Debug.Log("Shouldn't be here!");
            }

            bool completed = SetAnimVariablesSync(menu, index > currentIndex ? 1 : -1);
            if(completed)
            {
                currentMenu = menu;
                currentIndex = index;
            }
        }
    }

    private bool SetAnimVariables(GameObject newMenu, bool toOpen, int panelSwap)
    {
        Animator animator = newMenu.GetComponent<Animator>();
        if(animator != null && !animator.IsInTransition(0))
        {
            animator.SetInteger("vertical", panelSwap);
            animator.SetBool("show", toOpen);
            return true;
        }
        return false;
    }

    private bool SetAnimVariablesSync(GameObject newMenu, int direction)
    {
        Animator oldAnimator = currentMenu.GetComponent<Animator>();
        Animator newAnimator = newMenu.GetComponent<Animator>();
        if (newAnimator != null && oldAnimator != null && !newAnimator.IsInTransition(0) && !oldAnimator.IsInTransition(0))
        {
            newAnimator.SetInteger("vertical", direction);
            newAnimator.SetBool("show", true);

            oldAnimator.SetInteger("vertical", direction);
            oldAnimator.SetBool("show", false);
            return true;
        }
        return false;
    }
}
