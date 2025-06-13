using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartGameAndGameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private GameObject[] maps;
    [SerializeField] private GameObject gamePlayPanel,MenuPanel,loadingPanel;
    private Vector3 playerPosAtStart;
    [SerializeField] private Button startButton,mainMenuButton;
    [SerializeField] private GameObject gameOverPanel;
    private void Start()
    {
        startButton.onClick.AddListener(startGame);
        mainMenuButton.onClick.AddListener(goToMainMenu);
        loadingPanel.SetActive(false);
      
        playerPosAtStart= new Vector3(8f, -0.12f, 0f);
            
    }
    private void Update()
    {
       
        if(PlayerHealth.playerCurrentHealth > 0)
        {
            gameOverPanel.SetActive(false);

        }
        if (PlayerHealth.isPlayerDead)
        {
            gameOverPanel.SetActive(true);
            disablePlayerAndMap();
        }
        else
        {
            gameOverPanel.SetActive(false);
        }
            Debug.Log("Player Health: " + PlayerHealth.playerCurrentHealth);
    }
    private void OnEnable()
    {
        
    }
    void startGame()
    {
        gameOverPanel.SetActive(false);
        StartCoroutine(WaitBeforeStartGame());
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = playerPosAtStart;
        }
    }
    IEnumerator WaitBeforeStartGame()
    {
        loadingPanel.SetActive(true);
        MenuPanel.SetActive(false);
        disablePlayerAndMap();
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the game
        gamePlayPanel.SetActive(true);
        players[GameDataManager.GetSelectedPlayer()].SetActive(true);
        //will be dynamic in future
        maps[0].SetActive(true);
        MenuPanel.SetActive(false);
        loadingPanel.SetActive(false);
    }
    IEnumerator gameOver()
    {
        //gameOver UI
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing game over panel
        gameOverPanel.SetActive(true);
    }
    void goToMainMenu()
    {
        StartCoroutine(waitBeforeGotoMenu());
        gameOverPanel.SetActive(false);
    }
    IEnumerator waitBeforeGotoMenu()
    {

        loadingPanel.SetActive(true);
        MenuPanel.SetActive(false);
        gamePlayPanel.SetActive(false);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = playerPosAtStart;
        }
        yield return new WaitForSeconds(2f); // Wait for 1 second before starting the game

        loadingPanel.SetActive(false);
        MenuPanel.SetActive(true);

    }
    void disablePlayerAndMap()
    {
        for(int i = 0; i < players.Length; i++)
        {
            players[i].SetActive(false);
        }   
        for(int i = 0; i < maps.Length; i++)
        {
            maps[i].SetActive(false);
        }
    }

}
