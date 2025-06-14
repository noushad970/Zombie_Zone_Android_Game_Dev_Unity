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
        UIButtonSoundManager.instance.buttonAudioPlay();

        // Add logic to start the game or load the game scene
    }
    private void onClickShopButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        homeButtonsSection.SetActive(false);
        ShopSection.SetActive(true);
        // Add logic to open the shop UI
    }

    private void onClickOptionButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        //homeButtonsSection.SetActive(false);
        // Add logic to open the options menu
    }
    private void onClickProfileButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        //homeButtonsSection.SetActive(false);
        // Add logic to open the profile UI
    }

    private void onClickAboutButton()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();
        //homeButtonsSection.SetActive(false);
        // Add logic to open the about UI
    }

    private void onClickExitButton() {
    
        // Add logic to exit the game or close the application
        Application.Quit();
    }

}
