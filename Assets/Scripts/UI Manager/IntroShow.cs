using System.Collections;
using UnityEngine;

public class IntroShow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject menuPanel;
    void Start()
    {
        StartCoroutine(wait5SecForIntro());
    }

    IEnumerator wait5SecForIntro()
    {
        menuPanel.SetActive(false);
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
        menuPanel.SetActive(true);
    }
}
