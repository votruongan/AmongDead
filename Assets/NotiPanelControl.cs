using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotiPanelControl : MonoBehaviour
{
    public Text notiText;
    public static NotiPanelControl notiPanelInstance;
    void Start()
    {
        notiPanelInstance = this;
        CloseNotification();
    }

    public void DisplayNotification(string text)
    {
        notiText.text = text;
        this.gameObject.SetActive(true);
    }

    public void CloseNotification()
    {
        this.gameObject.SetActive(false);
    }
}
