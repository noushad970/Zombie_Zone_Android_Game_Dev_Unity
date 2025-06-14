using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameCollectionUI : MonoBehaviour
{

    [SerializeField] private Button pauseButton, resumeButton, restartButton, mainMenuButton,yesMainMenu,yesRestart,NoMainMenu,NoRestart,AfterCompleteRestart,AfterCompleteMainMenu;

    [SerializeField] private GameObject pausePanel, areYouSureRestartPanel, areYouSureMainMenuPanel;

    [SerializeField] private TextMeshProUGUI totalCoinCollectText, TotalKillText;
    [SerializeField] private GameObject GameCompletePanel;

    public static int totalCoinCollectedinOneGame = 0,totalKillinOneGame=0;

    private void Start()
    {
        pauseButton.onClick.AddListener(onClickPauseButton);
        resumeButton.onClick.AddListener(onClickResumeButton);
        restartButton.onClick.AddListener(onClickRestartButton);
        mainMenuButton.onClick.AddListener(onClickMainMenuButton);
        yesMainMenu.onClick.AddListener(onClickYesMainMenuButton);
        yesRestart.onClick.AddListener(onClickYesRestartButton);
        NoMainMenu.onClick.AddListener(onClickNoMainMenuButton);
        NoRestart.onClick.AddListener(onClickNoRestartButton);
        AfterCompleteRestart.onClick.AddListener(onClickAfterCompleteGameRestart);
        AfterCompleteMainMenu.onClick.AddListener(onClickAfterCompleteMainMenu);
        GameCompletePanel.SetActive(false); // Initially hide the game complete panel
        areYouSureMainMenuPanel.SetActive(false);
        areYouSureRestartPanel.SetActive(false);
    }

    private void Update()
    {
        totalCoinCollectText.text = totalCoinCollectedinOneGame.ToString(); // Update total coins collected text
        TotalKillText.text = totalKillinOneGame.ToString(); // Update total kills text
        if (InGameCollectionUI.totalKillinOneGame >= 100)
        {
            InGameCollectionUI.totalKillinOneGame = 0; // Reset total kills after showing the panel
             // Reward player with 1000 coins
            StartCoroutine(wait2Sec()); // Start coroutine to wait for 2 seconds before showing the game complete panel
        }
        
    }
    IEnumerator wait2Sec()
    {
        yield return new WaitForSeconds(2f);
        GameCompletePanel.SetActive(true); // Show game complete panel if total kills are 100 or more
        GameDataManager.AddCoins(1000);
    }
    private void OnEnable()
    {
         pausePanel.SetActive(false);
        areYouSureMainMenuPanel.SetActive(false);
        areYouSureRestartPanel.SetActive(false);
    }
    private void onClickPauseButton()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }
    private void onClickResumeButton()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
    private void onClickRestartButton()
    {
        areYouSureRestartPanel.SetActive(true);
        areYouSureMainMenuPanel.SetActive(false);
    }
    private void onClickMainMenuButton()
    {
        areYouSureMainMenuPanel.SetActive(true);
        areYouSureRestartPanel.SetActive(false);
    }
    private void onClickYesMainMenuButton()
    {
        pausePanel.SetActive(false);
        PlayerHealth.isGotoMainMenu = true; // Set flag to indicate going to main menu
        areYouSureMainMenuPanel.SetActive(false);
        areYouSureRestartPanel.SetActive(false);
        Time.timeScale = 1f;
        // Load the main menu scene here, e.g.:
    }
    private void onClickYesRestartButton()
    {
        pausePanel.SetActive(false);
        areYouSureMainMenuPanel.SetActive(false);
        areYouSureRestartPanel.SetActive(false);
        Time.timeScale = 1f;
        PlayerHealth.isRestartGame=true; // Set flag to indicate restarting the game
        // Restart the game here, e.g.:
    }
    private void onClickNoRestartButton()
    {
        areYouSureRestartPanel.SetActive(false);
    }
    private void onClickNoMainMenuButton()
    {
        areYouSureMainMenuPanel.SetActive(false);
    }
    private void onClickAfterCompleteGameRestart()
    {
        GameCompletePanel.SetActive(false);
        pausePanel.SetActive(false);
        areYouSureMainMenuPanel.SetActive(false);
        areYouSureRestartPanel.SetActive(false);
        Time.timeScale = 1f;
        PlayerHealth.isRestartGame = true; // Set flag to indicate restarting the game
    }
    private void onClickAfterCompleteMainMenu()
    {
        pausePanel.SetActive(false);
        PlayerHealth.isGotoMainMenu = true; // Set flag to indicate going to main menu
        areYouSureMainMenuPanel.SetActive(false);
        areYouSureRestartPanel.SetActive(false);
        Time.timeScale = 1f;
        GameCompletePanel.SetActive(false); 
    }
}
