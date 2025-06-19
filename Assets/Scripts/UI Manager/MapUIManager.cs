using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    [SerializeField] private MapObject allMapDetails;
    [Header("UI Refs")]
    private int[] priceOfMaps;
    [SerializeField] private Button[] buyMapsButton;
    [SerializeField] private TextMeshProUGUI[] MapPriceText;

    [SerializeField] private GameObject[] isBuyedObject, NotBuyedObject;
    [SerializeField] private GameObject[] isSelected, NotSelected;

    [SerializeField] private GameObject[] areYouSureSection, yesButtons, NoButtons;
    [SerializeField] private GameObject areYouSureSectionMain;
    [SerializeField] private Button[] yesButtonsButton, NoButtonsButton;

    [SerializeField] private Button[] selectMapButton;


    [SerializeField] private GameObject homeSection, shopSection;
    [SerializeField] private Button backButton;

    private void Start()
    {
        GameDataManager.LoadGameData();


        StartCoroutine(initializeAllAfter1Sec());
        areYouSureSectionMain.SetActive(false);
        //buyMapsButton[2].onClick.AddListener(onClickMap2);


        selectMapButton[0].onClick.AddListener(onClickMap0);
        selectMapButton[1].onClick.AddListener(onClickMap1);

        //yesButtonsButton[2].onClick.AddListener(onClickYesButton2);

        //NoButtonsButton[2].onClick.AddListener(onClickNoButton2);

        backButton.onClick.AddListener(onClickBackButton);
    }

    private IEnumerator initializeAllAfter1Sec()
    {
        yield return new WaitForSeconds(1f);
        priceOfMaps = new int[allMapDetails.allMaps.Count];
        initializePrices();
        initializeMapPriceUI();
        initializeMapEnableOrDisableUI();
        disableYesNoSections();
        initializeLastSelectMap();
    }
    private void initializeLastSelectMap()
    {
        DisableSelection();
        int lastSelectedMapIndex = GameDataManager.GetSelectedMapIndex();
        isSelected[lastSelectedMapIndex].SetActive(true);
        NotSelected[lastSelectedMapIndex].SetActive(false);
    }
    void initializePrices()
    {
        for (int i = 0; i < allMapDetails.allMaps.Count; i++)
        {
            priceOfMaps[i] = allMapDetails.allMaps[i].mapPrice;
        }
    }
    private void initializeMapPriceUI()
    {
        for (int i = 0; i < allMapDetails.allMaps.Count; i++)
        {
            MapPriceText[i].text = priceOfMaps[i].ToString();
        }
    }
    private void initializeMapEnableOrDisableUI()
    {
        for (int i = 0; i < allMapDetails.allMaps.Count; i++)
        {
            if (GameDataManager.IsMapUnlocked(allMapDetails.allMaps[i].mapName.ToString()))
            {
                isBuyedObject[i].SetActive(true);
                NotBuyedObject[i].SetActive(false);
                buyMapsButton[i].interactable = false;
            }
            else
            {
                isBuyedObject[i].SetActive(false);
                NotBuyedObject[i].SetActive(true);
                buyMapsButton[i].interactable = true;
            }
        }
    }
    private void DisableSelection()
    {
        for (int i = 0; i < allMapDetails.allMaps.Count; i++)
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
    private void onClickMap0()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        if (allMapDetails.allMaps[0].isMapBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedMapIndex(0); // Set the selected player index
            isSelected[0].SetActive(true);
            NotSelected[0].SetActive(false);
        }
        else
        {
            Debug.Log("Player not buyed yet!");
        }
    }
    private void onClickMap1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        if (allMapDetails.allMaps[1].isMapBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedMapIndex(1); // Set the selected player index
            isSelected[1].SetActive(true);
            NotSelected[1].SetActive(false);
        }
        else
        {
            Debug.Log("Player not buyed yet!");
        }
    }
    /*
    private void onClickMap1()
    {
        if (allMapDetails.allMaps[1].isMapBuyed)
        {
            DisableSelection();
            GameDataManager.SetSelectedMapIndex(1); // Set the selected player index
            isSelected[1].SetActive(true);
            NotSelected[1].SetActive(false);
        }
        else
        {
            if (GameDataManager.GetCoins() >= allMapDetails.allMaps[1].mapPrice)
            {
                areYouSureSection[1].SetActive(true);

                areYouSureSectionMain.SetActive(true);
            }
            else
            {
    
            DisableNotifier.Instance.gameObject.SetActive(true);
            DisableNotifier.Instance.showTextNotifier("Not Enough Coins!");
                Debug.Log("Not Engough Coins!");
            }
        }
    }
    private void onClickYesButton1()
    {
        if (GameDataManager.GetCoins() >= allMapDetails.allMaps[1].mapPrice)
        {
            GameDataManager.AddCoins(-allMapDetails.allMaps[1].mapPrice);
            allMapDetails.allMaps[1].isMapBuyed = true;
            initializeMapEnableOrDisableUI();
            GameDataManager.IsPlayerUnlocked(allMapDetails.allMaps[1].mapName);
            onClickMap1(); // Refresh selection
            GameDataManager.SetSelectedPlayer(1); // Set the selected player index
            areYouSureSection[1].SetActive(false);
            areYouSureSectionMain.SetActive(false);
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
        areYouSureSection[1].SetActive(false);
        areYouSureSectionMain.SetActive(false);
    }
    */
    private void onClickBackButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        homeSection.SetActive(true);
        shopSection.SetActive(false);
    }
}
