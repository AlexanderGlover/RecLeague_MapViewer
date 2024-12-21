using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadMapButton : MonoBehaviour
{
    MapInfo mMapInfo;
    GameModeInfo mGameModeInfo;
    int mNumPlayers;

    bool mActive = false;

    [SerializeField]
    GameObject MapList;
    [SerializeField]
    GameObject GameModeList;

    public void SetMapInfo(MapInfo mapInfo) { mMapInfo = mapInfo; }
    public void SetGameModeInfo(GameModeInfo gameModeInfo) { mGameModeInfo = gameModeInfo; }
    public void SetNumPlayers(int numPlayers) { mNumPlayers = numPlayers; }

    private void Start()
    {
        mMapInfo = new MapInfo();
        mGameModeInfo = new GameModeInfo();
        if (mGameModeInfo.GetName() == "Competitive")
        {
            mNumPlayers = 3;
        } else
        {
            mNumPlayers = 4;
        }
        

        //Default Inactive
        transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.1f);
    }

    void Update()
    {

        bool active = (mMapInfo.GetName() != "" && mGameModeInfo.GetName() != "");
        
        if(active != mActive)
        {
            mActive = active;
            transform.GetChild(0).GetComponent<Image>().color = mActive ? new Color(1f, 1f, 1, 1f) : new Color(1f, 1f, 1f, 0.1f);
            GetComponent<Button>().interactable = mActive;
        }
    }

    public void BeginLoadingMap()
    {
        _BeginLoadingMap();
    }

    void _BeginLoadingMap()
    {
        GameObject dataObj = GameObject.Find("DataObject");
        if (dataObj != null)
        {
            ApplicationData appData = dataObj.GetComponent<ApplicationData>();

            if(mMapInfo.GetName() != "" && mGameModeInfo.GetName() != "")
            {
                appData.GameModeSelected = mGameModeInfo;
                appData.GameMapSelected = mMapInfo;
                SceneManager.LoadScene("MapView");
            }
            else
            {
                //Throw Error!
            }

        }
        else
        {
            //Throw Error!
        }
    }
}
