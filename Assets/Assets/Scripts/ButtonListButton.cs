using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    [SerializeField]
    GameObject textObj;

    [SerializeField]
    GameObject parentContainer;

    GameModeInfo mGameModeInfo;

    Button mParentButton;
    Image mButtonImage;
    bool buttonInteractable;

    int mIndex;
    int mType;

    bool mIsPressed = false;

    public void SetGameModeInfo(GameModeInfo gameModeInfo) { mGameModeInfo = gameModeInfo;  }
    public GameModeInfo GetGameModeInfo() { return mGameModeInfo; }

    public void SetText(string textString)
    {
        textObj.GetComponent<TMPro.TextMeshProUGUI>().text = textString;
    }

    public string GetText() { return textObj.GetComponent<TMPro.TextMeshProUGUI>().text;  }

    public void SetButtonInfo(int index, int type)
    {
        mIndex = index;

        // 0: Map, 1: Game Mode, 2: # of Players
        mType = type;
    }

    public void SetPressed(bool isPressed)
    {
        if(isPressed == mIsPressed)
        {
            return;
        }

        mIsPressed = isPressed;

        transform.GetChild(1).GetComponent<Image>().color = isPressed ? new Color(0.2352941f, 0.2705882f, 1, 1) : new Color(1, 1, 1, 1);

        if (isPressed)
        {
            switch (mType)
            { 
                case 0:
                    parentContainer.GetComponent<ButtonListControl>().ButtonClicked(mIndex);
                    break;
                case 1:
                    parentContainer.GetComponent<GameModeListControl>().ButtonClicked(mIndex);
                    break;
                case 2:
                    parentContainer.GetComponent<PlayerNumListScript>().ButtonClicked(mIndex);
                    break;
                default:
                    break;
            }

            
        }
    }

    public void Start()
    {
        mParentButton = gameObject.GetComponent<Button>();
        mButtonImage = transform.GetChild(1).GetComponent<Image>();
    }

    public void Update()
    {
        if(buttonInteractable != mParentButton.interactable)
        {
            buttonInteractable = !buttonInteractable;

            mButtonImage.color = buttonInteractable ? new Color(1f, 1f, 1, 1f) : new Color(1f, 1f, 1f, 0.1f);
        }
    }
}
