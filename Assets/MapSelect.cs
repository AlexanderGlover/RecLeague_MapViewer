using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        applicationData = GameObject.Find("DataObject").GetComponent<ApplicationData>();
        Debug.Log(applicationData.GameSelectedIndex);
        gameData = applicationData.GameDataStorage[applicationData.GameSelectedIndex];
        LeagueName.GetComponent<TextMeshProUGUI>().text = "TacMaps: " + gameData.GetGameName();
        LeagueIcon.GetComponent<Image>().sprite = gameData.GetLeagueIcon();

        return true;
    }
}
