using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoRow : MonoBehaviour
{
    public ImageColorChanger colorChanger;
    public Text playerName;
    // Start is called before the first frame update
    void Start()
    {
        colorChanger = this.transform.GetChild(1).gameObject.GetComponent<ImageColorChanger>();
        if (playerName == null)
            playerName = this.transform.GetChild(2).gameObject.GetComponent<Text>();
        playerName.text = "";
    }
}
