using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerToken : MonoBehaviour
{
    public GameObject playerObjectPrefab;

    public LayerMask playerTokenLayerMask;
    public Canvas mainCanvas;

    private Vector3 offset;

    private EventSystem mEventSystem;
    private GraphicRaycaster mGRaycaster;

    private PointerEventData mPointerEventData;
    private GameObject mSelectedObject;
    private int mSelectObjectMode = -1;

    private InputController inptCtrl;

    public GameObject MapObject;

    //Player Generation Settings
    private int mPlayersPerTeam = 4;
    public bool useCustomSpawnLocations = false;
    public SpawnPositionSet mInitialSpawnLocations;
    public SpawnConfigurations spawnConfig;
    public Color[] teamColours;

    public enum SpawnConfigurations
    {
        Square,
        wLine,
        Line
    };

    private void Start()
    {
        mGRaycaster = mainCanvas.GetComponent<GraphicRaycaster>();
        mEventSystem = GetComponent<EventSystem>();

        LayerMask playerIconMask = new LayerMask();
        playerIconMask.value = LayerMask.GetMask("PlayerIcons");
        mGRaycaster.blockingMask = playerIconMask;

        GeneratePlayers();

        //GetInputController
        inptCtrl = GameObject.Find("InputObject").GetComponent<InputController>();
    }

    void Update()
    {
        //Print location of pointer relative to middle & screen height (make sure image fills height)
        if(inptCtrl.GetKeyDown(KeybindingAction.KA_Select))
        {
            Debug.Log("Image Height: " + Screen.height);
            Debug.Log("Spawn Location: " + (Input.mousePosition.x - Screen.width / 2) + " " + (Input.mousePosition.y - Screen.height / 2));
            Debug.Log(Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0.0f));
        }


        Vector3 relativeMousePosition = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0.0f);

        if (inptCtrl.GetKeyDown(KeybindingAction.KA_Select) || inptCtrl.GetKeyDown(KeybindingAction.KA_Rotate))
        {
            //Set up the new Pointer Event
            mPointerEventData = new PointerEventData(mEventSystem);
            //Set the Pointer Event Position to that of the mouse position
            mPointerEventData.position = Input.mousePosition;

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            mGRaycaster.Raycast(mPointerEventData, results);


            foreach (RaycastResult result in results)
            {
                if (result.gameObject.tag == "PlayerObject")
                {
                    mSelectedObject = result.gameObject;
                    offset = relativeMousePosition - mSelectedObject.transform.localPosition;

                    mSelectObjectMode = inptCtrl.GetKey(KeybindingAction.KA_Select) ? 0 : 1;
                }
            }
        }
        if (mSelectedObject)
        {
            if (inptCtrl.GetKey(KeybindingAction.KA_Rotate) && mSelectObjectMode == 1)
            {
                Vector3 offsetVector = relativeMousePosition - mSelectedObject.transform.localPosition;
                float vectorAngle = Mathf.Atan2(offsetVector.y, offsetVector.x) * 180 / Mathf.PI;
                mSelectedObject.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, vectorAngle));
            }

            if(inptCtrl.GetKey(KeybindingAction.KA_Select) && mSelectObjectMode == 0)
            {
                mSelectedObject.transform.localPosition = (relativeMousePosition - offset);
            }
        }
        if(inptCtrl.GetKeyUp(KeybindingAction.KA_Select) || inptCtrl.GetKeyUp(KeybindingAction.KA_Rotate) && mSelectedObject)
        {
            mSelectedObject = null;
            mSelectObjectMode = -1;
        }
    }

    private void GeneratePlayers()
    {
        SpawnPositionSet spawnPoints = GetSpawnLocations();

        if(spawnPoints != null)
        {
            if (spawnPoints.GetIsInitialized() && useCustomSpawnLocations)
            {
                GenerateTeam(spawnPoints.GetSpawn("red"), 0);
                GenerateTeam(spawnPoints.GetSpawn("blue"), 1);
            }
            else
            {
                GenerateTeam(mInitialSpawnLocations.GetSpawn("red"), 0);
                GenerateTeam(mInitialSpawnLocations.GetSpawn("blue"), 1);
            }
        }
    }

    private void GenerateTeam(SpawnPoint spawnLoc, int colorCode)
    {
        switch (spawnConfig)
        {
            case SpawnConfigurations.Square:
                break;
            case SpawnConfigurations.Line:
                for (int i = 0; i < mPlayersPerTeam; i++)
                {
                    CreatePlayerObject(spawnLoc, new Vector3(0.0f, 20.0f, 0.0f) * i, colorCode, i + 1);
                }
                break;
            case SpawnConfigurations.wLine:
            default:
                Vector3[] offsets = new[] { new Vector3(-27.5f, -12.5f, 0.0f), new Vector3(7.5f, -12.5f, 0.0f), new Vector3(-7.5f, 12.5f, 0.0f), new Vector3(27.5f, 12.5f, 0.0f)};
                for (int i = 0; i < mPlayersPerTeam; i++)
                {
                    CreatePlayerObject(spawnLoc, offsets[i], colorCode, i+1);
                }
                break;
        };
    }

    private void CreatePlayerObject(SpawnPoint spawnLoc, Vector3 offset, int colorCode, int playerNum)
    {
        GameObject newPlayer = Instantiate(playerObjectPrefab) as GameObject;

        SpriteRenderer bgImage = MapObject.GetComponent<SpriteRenderer>();

        float imgAR = bgImage.sprite.rect.width / bgImage.sprite.rect.height;
        float screenAR = (float)Screen.width / (float)Screen.height;

        //Find new position from ratio of recorded position (MUST HAVE IMG HEIGHT = WINDOW HEIGHT)
        float totalHeight = spawnLoc.GetSpawnPosition().z;
        float spawnHeight = spawnLoc.GetSpawnPosition().y;
        float spawnWidth = spawnLoc.GetSpawnPosition().x;

        //Get Screen Percentages
        float spawnHPercent = spawnHeight / totalHeight;
        float spawnWPercent = spawnWidth / (totalHeight * imgAR);

        float newHeight = imgAR > screenAR ? ((1 / imgAR) * Screen.width * spawnHPercent) : (Screen.height * spawnHPercent);
        float newWidth = imgAR > screenAR ? (Screen.width * spawnWPercent) : (imgAR * Screen.height * spawnWPercent);

        Vector3 spawnPos = new Vector3(newWidth, newHeight, 0.0f);
        newPlayer.transform.position = spawnPos + offset;


        float vectorAngle = Mathf.Atan2(spawnLoc.GetSpawnDirection().y, spawnLoc.GetSpawnDirection().x) * 180 / Mathf.PI;
        newPlayer.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, vectorAngle));
        newPlayer.transform.SetParent(playerObjectPrefab.transform.parent, false);
        newPlayer.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = playerNum.ToString();
        newPlayer.GetComponent<Image>().color = teamColours[colorCode];
        newPlayer.SetActive(true);
    }

    SpawnPositionSet GetSpawnLocations()
    {
        GameObject dataObj = GameObject.Find("DataObject");
        if (dataObj != null)
        {
            ApplicationData appData = dataObj.GetComponent<ApplicationData>();

            switch (appData.GameModeSelected.GetName())
            {
                case "Hardpoint":
                    return appData.GameMapSelected.GetSpawnPositions().GetHPSpawn();
                case "Search & Destroy":
                    return appData.GameMapSelected.GetSpawnPositions().GetSnDSpawn();
                case "Control":
                    return appData.GameMapSelected.GetSpawnPositions().GetCtrlSpawn();
                case "Capture the Flag":
                    return appData.GameMapSelected.GetSpawnPositions().GetCTFSpawn();
                case "King of the Hill":
                    return appData.GameMapSelected.GetSpawnPositions().GetKoTHSpawn();
                case "Oddball":
                    return appData.GameMapSelected.GetSpawnPositions().GetOddballSpawn();
                case "Slayer":
                    return appData.GameMapSelected.GetSpawnPositions().GetSlayerSpawn();
                case "Strongholds":
                    return appData.GameMapSelected.GetSpawnPositions().GetStrongholdsSpawn();
                default:
                    break;
            }
        }
        return null;
    }
}
