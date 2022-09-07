using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapInfo
{
    [SerializeField] string mName;

    [SerializeField] bool mCompetetive;
}

[System.Serializable]
public class GameModeInfo
{
    [SerializeField] MapInfo[] mAvailableMaps;
}

[System.Serializable]
public class GameData
{
    [SerializeField] string mName;
    [SerializeField] Sprite mLeagueIcon;
    [SerializeField] Sprite mBannerImage;
    [SerializeField] Sprite mMapSelectBackground;

    [SerializeField] GameModeInfo[] mGameModes;
    [SerializeField] MapInfo[] mMaps;

    [SerializeField] string mAssetsPath;

    public string GetGameName() { return mName; }
    public Sprite GetBannerImage() { return mBannerImage; }
    public Sprite GetLeagueIcon() { return mLeagueIcon;  }
}

public class ApplicationData : MonoBehaviour
{
    [SerializeField]
    public GameData[] GameDataStorage;

    public int GameSelectedIndex = -1;
}
