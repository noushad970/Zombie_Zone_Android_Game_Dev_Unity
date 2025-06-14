using System.Collections.Generic;
[System.Serializable]
public class MapSaveData
{
    public string mapName;
    public bool isMapBuyed;
}


[System.Serializable]
public class GameData
{
    public int coins=0;
    public int totalZombieKills=0;
    public int totalBossKills=0;
    public int totalScore=0;
    public int selectedPlayerIndex = 0;
    public int selectedMapIndex = 0;
    public List<MapSaveData> savedMaps = new List<MapSaveData>();
    public List<PlayerSaveData> savedPlayers = new List<PlayerSaveData>();
    public bool isFirstTime = true;
}

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public bool isPlayerBuyed;
}
