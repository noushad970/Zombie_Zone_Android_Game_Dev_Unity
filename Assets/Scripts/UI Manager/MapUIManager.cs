using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapUIManager : MonoBehaviour
{
    //[SerializeField] private MapObject allMapDetails;
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
        selectMapButton[0].onClick.AddListener(onClickMap0);
        selectMapButton[1].onClick.AddListener(onClickMap1);
        backButton.onClick.AddListener(onClickBackButton);
    }

    private IEnumerator initializeAllAfter1Sec()
    {
        yield return new WaitForSeconds(1f);
        priceOfMaps = new int[2]; // Fixed to 2 maps based on previous data
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
        if (lastSelectedMapIndex >= 0 && lastSelectedMapIndex < 2)
        {
            isSelected[lastSelectedMapIndex].SetActive(true);
            NotSelected[lastSelectedMapIndex].SetActive(false);
        }
    }

    void initializePrices()
    {
        // Hardcoded prices based on previous data (e.g., 0 for both Halloween and FantasyForest)
        priceOfMaps[0] = 0; // Halloween
        priceOfMaps[1] = 0; // FantasyForest
    }

    private void initializeMapPriceUI()
    {
        for (int i = 0; i < 2; i++)
        {
            MapPriceText[i].text = priceOfMaps[i].ToString();
        }
    }

    private void initializeMapEnableOrDisableUI()
    {
        for (int i = 0; i < 2; i++)
        {
            string mapName = GetMapNameByIndex(i);
            if (GameDataManager.IsMapUnlocked(mapName))
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
        for (int i = 0; i < 2; i++)
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

    private string GetMapNameByIndex(int index)
    {
        string[] mapNames = { "Halloween", "FantasyForest" };
        return index >= 0 && index < mapNames.Length ? mapNames[index] : "";
    }

    private void onClickMap0()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsMapUnlocked("Halloween"))
        {
            DisableSelection();
            GameDataManager.SetSelectedMapIndex(0);
            isSelected[0].SetActive(true);
            NotSelected[0].SetActive(false);
        }
        else
        {
            Debug.Log("Map not bought yet!");
        }
    }

    private void onClickMap1()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        if (GameDataManager.IsMapUnlocked("FantasyForest"))
        {
            DisableSelection();
            GameDataManager.SetSelectedMapIndex(1);
            isSelected[1].SetActive(true);
            NotSelected[1].SetActive(false);
        }
        else
        {
            Debug.Log("Map not bought yet!");
        }
    }

    private void onClickBackButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        homeSection.SetActive(true);
        shopSection.SetActive(false);
    }
}
