using UnityEngine;

public class InitializeData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameDataManager.LoadGameData(); // Load existing game data

        // Check if it's the first time the game is played
        if (GameDataManager.IsFirstTime())
        {
            // Initialize default player and map data manually
            var initialPlayers = new System.Collections.Generic.List<PlayerSaveData>
            {
                new PlayerSaveData { playerName = "Axel", isPlayerBuyed = false },
                new PlayerSaveData { playerName = "Ryder", isPlayerBuyed = false },
                new PlayerSaveData { playerName = "Zane", isPlayerBuyed = false },
                new PlayerSaveData { playerName = "Kairo", isPlayerBuyed = false },
                new PlayerSaveData { playerName = "Draven", isPlayerBuyed = false },
                new PlayerSaveData { playerName = "Orion", isPlayerBuyed = false }
            };

            var initialMaps = new System.Collections.Generic.List<MapSaveData>
            {
                new MapSaveData { mapName = "Halloween", isMapBuyed = false },
                new MapSaveData { mapName = "FantasyForest", isMapBuyed = false }
            };

            GameDataManager.InitializeGameData(initialPlayers, initialMaps); // Set initial data
            GameDataManager.UnlockPlayerByName("Axel"); // Unlock the first player
            GameDataManager.UnlockMapByName("Halloween"); // Unlock the first map
            GameDataManager.UnlockMapByName("FantasyForest"); // Unlock the second map
            GameDataManager.SetFirstTime(false); // Set flag to false after initialization
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}