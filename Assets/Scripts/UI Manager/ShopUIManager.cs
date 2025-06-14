using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField] private PlayerObject allPlayerDetails;
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
        priceOfPlayers = new int[allPlayerDetails.allPlayersDetails.Count];
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
        isSelected[lastSelectedPlayerIndex].SetActive(true);
        NotSelected[lastSelectedPlayerIndex].SetActive(false);
    }
    void initializePrices()
    {
        for (int i = 0; i < allPlayerDetails.allPlayersDetails.Count; i++)
        {
            priceOfPlayers[i] = allPlayerDetails.allPlayersDetails[i].playerPrice;
        }
    }
    private void initializePlayerPriceUI()
    {
        for (int i = 0; i < allPlayerDetails.allPlayersDetails.Count; i++)
        {
            playerPriceText[i].text = priceOfPlayers[i].ToString();
        }
    }
    private void initializePlayerEnableOrDisableUI()
    {
        for(int i = 0; i < allPlayerDetails.allPlayersDetails.Count; i++)
        {
            if( allPlayerDetails.allPlayersDetails[i].isPlayerBuyed)
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
        for (int i = 0; i < allPlayerDetails.allPlayersDetails.Count; i++)
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
    private void onClickPlayer0()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (allPlayerDetails.allPlayersDetails[0].isPlayerBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(0); // Set the selected player index
            isSelected[0].SetActive(true);
            NotSelected[0].SetActive(false);
        }
        else
        {
            Debug.Log("Player not buyed yet!");
        }
    }
    private void onClickPlayer1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (allPlayerDetails.allPlayersDetails[1].isPlayerBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(1); // Set the selected player index
            isSelected[1].SetActive(true);
            NotSelected[1].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins()>= allPlayerDetails.allPlayersDetails[1].playerPrice)
            {
                areYouSureSection[1].SetActive(true);

                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[1].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[1].playerPrice);
            allPlayerDetails.allPlayersDetails[1].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[1].playerName);
            onClickPlayer1(); // Refresh selection
            GameDataManager.SetSelectedPlayer(1); // Set the selected player index
            areYouSureSection[1].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
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
        if (allPlayerDetails.allPlayersDetails[2].isPlayerBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(2); // Set the selected player index
            isSelected[2].SetActive(true);
            NotSelected[2].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[2].playerPrice)
            {
                areYouSureSection[2].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton2()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[2].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[2].playerPrice);
            allPlayerDetails.allPlayersDetails[2].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(2); // Set the selected player index
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[2].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[2].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
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
        if (allPlayerDetails.allPlayersDetails[3].isPlayerBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(3); // Set the selected player index
            isSelected[3].SetActive(true);
            NotSelected[3].SetActive(false);
        }
        else
        {
           if( GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[3].playerPrice)
            {
                areYouSureSection[3].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton3()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[3].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[3].playerPrice);
            allPlayerDetails.allPlayersDetails[3].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(3); // Set the selected player index
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[3].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[3].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
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

        if (allPlayerDetails.allPlayersDetails[4].isPlayerBuyed)
        {
            DisableSelection();
            isSelected[4].SetActive(true);
            GameDataManager.SetSelectedPlayer(4); // Set the selected player index
            NotSelected[4].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[4].playerPrice)
            {
                areYouSureSection[4].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton4()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[4].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[4].playerPrice);
            allPlayerDetails.allPlayersDetails[4].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(4); // Set the selected player index
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[4].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[4].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
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

        if (allPlayerDetails.allPlayersDetails[5].isPlayerBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedPlayer(5); // Set the selected player index
            isSelected[5].SetActive(true);
            NotSelected[5].SetActive(false);
        }
        else
        {
            if(GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[5].playerPrice)
            {
                areYouSureSection[5].SetActive(true);
                areYouSureSectionMain.SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton5()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[5].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[5].playerPrice);
            allPlayerDetails.allPlayersDetails[5].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.SetSelectedPlayer(5); // Set the selected player index
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[5].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[5].SetActive(false);
            areYouSureSectionMain.SetActive(false);
            UIButtonSoundManager.instance.purchaseAudioPlay();
        }
        else
        {
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
