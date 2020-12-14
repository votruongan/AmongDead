using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreLoader : MonoBehaviour
{
    public string modeName;
    public Text killDisplayer;
    public Text timeDisplayer;
    // Start is called before the first frame update
    void Awake()
    {
        int k = PlayerPrefs.GetInt(modeName + "_Kill",0);
        int t = PlayerPrefs.GetInt(modeName + "_Time",281020);
        if (t == 281020) return;
        killDisplayer.text = "Kill:" + k.ToString();
        timeDisplayer.text = "Seconds:" + t.ToString();
    }
}
