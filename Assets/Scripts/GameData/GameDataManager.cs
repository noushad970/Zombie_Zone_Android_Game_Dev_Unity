using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameDataManager
{
    private static string filePath = Application.persistentDataPath + "/gamedata.dat";
    private static GameData gameData = new GameData();

    public static void SaveGameData()
    {
        string json = JsonUtility.ToJson(gameData, true);
        string encryptedJson = AESHelper.Encrypt(json);
        File.WriteAllText(filePath, encryptedJson);
    }

    public static void LoadGameData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string encryptedJson = File.ReadAllText(filePath);
                string json = AESHelper.Decrypt(encryptedJson);
                gameData = JsonUtility.FromJson<GameData>(json);
            }
            catch
            {
                Debug.LogError("Failed to load or decrypt game data. Resetting to default.");
                gameData = new GameData();
                SaveGameData();
            }
        }
        else
        {
            gameData = new GameData();
            SaveGameData();
        }
    }

    // --- Game Progress API ---
    public static void AddCoins(int amount) { gameData.coins += amount; Debug.Log("Added Coin:" + amount); SaveGameData(); }
    public static void AddZombieKill(int amount) { gameData.totalZombieKills += amount; SaveGameData(); }
    public static void AddBossKill() { gameData.totalBossKills++; SaveGameData(); }
    public static void AddScore(int score) { gameData.totalScore += score; SaveGameData(); }

    public static int GetCoins() => gameData.coins;
    public static int GetZombieKills() => gameData.totalZombieKills;
    public static int GetBossKills() => gameData.totalBossKills;
    public static int GetTotalScore() => gameData.totalScore;

    // --- Player API ---
    public static void UnlockPlayerByName(string name)
    {
        var player = gameData.savedPlayers.Find(p => p.playerName == name);
        if (player != null)
        {
            player.isPlayerBuyed = true;
            SaveGameData();
        }
    }

    public static bool IsPlayerUnlocked(string name)
    {
        var player = gameData.savedPlayers.Find(p => p.playerName == name);
        return player != null && player.isPlayerBuyed;
    }

    public static void SetSelectedPlayer(int index)
    {
        gameData.selectedPlayerIndex = index;
        SaveGameData();
    }

    public static int GetSelectedPlayer() => gameData.selectedPlayerIndex;

    // --- Map API ---
    public static void UnlockMapByName(string name)
    {
        var map = gameData.savedMaps.Find(m => m.mapName == name);
        if (map != null)
        {
            map.isMapBuyed = true;
            SaveGameData();
        }
    }

    public static bool IsMapUnlocked(string name)
    {
        var map = gameData.savedMaps.Find(m => m.mapName == name);
        return map != null && map.isMapBuyed;
    }

    public static void SetSelectedMapIndex(int index)
    {
        gameData.selectedMapIndex = index;
        SaveGameData();
    }

    public static int GetSelectedMapIndex() => gameData.selectedMapIndex;

    // New method to initialize game data
    public static void InitializeGameData(List<PlayerSaveData> players, List<MapSaveData> maps)
    {
        gameData.savedPlayers.Clear();
        gameData.savedPlayers.AddRange(players);
        gameData.savedMaps.Clear();
        gameData.savedMaps.AddRange(maps);
        SaveGameData();
    }

    // Methods to access and modify isFirstTime
    public static bool IsFirstTime()
    {
        return gameData.isFirstTime;
    }

    public static void SetFirstTime(bool value)
    {
        gameData.isFirstTime = value;
        SaveGameData();
    }
}