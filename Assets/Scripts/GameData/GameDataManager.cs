using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class GameDataManager
{
    private static string filePath = Application.persistentDataPath + "/gamedata.dat";
    private static GameData gameData;

    public static PlayerObject playerObject; // Assign this externally at runtime if needed

    
    public static void SaveGameData()
    {
        // Sync player unlocks before saving
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

        // Apply saved unlocks to playerObject
        if (playerObject != null)
        {
            foreach (var save in gameData.savedPlayers)
            {
                var player = playerObject.allPlayersDetails.Find(p => p.playerName == save.playerName);
                if (player != null)
                {
                    player.isPlayerBuyed = save.isPlayerBuyed;
                }
            }
        }
    }

    // --- Game Progress API ---
    public static void AddCoins(int amount) { gameData.coins += amount; SaveGameData(); }
    public static void AddZombieKill() { gameData.totalZombieKills += 1; SaveGameData(); }
    public static void AddBossKill() { gameData.totalBossKills += 1; SaveGameData(); }
    public static void AddScore(int score) { gameData.totalScore += score; SaveGameData(); }

    public static int GetCoins() => gameData.coins;
    public static int GetZombieKills() => gameData.totalZombieKills;
    public static int GetBossKills() => gameData.totalBossKills;
    public static int GetTotalScore() => gameData.totalScore;

    // --- Player Unlock API ---
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
}
