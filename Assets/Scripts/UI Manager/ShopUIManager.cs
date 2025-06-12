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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameDataManager.AddCoins(60000);
        priceOfPlayers= new int[allPlayerDetails.allPlayersDetails.Count];
        initializePrices(); 
        initializePlayerPriceUI();
        initializePlayerEnableOrDisableUI();
        disableYesNoSections();
        buyPlayersButton[0].onClick.AddListener(onClickPlayer0);
        buyPlayersButton[1].onClick.AddListener(onClickPlayer1);
        buyPlayersButton[2].onClick.AddListener(onClickPlayer2);
        buyPlayersButton[3].onClick.AddListener(onClickPlayer3);
        buyPlayersButton[4].onClick.AddListener(onClickPlayer4);
        buyPlayersButton[5].onClick.AddListener(onClickPlayer5);

        yesButtons[1].GetComponents<Button>()[1].onClick.AddListener(onClickYesButton1);
        yesButtons[2].GetComponents<Button>()[2].onClick.AddListener(onClickYesButton2);
        yesButtons[3].GetComponents<Button>()[3].onClick.AddListener(onClickYesButton3);
        yesButtons[4].GetComponents<Button>()[4].onClick.AddListener(onClickYesButton4);
        yesButtons[5].GetComponents<Button>()[5].onClick.AddListener(onClickYesButton5);

        NoButtons[1].GetComponents<Button>()[1].onClick.AddListener(onClickNoButton1);
        NoButtons[2].GetComponents<Button>()[2].onClick.AddListener(onClickNoButton2);
        NoButtons[3].GetComponents<Button>()[3].onClick.AddListener(onClickNoButton3);
        NoButtons[4].GetComponents<Button>()[4].onClick.AddListener(onClickNoButton4);
        NoButtons[5].GetComponents<Button>()[5].onClick.AddListener(onClickNoButton5);


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
        }
    }
    private void onClickPlayer0()
    {
        if(allPlayerDetails.allPlayersDetails[0].isPlayerBuyed)
        {
            DisableSelection();
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
        if(allPlayerDetails.allPlayersDetails[1].isPlayerBuyed)
        {
            DisableSelection();
            isSelected[1].SetActive(true);
            NotSelected[1].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins()>= allPlayerDetails.allPlayersDetails[1].playerPrice)
            {
                areYouSureSection[1].SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton1()
    {
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[1].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[1].playerPrice);
            allPlayerDetails.allPlayersDetails[1].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[1].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[1].SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough Coins!");
        }
    }
    private void onClickNoButton1()
    {
        areYouSureSection[1].SetActive(false);
    }
    private void onClickPlayer2()
    {
        if(allPlayerDetails.allPlayersDetails[2].isPlayerBuyed)
        {
            DisableSelection();
            isSelected[2].SetActive(true);
            NotSelected[2].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[2].playerPrice)
            {
                areYouSureSection[2].SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton2()
    {
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[2].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[2].playerPrice);
            allPlayerDetails.allPlayersDetails[2].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[2].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[2].SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough Coins!");
        }
    }
    private void onClickNoButton2()
    {
        areYouSureSection[2].SetActive(false);
    }
    private void onClickPlayer3()
    {
        if(allPlayerDetails.allPlayersDetails[3].isPlayerBuyed)
        {
            DisableSelection();
            isSelected[3].SetActive(true);
            NotSelected[3].SetActive(false);
        }
        else
        {
           if( GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[3].playerPrice)
            {
                areYouSureSection[3].SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton3()
    {
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[3].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[3].playerPrice);
            allPlayerDetails.allPlayersDetails[3].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[3].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[3].SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough Coins!");
        }
    }
    private void onClickNoButton3()
    {
        areYouSureSection[3].SetActive(false);
    }



    private void onClickPlayer4()
    {
        if(allPlayerDetails.allPlayersDetails[4].isPlayerBuyed)
        {
            DisableSelection();
            isSelected[4].SetActive(true);
            NotSelected[4].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[4].playerPrice)
            {
                areYouSureSection[4].SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton4()
    {
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[4].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[4].playerPrice);
            allPlayerDetails.allPlayersDetails[4].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[4].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[4].SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough Coins!");
        }
    }
    private void onClickNoButton4()
    {
        areYouSureSection[4].SetActive(false);
    }
    private void onClickPlayer5()
    {
        if(allPlayerDetails.allPlayersDetails[5].isPlayerBuyed)
        {
            DisableSelection();
            isSelected[5].SetActive(true);
            NotSelected[5].SetActive(false);
        }
        else
        {
            if(GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[5].playerPrice)
            {
                areYouSureSection[5].SetActive(true);
            }
            else
            {
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton5()
    {
        if (GameDataManager.GetCoins() >= allPlayerDetails.allPlayersDetails[5].playerPrice)
        {
            GameDataManager.AddCoins(-allPlayerDetails.allPlayersDetails[5].playerPrice);
            allPlayerDetails.allPlayersDetails[5].isPlayerBuyed = true;
            initializePlayerEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allPlayerDetails.allPlayersDetails[5].playerName);
            onClickPlayer1(); // Refresh selection
            areYouSureSection[5].SetActive(false);
        }
        else
        {
            Debug.Log("Not Enough Coins!");
        }
    }
    private void onClickNoButton5()
    {
        areYouSureSection[5].SetActive(false);
    }

}
