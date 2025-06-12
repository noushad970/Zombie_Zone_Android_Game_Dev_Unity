using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int coins;
    public int totalZombieKills;
    public int totalBossKills;
    public int totalScore;
    public List<PlayerSaveData> savedPlayers = new List<PlayerSaveData>();
}

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public bool isPlayerBuyed;
}
