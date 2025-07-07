using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    //[SerializeField] private PlayerObject allPlayerDetails;
    [Header("UI Refs")]
    private int[] priceOfPlayers;
    [SerializeField] private Button[] buyPlayersButton;
    [SerializeField] private TextMeshProUGUI[] playerPriceText;

    [SerializeField] private GameObject[] isBuyedObject,NotBuyedObject;
    [SerializeField] private GameObject[] isSelected,NotSelected;

    [SerializeField] private GameObject[] areYouSureSection,yesButtons,NoButtons;
    [SerializeField] private GameObject areYouSureSectionMain;
    [SerializeField] private Button[] yesButtonsButton, NoButtonsButton;

    [SerializeField] private Button[] selectCharButton;


    [SerializeField] private GameObject homeSection, shopSection;
    [SerializeField] private Button backButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameDataManager.LoadGameData();
        StartCoroutine(initializeAllAfter1Sec());
        areYouSureSectionMain.SetActive(false);
        buyPlayersButton[0].onClick.AddListener(onClickPlayer0);
        buyPlayersButton[1].onClick.AddListener(onClickPlayer1);
        buyPlayersButton[2].onClick.AddListener(onClickPlayer2);
        buyPlayersButton[3].onClick.AddListener(onClickPlayer3);
        buyPlayersButton[4].onClick.AddListener(onClickPlayer4);
        buyPlayersButton[5].onClick.AddListener(onClickPlayer5);

        selectCharButton[0].onClick.AddListener(onClickPlayer0);
        selectCharButton[1].onClick.AddListener(onClickPlayer1);
        selectCharButton[2].onClick.AddListener(onClickPlayer2);
        selectCharButton[3].onClick.AddListener(onClickPlayer3);
        selectCharButton[4].onClick.AddListener(onClickPlayer4);
        selectCharButton[5].onClick.AddListener(onClickPlayer5);

        yesButtonsButton[1].onClick.AddListener(onClickYesButton1);
        yesButtonsButton[2].onClick.AddListener(onClickYesButton2);
        yesButtonsButton[3].onClick.AddListener(onClickYesButton3);
        yesButtonsButton[4].onClick.AddListener(onClickYesButton4);
        yesButtonsButton[5].onClick.AddListener(onClickYesButton5);

        NoButtonsButton[1].onClick.AddListener(onClickNoButton1);
        NoButtonsButton[2].onClick.AddListener(onClickNoButton2);
        NoButtonsButton[3].onClick.AddListener(onClickNoButton3);
        NoButtonsButton[4].onClick.AddListener(onClickNoButton4);
        NoButtonsButton[5].onClick.AddListener(onClickNoButton5);

        backButton.onClick.AddListener(onClickBackButton);
    }

    private IEnumerator initializeAllAfter1Sec()
    {
        yield return new WaitForSeconds(1f);
        priceOfPlayers = new int[6]; // Fixed to 6 players based on previous data
        initializePrices();
        initializePlayerPriceUI();
        initializePlayerEnableOrDisableUI();
        disableYesNoSections();
        initializeLastSelectPlayer();
    }

    private void initializeLastSelectPlayer()
    {
        DisableSelection();
        int lastSelectedPlayerIndex = GameDataManager.GetSelectedPlayer();
        if (lastSelectedPlayerIndex >= 0 && lastSelectedPlayerIndex < 6)
        {
            isSelected[lastSelectedPlayerIndex].SetActive(true);
            NotSelected[lastSelectedPlayerIndex].SetActive(false);
        }
    }

    void initializePrices()
    {
        // Hardcoded prices based on previous data (e.g., 0, 1000, 1000, 2000, 2000, 4000)
        priceOfPlayers[0] = 0;   // Axel
        priceOfPlayers[1] = 1000; // Ryder
        priceOfPlayers[2] = 1000; // Zane
        priceOfPlayers[3] = 2000; // Kairo
        priceOfPlayers[4] = 2000; // Draven
        priceOfPlayers[5] = 4000; // Orion
    }

    private void initializePlayerPriceUI()
    {
        for (int i = 0; i < 6; i++)
        {
            playerPriceText[i].text = priceOfPlayers[i].ToString();
        }
    }

    private void initializePlayerEnableOrDisableUI()
    {
        for (int i = 0; i < 6; i++)
        {
            string playerName = GetPlayerNameByIndex(i);
            if (GameDataManager.IsPlayerUnlocked(playerName))
            {
                isBuyedObject[i].SetActive(true);
                NotBuyedObject[i].SetActive(false);
                buyPlayersButton[i].interactable = false;
            }
            else
            {
                isBuyedObject[i].SetActive(false);
                NotBuyedObject[i].SetActive(true);
                buyPlayersButton[i].interactable = true;
            }
        }
    }

    private void DisableSelection()
    {
        for (int i = 0; i < 6; i++)
        {
            isSelected[i].SetActive(false);
            NotSelected[i].SetActive(true);
        }
    }

    private void disableYesNoSections()
    {
        for (int i = 0; i < areYouSureSection.Length; i++)
        {
            areYouSureSection[i].SetActive(false);
            areYouSureSectionMain.SetActive(false);
        }
    }

    private string GetPlayerNameByIndex(int index)
    {
        string[] playerNames = { "Axel", "Ryder", "Zane", "Kairo", "Draven", "Orion" };
        return index >= 0 && index < playerNames.Length ? playerNames[index] : "";
    }

    private void onClickPlayer0()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsPlayerUnlocked("Axel"))
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(0);
            isSelected[0].SetActive(true);
            NotSelected[0].SetActive(false);
        }
        else
        {
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Player not bought yet!");
            Debug.Log("Player not bought yet!");
        }
    }

    private void onClickPlayer1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsPlayerUnlocked("Ryder"))
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(1);
            isSelected[1].SetActive(true);
            NotSelected[1].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= priceOfPlayers[1])
            {
                areYouSureSection[1].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                DisableNotifier.Instance.gameObject.SetActive(true);
                DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
                Debug.Log("Not Enough Coins!");
            }
        }
    }

    private void onClickYesButton1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= priceOfPlayers[1])
        {
            GameDataManager.AddCoins(-priceOfPlayers[1]);
            GameDataManager.UnlockPlayerByName("Ryder");
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(1);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[1].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
            Debug.Log("Not Enough Coins!");
        }
    }

    private void onClickNoButton1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        areYouSureSection[1].SetActive(false);
        areYouSureSectionMain.SetActive(false);
    }

    private void onClickPlayer2()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsPlayerUnlocked("Zane"))
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(2);
            isSelected[2].SetActive(true);
            NotSelected[2].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= priceOfPlayers[2])
            {
                areYouSureSection[2].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                DisableNotifier.Instance.gameObject.SetActive(true);
                DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
                Debug.Log("Not Enough Coins!");
            }
        }
    }

    private void onClickYesButton2()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= priceOfPlayers[2])
        {
            GameDataManager.AddCoins(-priceOfPlayers[2]);
            GameDataManager.UnlockPlayerByName("Zane");
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(2);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[2].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
            Debug.Log("Not Enough Coins!");
        }
    }

    private void onClickNoButton2()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        areYouSureSection[2].SetActive(false);
        areYouSureSectionMain.SetActive(false);
    }

    private void onClickPlayer3()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsPlayerUnlocked("Kairo"))
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(3);
            isSelected[3].SetActive(true);
            NotSelected[3].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= priceOfPlayers[3])
            {
                areYouSureSection[3].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                DisableNotifier.Instance.gameObject.SetActive(true);
                DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
                Debug.Log("Not Enough Coins!");
            }
        }
    }

    private void onClickYesButton3()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= priceOfPlayers[3])
        {
            GameDataManager.AddCoins(-priceOfPlayers[3]);
            GameDataManager.UnlockPlayerByName("Kairo");
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(3);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[3].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
            Debug.Log("Not Enough Coins!");
        }
    }

    private void onClickNoButton3()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        areYouSureSection[3].SetActive(false);
        areYouSureSectionMain.SetActive(false);
    }

    private void onClickPlayer4()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsPlayerUnlocked("Draven"))
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(4);
            isSelected[4].SetActive(true);
            NotSelected[4].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= priceOfPlayers[4])
            {
                areYouSureSection[4].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                DisableNotifier.Instance.gameObject.SetActive(true);
                DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
                Debug.Log("Not Enough Coins!");
            }
        }
    }

    private void onClickYesButton4()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= priceOfPlayers[4])
        {
            GameDataManager.AddCoins(-priceOfPlayers[4]);
            GameDataManager.UnlockPlayerByName("Draven");
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(4);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[4].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
            Debug.Log("Not Enough Coins!");
        }
    }

    private void onClickNoButton4()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        areYouSureSection[4].SetActive(false);
        areYouSureSectionMain.SetActive(false);
    }

    private void onClickPlayer5()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsPlayerUnlocked("Orion"))
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(5);
            isSelected[5].SetActive(true);
            NotSelected[5].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= priceOfPlayers[5])
            {
                areYouSureSection[5].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                DisableNotifier.Instance.gameObject.SetActive(true);
                DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
                Debug.Log("Not Enough Coins!");
            }
        }
    }

    private void onClickYesButton5()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= priceOfPlayers[5])
        {
            GameDataManager.AddCoins(-priceOfPlayers[5]);
            GameDataManager.UnlockPlayerByName("Orion");
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(5);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[5].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
            Debug.Log("Not Enough Coins!");
        }
    }

    private void onClickNoButton5()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        areYouSureSection[5].SetActive(false);
        areYouSureSectionMain.SetActive(false);
    }

    private void onClickBackButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        homeSection.SetActive(true);
        shopSection.SetActive(false);
    }
}
