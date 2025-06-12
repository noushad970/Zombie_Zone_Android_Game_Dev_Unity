using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    public int coins=0;
    public int totalZombieKills;
    public int totalBossKills;
    public int totalScore;
    public int selectedPlayerIndex = 0;
    public List<PlayerSaveData> savedPlayers = new List<PlayerSaveData>();
    public bool isFirstTime = true;
}

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public bool isPlayerBuyed;
}
