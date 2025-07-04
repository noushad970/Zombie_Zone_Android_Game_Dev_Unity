using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroShow : MonoBehaviour
{
  
    void Start()
    {
        StartCoroutine(wait5SecForIntro());
    }

    IEnumerator wait5SecForIntro()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Gameplay"); // Load the main menu scene
    }
}
