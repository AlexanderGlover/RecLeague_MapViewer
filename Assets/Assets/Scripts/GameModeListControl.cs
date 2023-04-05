using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameModeListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayButton;

    [SerializeField]
    private GameObject mButtonTemplate;

    [SerializeField]
    private GameObject mapListObject;

    private List<GameObject> buttons;

    GameData gameData;

    GameObject selectedButton;
    bool mButtonSelected = false;

    private void Start()
    {
        GenerateList();
    }

    void GenerateList()
    {
        buttons = new List<GameObject>();

        if (GameObject.Find("DataObject") == null) { return; }

        ApplicationData applicationData = GameObject.Find("DataObject").GetComponent<ApplicationData>();
        gameData = applicationData.GameDataStorage[applicationData.GameSelectedIndex];

        //Clear existing Buttons
        buttons = ClearButtonList(buttons);

        int index = 0;

        //Add new buttons
        foreach (GameModeInfo gameMode in gameData.GetGameModes())
        {
            GameObject newButton = Instantiate(mButtonTemplate) as GameObject;

            buttons.Add(newButton);
            newButton.SetActive(true);

            newButton.GetComponent<ButtonListButton>().SetText(gameMode.GetName());
            newButton.GetComponent<ButtonListButton>().SetButtonInfo(index, 1);
            newButton.GetComponent<ButtonListButton>().SetGameModeInfo(gameMode);
            index++;

            newButton.transform.SetParent(mButtonTemplate.transform.parent, false);
        }
    }

    public GameModeInfo GetSelectedGameMode()
    {
        foreach (GameModeInfo gameMode in gameData.GetGameModes())
        {
            if (gameMode.GetName() == selectedButton.GetComponent<ButtonListButton>().GetText())
            {
                return gameMode;
            }
        }
        return new GameModeInfo();
    }

    public void ButtonClicked(int buttonIndex)
    {
        if (mButtonSelected)
        {
            selectedButton.GetComponent<ButtonListButton>().SetPressed(false);
        }

        selectedButton = buttons[buttonIndex];

        mapListObject.GetComponent<ButtonListControl>().SetAvailableMaps(selectedButton.GetComponent<ButtonListButton>().GetGameModeInfo().GetAvailableMaps());

        PlayButton.GetComponent<LoadMapButton>().SetGameModeInfo(GetSelectedGameMode());

        mButtonSelected = true;
    }

    List<GameObject> ClearButtonList(List<GameObject> buttonList)
    {
        foreach (GameObject button in buttonList)
        {
            Destroy(button.gameObject);
        }
        buttonList.Clear();

        return buttonList;
    }
}
