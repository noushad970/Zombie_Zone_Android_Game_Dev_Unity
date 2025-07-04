using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
      
        playerPosAtStart= new Vector3(3f, 1.12f, 0f);
            
    }
    private void Update()
    {
       
        ConditionalFunction();

    }

    private void ConditionalFunction()
    {
        if (PlayerHealth.playerCurrentHealth > 0)
        {
            gameOverPanel.SetActive(false);

        }
        if (PlayerHealth.isGotoMainMenu)
        {
            goToMainMenu();
            disablePlayerAndMap();
            PlayerHealth.isGotoMainMenu = false; // Reset the flag
            DestroyAllClones();
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
        if (MenuPanel.activeSelf && !gamePlayPanel.activeSelf)
        {
            DestroyAllClones();
        }
        if (PlayerHealth.isRestartGame)
        {
            PlayerHealth.isRestartGame = false; // Reset the flag
            DestroyAllClones();
            startGame();
        }
    }
    private void OnEnable()
    {
        
    }
    public void DestroyAllClones()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains("Clone"))
            {
                Destroy(obj);
            }
        }

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
        gamePlayPanel.SetActive(false);
        disablePlayerAndMap();
        yield return new WaitForSeconds(2f); // Wait for 1 second before starting the game
        gamePlayPanel.SetActive(true);
        players[GameDataManager.GetSelectedPlayer()].SetActive(true);
        //will be dynamic in future
        maps[GameDataManager.GetSelectedMapIndex()].SetActive(true);
        MenuPanel.SetActive(false);
        loadingPanel.SetActive(false);
    }
    IEnumerator gameOver()
    {
        //gameOver UI
        yield return new WaitForSeconds(1f); // Wait for 1 second before showing game over panel
        gameOverPanel.SetActive(true);
        SceneManager.LoadScene("Gameplay"); // Load the main menu scene
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
        SceneManager.LoadScene("Gameplay"); // Load the main menu scene

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
