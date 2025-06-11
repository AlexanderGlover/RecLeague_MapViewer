using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenSize
{
    public static Vector2 GetScreenToWorldVector
    {
        get
        {
            Vector2 topRightCorner = new Vector2(-1, 1);
            Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
            var vector = new Vector2(edgeVector.x * 2, edgeVector.y * 2);
            return vector;
        }
    }
}

public class GameTabManager : MonoBehaviour
{
    GameData[] gameData;
    List<GameObject> mButtons = new List<GameObject>();

    [SerializeField]
    Canvas canvas;

    private Vector2 screenSize = new Vector2();

    void Start()
    {
        gameData = GameObject.Find("DataObject").GetComponent<ApplicationData>().GameDataStorage;
        foreach (GameData game in gameData)
        {
            Sprite img = game.GetBannerImage();
            GameObject newButton = new GameObject();
            newButton.name = game.GetGameName();
            newButton.transform.parent = canvas.transform;

            newButton.AddComponent<RectTransform>();

            //Adding sprite from game data to object
            newButton.AddComponent<Image>();
            newButton.GetComponent<Image>().sprite = img;

            //Adding button functionality to empty game object
            newButton.AddComponent<Button>();
            Button buttonObj = newButton.GetComponent<Button>();

            if (game.GetIsGameActive())
            {
                buttonObj.onClick.AddListener(delegate { GameSelected(Array.IndexOf(gameData, game)); });
            }

            ColorBlock cb = buttonObj.colors;
            cb.normalColor = new Color(0.25f, 0.25f, 0.25f, 1);
            cb.pressedColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            Navigation noNav = new Navigation();
            noNav.mode = Navigation.Mode.None;
            buttonObj.navigation = noNav;
            buttonObj.colors = cb;

            mButtons.Add(newButton);
        }
    }

    void Update()
    {
        int numOfButtons = mButtons.Count;

        Vector2 newScreenSize = ScreenSize.GetScreenToWorldVector;

        if(newScreenSize != screenSize)
        {
            screenSize = newScreenSize;
            switch (numOfButtons)
            {
                case 3:
                    int i = -1;
                    foreach (GameObject button in mButtons)
                    {
                        RectTransform rt = button.GetComponent<RectTransform>();
                        Image img = button.GetComponent<Image>();

                        ColorBlock cb = button.GetComponent<Button>().colors;

                        rt.anchorMin = new Vector2(0.333f + (i * 0.333f), 0);
                        rt.anchorMax = new Vector2(1 + ((i - 1) * 0.333f), 1);
                        rt.pivot = new Vector2(0.5f, 0.5f);

                        rt.localScale = new Vector3(1, 1, 1);
                        rt.sizeDelta = new Vector2(0,0);
                        i++;
                    }
                    break;
                case 4:
                    int j = -1;
                    foreach (GameObject button in mButtons)
                    {
                        RectTransform rt = button.GetComponent<RectTransform>();
                        Image img = button.GetComponent<Image>();

                        ColorBlock cb = button.GetComponent<Button>().colors;

                        rt.anchorMin = new Vector2(0.25f + (j * 0.25f), 0);
                        rt.anchorMax = new Vector2(1 + ((j - 1) * 0.25f), 1);
                        rt.pivot = new Vector2(0.5f, 0.5f);

                        rt.localScale = new Vector3(1, 1, 1);
                        rt.sizeDelta = new Vector2(0, 0);
                        j++;
                    }
                    break;
                case 6:
                    int k = 0;
                    foreach (GameObject button in mButtons)

                    {
                        float rowHeight = 0f;
                        if (k > 2)
                        {
                            rowHeight = 0f;
                        } else
                        {
                            rowHeight = 0.5f;
                        }

                        RectTransform rt = button.GetComponent<RectTransform>();
                        Image img = button.GetComponent<Image>();

                        ColorBlock cb = button.GetComponent<Button>().colors;
                        int horizontalIndex = k % 3 - 1;
                        rt.anchorMin = new Vector2(0.333f + ((horizontalIndex) * 0.333f), rowHeight);
                        rt.anchorMax = new Vector2(1 + (((horizontalIndex) - 1) * 0.333f), 0.5f + rowHeight);
                        rt.pivot = new Vector2(0.5f, 0.5f);

                        rt.localScale = new Vector3(1, 1, 1);
                        rt.sizeDelta = new Vector2(0, 0);
                        k++;
                    }
                    break;             
                default:
                    break;
            }
        }

        
    }

    public void GameSelected(int index)
    {
        ApplicationData appData = GameObject.Find("DataObject").GetComponent<ApplicationData>();

        appData.GameSelectedIndex = index;

        SceneManager.LoadScene("MapSelect");
    }
}
