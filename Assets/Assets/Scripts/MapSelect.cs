using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSelect : MonoBehaviour
{
    ApplicationData applicationData;
    GameData gameData;

    [SerializeField]
    GameObject LeagueIcon;
    [SerializeField]
    GameObject LeagueName;

    private bool mInitialized = false;

    // Start is called before the first frame update
    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(!mInitialized)
        {
            mInitialized = Initialize();
        }
    }

    private bool Initialize()
    {
        if(GameObject.Find("DataObject") != null)
        {
            applicationData = GameObject.Find("DataObject").GetComponent<ApplicationData>();
            gameData = applicationData.GameDataStorage[applicationData.GameSelectedIndex];
            LeagueName.GetComponent<TextMeshProUGUI>().text = "TacMaps: " + gameData.GetGameName();
            LeagueIcon.GetComponent<Image>().sprite = gameData.GetLeagueIcon();

        }

        return true;
    }

    public void GoHome()
    {
        GameObject dataObj = GameObject.Find("DataObject");
        if(dataObj != null)
        {
            ApplicationData appData = dataObj.GetComponent<ApplicationData>();
            appData.GameSelectedIndex = -1;
        }


        SceneManager.LoadScene("GameSelect");
    }

    private void StartSim()
    {

    }
}
