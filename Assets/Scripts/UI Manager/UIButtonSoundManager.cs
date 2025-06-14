using UnityEngine;
using UnityEngine.UI;

public class UIButtonSoundManager : MonoBehaviour
{
    public static UIButtonSoundManager instance;
    private void Start()
    {
        instance = this;
    }
    [SerializeField]
    private AudioSource buttonClickAudio;
    [SerializeField] private AudioSource purchaseAudio;

    public void buttonAudioPlay()
    {
        if (!buttonClickAudio.isPlaying) {
            buttonClickAudio.Play();
        }
    }
    public void purchaseAudioPlay()
    {
  
            purchaseAudio.Play();
        
    }
    

    
}
