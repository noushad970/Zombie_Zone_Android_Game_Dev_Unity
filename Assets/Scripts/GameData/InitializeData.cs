using UnityEngine;

public class InitializeData : MonoBehaviour
{
    [SerializeField]
    private PlayerObject allPlayerDetails;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        GameDataManager.playerObject = allPlayerDetails;
        GameDataManager.LoadGameData();
        GameDataManager.UnlockPlayerByName("Axel");
        GameDataManager.IsPlayerUnlocked("Axel");
        GameDataManager.IsPlayerUnlocked("Zane"); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
