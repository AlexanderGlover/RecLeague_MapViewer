using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapImageManager : MonoBehaviour
{
    public GameObject calloutOverlaySprite;
    public GameObject objectiveOverlaySprite;
    public GameObject helpScreenOverlaySprite;

    Vector2 ScreenSize;

    ApplicationData mAppData;
    // Start is called before the first frame update
    void Start()
    {
        ScreenSize = new Vector2(Screen.width, Screen.height);
        
        GameObject dataObj = GameObject.Find("DataObject");

        if(dataObj != null)
        {
            mAppData = dataObj.GetComponent<ApplicationData>();


            switch (mAppData.GameModeSelected.GetName())
            {
                case "Hardpoint":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetHPOverlay()[0]);
                    break;
                case "Search & Destroy":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetSnDOverlay()[0]);
                    break;
                case "Control":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetCtrlOverlay()[0]);
                    break;
                case "Overload":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetOverloadOverlay()[0]);
                    break;
                case "Capture the Flag":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetCTFOverlay()[0]);
                    break;
                case "King of the Hill":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetKoTHOverlay()[0]);
                    break;
                case "Neutral Bomb":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetNeutralBombOverlay()[0]);
                    break;
                case "Oddball":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetOddballOverlay()[0]);
                    break;
                case "Strongholds":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetStrongholdsOverlay()[0]);
                    break;
                case "Uplink":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetUplinkOverlay()[0]);
                    break;
                case "Competitive":
                    SetObjectiveOverlay(mAppData.GameMapSelected.GetMapImages().GetCompetitiveOverlay()[0]);
                    break;
                default:
                    break;
            }


            //Base Map
            SetImageBackground(mAppData.GameMapSelected.GetMapImages().GetBaseMap());
            SetCalloutOverlay(mAppData.GameMapSelected.GetMapImages().GetCalloutOverlay());
        }

        //Set objective overlay to active, and callout to inactive
        calloutOverlaySprite.GetComponent<SpriteRenderer>().enabled = false;
        objectiveOverlaySprite.GetComponent<SpriteRenderer>().enabled = true;
        helpScreenOverlaySprite.SetActive(false);
    }

    void SetImageBackground(Sprite img)
    {
        transform.gameObject.GetComponent<SpriteRenderer>().sprite = img;
    }

    void SetCalloutOverlay(Sprite img)
    {        
        calloutOverlaySprite.GetComponent<SpriteRenderer>().sprite = img;

        if (img)
        {
            float aspectRatio = img.bounds.size.x / img.bounds.size.y;
            calloutOverlaySprite.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        }
        else
        {
            Debug.Log("Failed to find image");
        }
    }

    void SetObjectiveOverlay(Sprite img)
    {
        objectiveOverlaySprite.GetComponent<SpriteRenderer>().sprite = img;

        if (img)
        {
            float aspectRatio = img.bounds.size.x / img.bounds.size.y;
            objectiveOverlaySprite.GetComponent<AspectRatioFitter>().aspectRatio = aspectRatio;
        }
        else
        {
            Debug.Log("Failed to find image");
        }
    }

    public void ToggleHelpWindow()
    {
        helpScreenOverlaySprite.SetActive(!helpScreenOverlaySprite.activeSelf);
    }
}
