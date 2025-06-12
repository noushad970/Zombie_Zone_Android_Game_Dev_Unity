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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
