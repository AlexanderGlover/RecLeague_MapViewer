using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBarManager : MonoBehaviour
{
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
}
