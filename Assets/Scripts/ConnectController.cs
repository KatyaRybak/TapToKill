using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ConnectController: MonoBehaviour
{
    public GameObject connectPanel;
    public GameObject mainMenuPanel;
    public string url = "https://www.google.com/";
    IEnumerator Start()
    {      
        WWW www = new WWW(url);
        mainMenuPanel.SetActive(false);
        Text connectingText = connectPanel.GetComponentInChildren<Text>();
        connectingText.text = "Connecting to " + url + "...";
        yield return www;
        if (www.error != null)
        {
            Debug.LogError(www.error);
            connectingText.text = "Unable to connect to server:"+ url +"\n"+ (string)www.error;
        }
        else
        {
            connectingText.text = "Connected to server:" + url;
            mainMenuPanel.SetActive(true);
        }
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(1);
    }
}
