using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDisplayController : MonoBehaviour
{
    public int cooldownTime;
    public Text cooldownTimeDisplay;

    void Start()
    {
        Transform t = this.transform.GetChild(0);
        if (t != null) cooldownTimeDisplay = t.gameObject.GetComponent<Text>();
        cooldownTimeDisplay.gameObject.SetActive(false);
    }

    public void SetCooldownTime(int seconds)
    {
        gameObject.GetComponent<Button>().interactable = false;
        cooldownTime = seconds;
        cooldownTimeDisplay.text = cooldownTime.ToString();
        cooldownTimeDisplay.gameObject.SetActive(true);
        StartCoroutine("ExecDisplayCooldown");
    }
}
