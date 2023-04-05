using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject mButtonTemplate;
    [SerializeField]
    private GameObject PlayButton;

    private List<GameObject> compButtons;
    private List<GameObject> casualButtons;

    [SerializeField]
    private GameObject mMapTypeUnderline;

    GameData gameData;

    bool mCompetetiveMaps = true;

    GameObject selectedButton;
    bool mButtonSelected = false;

    MapInfo[] mAvailableMaps;
    bool mAvailableMapsSaved = false;

    private void Start()
    {
        GenerateList();
        selectedButton = new GameObject();
    }

    public MapInfo GetSelectedMap()
    {
        foreach(MapInfo map in gameData.GetMaps())
        {
            if(map.GetName() == selectedButton.GetComponent<ButtonListButton>().GetText())
            {
                return map;
            }
        }
        return new MapInfo();
    }

    void GenerateList()
    {
        compButtons = new List<GameObject>();
        casualButtons = new List<GameObject>();

        if (GameObject.Find("DataObject") == null) { return; }

        ApplicationData applicationData = GameObject.Find("DataObject").GetComponent<ApplicationData>();
        gameData = applicationData.GameDataStorage[applicationData.GameSelectedIndex];

        //Clear existing Buttons
        compButtons = ClearButtonList(compButtons);
        casualButtons = ClearButtonList(casualButtons);

        int compIndex = 0;
        int casualIndex = 0;

        //Add new buttons
        foreach (MapInfo map in gameData.GetMaps())
        {
            GameObject newButton = Instantiate(mButtonTemplate) as GameObject;

            newButton.GetComponent<ButtonListButton>().SetText(map.GetName());            
            newButton.GetComponent<ButtonListButton>().SetButtonInfo(map.GetIsCompetetive() ? compIndex : casualIndex, 0);

            if (map.GetIsCompetetive())
            {
                compButtons.Add(newButton);
                newButton.SetActive(mCompetetiveMaps);
                compIndex++;
            }
            else
            {
                casualButtons.Add(newButton);
                newButton.SetActive(!mCompetetiveMaps);
                casualIndex++;
            }
            

            newButton.transform.SetParent(mButtonTemplate.transform.parent, false);
        }
    }

    public void ButtonClicked(int buttonIndex)
    {
        if(mButtonSelected)
        {
            selectedButton.GetComponent<ButtonListButton>().SetPressed(false);
        }

        selectedButton = mCompetetiveMaps ? compButtons[buttonIndex] : casualButtons[buttonIndex];

        PlayButton.GetComponent<LoadMapButton>().SetMapInfo(GetSelectedMap());

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

    public void ShowCompetetiveMaps(bool isCompetetive)
    {
        ClearSelected();

        mCompetetiveMaps = isCompetetive;

        mMapTypeUnderline.GetComponent<RectTransform>().localPosition = isCompetetive ? new Vector3(-400, 210, 0) : new Vector3(000, 210, 0); 

        foreach(GameObject button in compButtons)
        {
            button.SetActive(isCompetetive);
        }

        foreach (GameObject button in casualButtons)
        {
            button.SetActive(!isCompetetive);
        }

        SetAvailableMaps(mAvailableMaps);
    }

    public void ClearSelected()
    {
        if(mCompetetiveMaps)
        {
            foreach (GameObject button in compButtons)
            {
                button.GetComponent<ButtonListButton>().SetPressed(false);
            }
        }
        else
        {
            foreach (GameObject button in casualButtons)
            {
                button.GetComponent<ButtonListButton>().SetPressed(false);
            }
        }

        mButtonSelected = false;
        selectedButton = new GameObject();
        PlayButton.GetComponent<LoadMapButton>().SetMapInfo(new MapInfo());
    }

    public void SaveAvailableMaps(MapInfo[] availableMaps) 
    { 
        mAvailableMaps = availableMaps;
        mAvailableMapsSaved = true;
    }

    public void SetAvailableMaps(MapInfo[] availableMaps)
    {
        SaveAvailableMaps(availableMaps);

        if (!mAvailableMapsSaved || mAvailableMaps.Length == 0)
        {
            return;
        }

        if (mCompetetiveMaps)
        {
            foreach (GameObject button in compButtons)
            {
                bool availableMap = false;
                foreach(MapInfo map in mAvailableMaps)
                {
                    if(map.GetName() == button.GetComponent<ButtonListButton>().GetText())
                    {
                        availableMap = true;
                    }
                }

                if(mButtonSelected && button == selectedButton && !availableMap)
                {
                    ClearSelected();
                }

                button.GetComponent<Button>().interactable = availableMap;
            }
        }
        else
        {
            foreach (GameObject button in casualButtons)
            {
                bool availableMap = false;
                foreach (MapInfo map in mAvailableMaps)
                {
                    if (map.GetName() == button.GetComponent<ButtonListButton>().GetText())
                    {
                        availableMap = true;
                    }
                }

                if (mButtonSelected && button == selectedButton && !availableMap)
                {
                    ClearSelected();
                }

                button.GetComponent<Button>().interactable = availableMap;
            }
        }
    }

    void SetActiveMap(int buttonIndex)
    {
        //Get map index from button index

    }
}
