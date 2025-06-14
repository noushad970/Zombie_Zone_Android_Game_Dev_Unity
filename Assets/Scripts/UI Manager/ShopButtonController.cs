using UnityEngine;
using UnityEngine.UI;

public class ShopButtonController : MonoBehaviour
{
    [SerializeField] private Button charButton,MapButton;
    [SerializeField] private GameObject charPanel, mapPanel;
    private void Start()
    {
        charButton.onClick.AddListener(OnCharButtonClicked);
        MapButton.onClick.AddListener(OnMapButtonClicked);
    }
    private void OnEnable()
    {
        charPanel.SetActive(true);
        mapPanel.SetActive(false);
    }
    void OnCharButtonClicked()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        charPanel.SetActive(true);
        mapPanel.SetActive(false);
    }
    void OnMapButtonClicked()
    {
        UIButtonSoundManager.instance.buttonAudioPlay();

        charPanel.SetActive(false);
        mapPanel.SetActive(true);
    }
}
