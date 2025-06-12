using System.IO;
using UnityEngine;

public static class GameDataManager
{
    private static string filePath = Application.persistentDataPath + "/gamedata.dat"; // use .dat for encrypted file
    private static GameData gameData;

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
                Debug.LogError("Failed to decrypt or load game data. Resetting to default.");
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

    public static void AddCoins(int amount)
    {
        gameData.coins += amount;
        SaveGameData();
    }

    public static void AddZombieKill()
    {
        gameData.totalZombieKills += 1;
        SaveGameData();
    }

    public static void AddBossKill()
    {
        gameData.totalBossKills += 1;
        SaveGameData();
    }

    public static void AddScore(int score)
    {
        gameData.totalScore += score;
        SaveGameData();
    }

    public static int GetCoins() => gameData.coins;
    public static int GetZombieKills() => gameData.totalZombieKills;
    public static int GetBossKills() => gameData.totalBossKills;
    public static int GetTotalScore() => gameData.totalScore;
}
