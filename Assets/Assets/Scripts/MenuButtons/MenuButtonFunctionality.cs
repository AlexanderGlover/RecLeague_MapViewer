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
        ApplicationData mAppData = GameObject.Find("DataObject").GetComponent<ApplicationData>();

        Sprite[] overlayObjective;


        switch (mAppData.GameModeSelected.GetName())
        {
            case "Hardpoint":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetHPOverlay();
                break;
            case "Search & Destroy":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetSnDOverlay();
                break;
            case "Control":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetCtrlOverlay();
                break;
            case "Capture the Flag":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetCTFOverlay();
                break;
            case "King of the Hill":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetKoTHOverlay();
                break;
            case "Oddball":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetOddballOverlay();
                break;
            case "Strongholds":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetStrongholdsOverlay();
                break;
            case "Competitive":
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetCompetitiveOverlay();
                break;
            default:
                overlayObjective = mAppData.GameMapSelected.GetMapImages().GetHPOverlay();
                break;
        }

        bool trigger = false;

        foreach (Sprite img in overlayObjective)
        {
            if (trigger)
            {
                objectiveOverlaySprite.GetComponent<SpriteRenderer>().sprite = img;
                trigger = false;
                break;
            }

            if (img.name == objectiveOverlaySprite.GetComponent<SpriteRenderer>().sprite.name && objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled)
            {
                trigger = true;
            }
        }

        if (trigger)
        {
            objectiveOverlaySprite.GetComponent<SpriteRenderer>().sprite = overlayObjective[0];
            objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled = false;
        }

        else if (!objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled)
        {
            objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled = true;
        }

        transform.gameObject.GetComponent<Image>().sprite = objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled ? selectedButtonSprite : defaultButtonImage;
    }
}
