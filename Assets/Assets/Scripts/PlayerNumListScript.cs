using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerNumListScript : MonoBehaviour
{
    [SerializeField]
    private GameObject PlayButton;

    [SerializeField]
    private GameObject mButtonTemplate;

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

        //Clear existing Buttons
        buttons = ClearButtonList(buttons);

        //Add new buttons
        for(int i = 1; i < 6; i++)
        {
            GameObject newButton = Instantiate(mButtonTemplate) as GameObject;

            buttons.Add(newButton);
            newButton.SetActive(true);

            newButton.GetComponent<ButtonListButton>().SetText(i.ToString());
            newButton.GetComponent<ButtonListButton>().SetButtonInfo(i - 1, 2);

            newButton.transform.SetParent(mButtonTemplate.transform.parent, false);
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        if (mButtonSelected)
        {
            selectedButton.GetComponent<ButtonListButton>().SetPressed(false);
        }

        selectedButton = buttons[buttonIndex];

        PlayButton.GetComponent<LoadMapButton>().SetNumPlayers(buttonIndex + 1);

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
