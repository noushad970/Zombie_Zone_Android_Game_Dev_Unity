using System.Collections;
using TMPro;
using UnityEngine;

public class DisableNotifier : MonoBehaviour
{
    public static DisableNotifier Instance; // Singleton instance
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    TextMeshProUGUI showText;
    private void Start()
    {
        Instance= this; // Assign the singleton instance
        showText = GetComponent<TextMeshProUGUI>();
    }
    
    private void OnEnable()
    {
        StartCoroutine(waitFor2Seconds()); // Start the coroutine to wait for 2 seconds
    }
    IEnumerator waitFor2Seconds()
    {
        yield return new WaitForSeconds(3f); // Wait for 2 seconds
        gameObject.SetActive(false); // Disable the GameObject after 2 seconds
    }
    public void showTextNotifier(string s)
    {
        showText.text = s; // Set the text of the TextMeshProUGUI component
    }
}
