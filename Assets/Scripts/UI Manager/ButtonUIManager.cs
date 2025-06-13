using UnityEngine;
using UnityEngine.UI;

public class ButtonUIManager : MonoBehaviour
{
    [SerializeField] private Button playButton,shopButton,optionButton,profileButton,aboutButton,exitButton;
    [SerializeField] private GameObject homeButtonsSection,ShopSection;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playButton.onClick.AddListener(onClickPlayButton);
        shopButton.onClick.AddListener(onClickShopButton);
        optionButton.onClick.AddListener(onClickOptionButton);
        profileButton.onClick.AddListener(onClickProfileButton);
        aboutButton.onClick.AddListener(onClickAboutButton);
        exitButton.onClick.AddListener(onClickExitButton);

    }

    
    private void onClickPlayButton()
    {
        
        Debug.Log("Play button clicked");

        // Add logic to start the game or load the game scene
    }
    private void onClickShopButton()
    {   
        homeButtonsSection.SetActive(false);
        ShopSection.SetActive(true);
        Debug.Log("Shop button clicked");
        // Add logic to open the shop UI
    }

    private void onClickOptionButton()
    {
        homeButtonsSection.SetActive(false);
        Debug.Log("Option button clicked");
        // Add logic to open the options menu
    }
    private void onClickProfileButton()
    {
        homeButtonsSection.SetActive(false);
        Debug.Log("Profile button clicked");
        // Add logic to open the profile UI
    }

    private void onClickAboutButton()
    {
        homeButtonsSection.SetActive(false);
        Debug.Log("About button clicked");
        // Add logic to open the about UI
    }

    private void onClickExitButton() {
    
        Debug.Log("Exit button clicked");
        // Add logic to exit the game or close the application
        Application.Quit();
    }

}
