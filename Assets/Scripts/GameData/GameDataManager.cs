using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameDataManager
{
    private static string filePath = Application.persistentDataPath + "/gamedata.dat";
    private static GameData gameData;

    public static PlayerObject playerObject; // Assign externally
    public static MapObject mapObject;       // Assign externally

    public static void SaveGameData()
    {
        // Save player unlocks
        if (playerObject != null)
        {
            gameData.savedPlayers.Clear();
            foreach (var player in playerObject.allPlayersDetails)
            {
                gameData.savedPlayers.Add(new PlayerSaveData
                {
                    playerName = player.playerName,
                    isPlayerBuyed = player.isPlayerBuyed
                });
            }

            gameData.selectedPlayerIndex = playerObject.CurrentlySelectedPlayer;
        }

        // Save map unlocks
        if (mapObject != null)
        {
            gameData.savedMaps.Clear();
            foreach (var map in mapObject.allMaps)
            {
                gameData.savedMaps.Add(new MapSaveData
                {
                    mapName = map.mapName,
                    isMapBuyed = map.isMapBuyed
                });
            }

            gameData.selectedMapIndex = mapObject.currentlySelectedMapIndex;
        }

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

        // Load players
        if (playerObject != null)
        {
            foreach (var save in gameData.savedPlayers)
            {
                var player = playerObject.allPlayersDetails.Find(p => p.playerName == save.playerName);
                if (player != null)
                    player.isPlayerBuyed = save.isPlayerBuyed;
            }

            playerObject.CurrentlySelectedPlayer = gameData.selectedPlayerIndex;
        }

        // Load maps
        if (mapObject != null)
        {
            foreach (var save in gameData.savedMaps)
            {
                var map = mapObject.allMaps.Find(m => m.mapName == save.mapName);
                if (map != null)
                    map.isMapBuyed = save.isMapBuyed;
            }

            mapObject.currentlySelectedMapIndex = gameData.selectedMapIndex;
        }
    }

    // --- Game Progress API ---
    public static void AddCoins(int amount) { gameData.coins += amount;Debug.Log("Added Coin:" + amount); SaveGameData(); }
    public static void AddZombieKill(int amount) { gameData.totalZombieKills+=amount; SaveGameData(); }
    public static void AddBossKill() { gameData.totalBossKills++; SaveGameData(); }
    public static void AddScore(int score) { gameData.totalScore += score; SaveGameData(); }

    public static int GetCoins() => gameData.coins;
    public static int GetZombieKills() => gameData.totalZombieKills;
    public static int GetBossKills() => gameData.totalBossKills;
    public static int GetTotalScore() => gameData.totalScore;

    // --- Player API ---
    public static void UnlockPlayerByName(string name)
    {
        if (playerObject == null) return;
        var player = playerObject.allPlayersDetails.Find(p => p.playerName == name);
        if (player != null)
        {
            player.isPlayerBuyed = true;
            SaveGameData();
        }
    }

    public static bool IsPlayerUnlocked(string name)
    {
        if (playerObject == null) return false;
        var player = playerObject.allPlayersDetails.Find(p => p.playerName == name);
        return player != null && player.isPlayerBuyed;
    }

    public static void SetSelectedPlayer(int index)
    {
        if (playerObject != null)
            playerObject.CurrentlySelectedPlayer = index;

        gameData.selectedPlayerIndex = index;
        SaveGameData();
    }

    public static int GetSelectedPlayer() => gameData.selectedPlayerIndex;

    // --- Map API ---
    public static void UnlockMapByName(string name)
    {
        if (mapObject == null) return;
        var map = mapObject.allMaps.Find(m => m.mapName == name);
        if (map != null)
        {
            map.isMapBuyed = true;
            SaveGameData();
        }
    }

    public static bool IsMapUnlocked(string name)
    {
        if (mapObject == null) return false;
        var map = mapObject.allMaps.Find(m => m.mapName == name);
        return map != null && map.isMapBuyed;
    }

    public static void SetSelectedMapIndex(int index)
    {
        if (mapObject != null)
            mapObject.currentlySelectedMapIndex = index;

        gameData.selectedMapIndex = index;
        SaveGameData();
    }

    public static int GetSelectedMapIndex() => gameData.selectedMapIndex;

   
}
