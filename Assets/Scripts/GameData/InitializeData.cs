using UnityEngine;

public class InitializeData : MonoBehaviour
{
    [SerializeField]
    private PlayerObject allPlayerDetails;
    [SerializeField] MapObject allMapDetails;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GameDataManager.playerObject = allPlayerDetails;
        GameDataManager.mapObject = allMapDetails;
        GameDataManager.UnlockPlayerByName(allPlayerDetails.allPlayersDetails[0].playerName.ToString());
        GameDataManager.UnlockMapByName(allMapDetails.allMaps[0].mapName.ToString());
        GameDataManager.UnlockMapByName(allMapDetails.allMaps[1].mapName.ToString());
        GameDataManager.SaveGameData();
        GameDataManager.LoadGameData();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
