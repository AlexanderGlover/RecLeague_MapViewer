using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleWithScreen : MonoBehaviour
{
    private Vector2 screenBounds;

    public bool maintainAR = true;
    public bool updateTokens = false;
    public float globalScaling = 100.00f;

    void Start()
    {
        ResizeBackgroundToScreen();

        screenBounds = new Vector2(Screen.width, Screen.height);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newScreenBounds = new Vector2(Screen.width, Screen.height);

        if(screenBounds != newScreenBounds)
        {
            if(maintainAR)
            {
                ResizeBackgroundToScreen();
            }
            else
            {
                FillScreenWithImage();
            }

            if(updateTokens)
            {
                foreach (GameObject playerToken in GameObject.FindGameObjectsWithTag("PlayerObject"))
                {
                    UpdatePlayerPosition(playerToken);
                }
            }

            screenBounds = newScreenBounds;
        }
    }
    private void ResizeBackgroundToScreen()
    {
        if(maintainAR)
        {
            SpriteRenderer bgImage = transform.gameObject.GetComponent<SpriteRenderer>();
            RectTransform rectTransform = transform.gameObject.GetComponent<RectTransform>();
            float imgAR = bgImage.sprite.rect.width / bgImage.sprite.rect.height;
            float screenAR = (float)Screen.width / (float)Screen.height;

            float currentHeight = Screen.height;
            float currentWidth = Screen.height * imgAR;

            /*
            Debug.Log("Image AR: " + imgAR);
            Debug.Log("Screen AR: " + screenAR);
            Debug.Log("Screen Height: " + Screen.height);
            Debug.Log("Screen Width: " + Screen.width);
            */

            if(imgAR == screenAR)
            {
                
                rectTransform.localScale = Vector3.one * globalScaling;
            }
            else if(imgAR < screenAR)
            {
                //L&R Black Bars
                //rectTransform.sizeDelta = new Vector2(imgAR * Screen.height, Screen.height);
                float b = Screen.height / bgImage.sprite.rect.height;
                rectTransform.localScale = new Vector3(b, b, 0.0f) * globalScaling;
            }
            else
            {
                //T&B Black Bars
                //rectTransform.sizeDelta = new Vector2(Screen.width, (1 / imgAR) * Screen.width);
                //Vector3 pixelSize = new Vector3(Screen.width, (1 / imgAR) * Screen.width, 0.0f);
                //rectTransform.localScale = pixelSize / bgImage.sprite.rect.size * bgImage.sprite.pixelsPerUnit;

                float a = Screen.width / bgImage.sprite.rect.width;
                rectTransform.localScale = new Vector3(a, a, 0.0f) * globalScaling;
            }
        }
        else
        {
            FillScreenWithImage();
        }
    }

    private void FillScreenWithImage()
    {
        transform.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
    }

    private void UpdatePlayerPosition(GameObject playerToken)
    {
        SpriteRenderer bgImage = transform.gameObject.GetComponent<SpriteRenderer>();

        float imgAR = bgImage.sprite.rect.width / bgImage.sprite.rect.height;
        float oldScreenAR = screenBounds.x / screenBounds.y;
        float screenAR = (float)Screen.width / (float)Screen.height;

        float heightPercent = imgAR < oldScreenAR ? playerToken.transform.localPosition.y / screenBounds.y : playerToken.transform.localPosition.y / ((1/imgAR) * screenBounds.x);
        float widthPercent = imgAR < oldScreenAR ? playerToken.transform.localPosition.x / (imgAR * screenBounds.y) : playerToken.transform.localPosition.x / screenBounds.x;

        float newHeight = imgAR > screenAR ? ((1 / imgAR) * Screen.width * heightPercent) : (Screen.height * heightPercent);
        float newWidth = imgAR > screenAR ? (Screen.width * widthPercent) : (imgAR * Screen.height * widthPercent);

        Vector3 spawnPos = new Vector3(newWidth, newHeight, 0.0f);
        playerToken.transform.localPosition = spawnPos;
    }
}