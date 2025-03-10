using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapImages
{
    [SerializeField] Sprite mapBase;

    //OVERLAYS
    [SerializeField] Sprite overlayCallouts;
    [SerializeField] Sprite[] overlayCtrl;
    [SerializeField] Sprite[] overlayHP;
    [SerializeField] Sprite[] overlaySnD;
    [SerializeField] Sprite[] overlayCTF;
    [SerializeField] Sprite[] overlayKoTH;
    [SerializeField] Sprite[] overlayNeutralBomb;
    [SerializeField] Sprite[] overlayOddball;
    [SerializeField] Sprite[] overlayStrongholds;
    [SerializeField] Sprite[] overlayCompetitive;


    public Sprite GetBaseMap() { return mapBase; }

    public Sprite[] GetCtrlOverlay() { return overlayCtrl; }
    public Sprite[] GetHPOverlay() { return overlayHP; }
    public Sprite[] GetSnDOverlay() { return overlaySnD; }
    public Sprite[] GetCTFOverlay() { return overlayCTF; }
    public Sprite[] GetKoTHOverlay() { return overlayKoTH; }
    public Sprite[] GetNeutralBombOverlay() { return overlayNeutralBomb; }
    public Sprite[] GetOddballOverlay() { return overlayOddball; }
    public Sprite[] GetStrongholdsOverlay() { return overlayStrongholds; }
    public Sprite[] GetCompetitiveOverlay() { return overlayCompetitive; }

    public Sprite GetCalloutOverlay() { return overlayCallouts; }

}

[System.Serializable]
public class SpawnPositions
{
    [SerializeField] SpawnPositionSet spawnCtrl;
    [SerializeField] SpawnPositionSet spawnHP;
    [SerializeField] SpawnPositionSet spawnSnD;
    [SerializeField] SpawnPositionSet spawnCTF;
    [SerializeField] SpawnPositionSet spawnKoTH;
    [SerializeField] SpawnPositionSet spawnNeutralBomb;
    [SerializeField] SpawnPositionSet spawnOddball;
    [SerializeField] SpawnPositionSet spawnSlayer;
    [SerializeField] SpawnPositionSet spawnStrongholds;
    [SerializeField] SpawnPositionSet spawnCompetitive;

    public SpawnPositionSet GetCtrlSpawn() { return spawnCtrl; }
    public SpawnPositionSet GetHPSpawn() { return spawnHP; }
    public SpawnPositionSet GetSnDSpawn() { return spawnSnD; }
    public SpawnPositionSet GetCTFSpawn() { return spawnCTF; }
    public SpawnPositionSet GetKoTHSpawn() { return spawnKoTH; }
    public SpawnPositionSet GetNeutralBombSpawn() { return spawnNeutralBomb; }
    public SpawnPositionSet GetOddballSpawn() { return spawnOddball; }
    public SpawnPositionSet GetSlayerSpawn() { return spawnSlayer; }
    public SpawnPositionSet GetStrongholdsSpawn() { return spawnStrongholds; }
    public SpawnPositionSet GetCompetitiveSpawn() { return spawnCompetitive; }
}

[System.Serializable]
public class SpawnPositionSet
{
    [SerializeField] bool mInitialized;
    [SerializeField] SpawnPoint redSpawn;
    [SerializeField] SpawnPoint blueSpawn;

    public SpawnPoint GetSpawn(string team)
    {
        switch(team)
        {
            case "blue":
                return blueSpawn;
            case "red":
                return redSpawn;
            default:
                return new SpawnPoint();
        }
    }

    public bool GetIsInitialized() { return mInitialized; }
}

[System.Serializable]
public class SpawnPoint
{
    [SerializeField] Vector3 mSpawnPosition;
    [SerializeField] Vector3 mSpawnDirectionVector;

    public Vector3 GetSpawnPosition() { return mSpawnPosition; }
    public Vector3 GetSpawnDirection() { return mSpawnDirectionVector; }
}

[System.Serializable]
public class MapInfo
{
    [SerializeField] string mName = "";
    [SerializeField] bool mCompetetive;
    [SerializeField] MapImages mMapImages;
    [SerializeField] SpawnPositions mSpawnPositions;

    public string GetName() { return mName; }
    public bool GetIsCompetetive() { return mCompetetive; }
    public MapImages GetMapImages() { return mMapImages; }
    public SpawnPositions GetSpawnPositions() { return mSpawnPositions; }
}

[System.Serializable]
public class GameModeInfo
{
    [SerializeField] string mName = "";
    [SerializeField] MapInfo[] mAvailableMaps;

    public string GetName() { return mName; }
    public MapInfo[] GetAvailableMaps() { return mAvailableMaps; }
}

[System.Serializable]
public class GameData
{
    [SerializeField] string mName;

    [SerializeField] bool mIsGameActive;

    [SerializeField] Sprite mLeagueIcon;
    [SerializeField] Sprite mBannerImage;
    [SerializeField] Sprite mMapSelectBackground;

    [SerializeField] GameModeInfo[] mGameModes;
    [SerializeField] MapInfo[] mMaps;

    [SerializeField] string mAssetsPath;

    public bool GetIsGameActive() { return mIsGameActive; }
    public string GetGameName() { return mName; }
    public Sprite GetBannerImage() { return mBannerImage; }
    public Sprite GetLeagueIcon() { return mLeagueIcon;  }
    public MapInfo[] GetMaps() { return mMaps;  }
    public GameModeInfo[] GetGameModes() { return mGameModes; }
}

public class ApplicationData : MonoBehaviour
{
    [SerializeField]
    public GameData[] GameDataStorage;

    public int GameSelectedIndex = -1;
    public GameModeInfo GameModeSelected;
    public MapInfo GameMapSelected;

    public ApplicationPointer ApplicationPointerData;
}