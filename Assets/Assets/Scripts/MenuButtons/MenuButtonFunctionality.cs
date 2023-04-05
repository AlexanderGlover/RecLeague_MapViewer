using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonFunctionality : MonoBehaviour
{
    //######################         INITIAL VARIABLES         ######################
    private InputController inputCtrl;
    [SerializeField]
    private PointerTypes mPointerType;
    [SerializeField]
    private Sprite defaultButtonImage;
    [SerializeField]
    private Sprite selectedButtonSprite;

    [SerializeField]
    GameObject referenceObject;

    public void Awake()
    {
        inputCtrl = GameObject.FindGameObjectsWithTag("InputController")[0].GetComponent<InputController>();
    }

    //######################         Update         ######################
    private PointerTypes prevPT = PointerTypes.PT_Default;
    public void Update()
    {
        PointerTypes newPT = inputCtrl.GetPointerType();
        if(mPointerType != PointerTypes.PT_NONE && newPT != prevPT)
        {
            if(mPointerType == newPT)
            {
                //Now selected; Highlight
                transform.gameObject.GetComponent<Image>().sprite = selectedButtonSprite;
            }
            else if(mPointerType == prevPT)
            {
                //No longer selected; Back to normal
                transform.gameObject.GetComponent<Image>().sprite = defaultButtonImage;
            }
            prevPT = newPT;
        }
    }

    //######################         POINTER MENU         ######################
    public void ChangePointerType(int pointerTypeint)
    { 
        //Input corresponding INT with Enum (shown at definition)
        if(inputCtrl)
        {
            inputCtrl.UpdatePointerType((PointerTypes)pointerTypeint, false);
        }
    }

    //######################         DRAWING MENU         ######################
    public void EraseDrawingLayer()
    {
        FreeDraw.Drawable DrawLayer = FindObjectOfType(typeof(FreeDraw.Drawable)) as FreeDraw.Drawable;
        DrawLayer.ResetCanvas();
    }

    public void ToggleColourPicker()
    {
        FreeDraw.Drawable DrawLayer = FindObjectOfType(typeof(FreeDraw.Drawable)) as FreeDraw.Drawable;
        DrawLayer.ToggleColourPicker();
    }


    //######################         BACK BUTTON         ######################
    public void BackButton()
    {
        GameObject dataObj = GameObject.Find("DataObject");
        if (dataObj != null)
        {
            ApplicationData appData = dataObj.GetComponent<ApplicationData>();
            appData.GameModeSelected = new GameModeInfo();
            appData.GameMapSelected = new MapInfo();
        }

        SceneManager.LoadScene("MapSelect");
    }

    //######################         OVERLAY MENU        ######################
    public void ToggleCalloutOverlay()
    {
        GameObject calloutOverlaySprite = GameObject.Find("CalloutOverlay");
        if(calloutOverlaySprite)
        {
            calloutOverlaySprite.GetComponent<SpriteRenderer>().enabled = !calloutOverlaySprite.GetComponent<SpriteRenderer>().enabled;
        }

        transform.gameObject.GetComponent<Image>().sprite = calloutOverlaySprite.GetComponent<SpriteRenderer>().enabled  ? selectedButtonSprite : defaultButtonImage;
    }

    public void ToggleObjectiveOverlay()
    {
        GameObject objectiveOverlaySprite = GameObject.Find("ObjectiveOverlay");
        if (objectiveOverlaySprite)
        {
            objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled = !objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled;
        }

        transform.gameObject.GetComponent<Image>().sprite = objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled ? selectedButtonSprite : defaultButtonImage;
    }
}
