using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public List<Button> buttons;
    private static Dictionary<string, Button> buttonsDict;
    private static Dictionary<string, int> buttonCooldownDict;
    private static Dictionary<string, Text> buttonCooldownDisplayDict;
    public static UIController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        if (buttons == null || buttons.Count == 0)
        {
            Transform migo = GameObject.Find("TouchInputs").GetComponent<Transform>();
            for (int i = 0; i < migo.childCount; i++)
            {
                Button b = migo.GetChild(i).gameObject.GetComponent<Button>();
                if (b != null)
                    buttons.Add(b);
            }
        }
        buttonsDict = new Dictionary<string, Button>();
        buttonCooldownDict = new Dictionary<string, int>();
        buttonCooldownDisplayDict = new Dictionary<string, Text>();
        foreach (Button b in buttons)
        {
            string sname = b.gameObject.name.Split('_')[1].ToUpper();
            buttonsDict.Add(sname, b);
            buttonCooldownDict.Add(sname, 0);
            buttonCooldownDisplayDict.Add(sname, b.transform.GetChild(0).gameObject.GetComponent<Text>());
            buttonCooldownDisplayDict[sname].gameObject.SetActive(false);
        }
        SetAllButtonsActive(false);
    }
    public void ExecKill()
    {
        if (!buttonsDict["KILL"].gameObject.activeInHierarchy || !buttonsDict["KILL"].interactable) return;
        buttonsDict["KILL"].interactable = false;
        buttonCooldownDict["KILL"] = 10;
        buttonCooldownDisplayDict["KILL"].text = buttonCooldownDict["KILL"].ToString();
        buttonCooldownDisplayDict["KILL"].gameObject.SetActive(true);
        KillActionController.KillFirstPlayer();
        StartCoroutine("ExecKillCoolDown");
    }
    IEnumerator ExecKillCoolDown()
    {
        while (buttonCooldownDict["KILL"] > 0)
        {
            yield return new WaitForSeconds(1.0f);
            buttonCooldownDict["KILL"]--;
            buttonCooldownDisplayDict["KILL"].text = buttonCooldownDict["KILL"].ToString();
        }
        buttonCooldownDict["KILL"] = 0;
        buttonCooldownDisplayDict["KILL"].gameObject.SetActive(false);
        buttonsDict["KILL"].interactable = true;
    }
    public static void SetAllButtonsActive(bool isActive)
    {
        foreach (KeyValuePair<string, Button> b in buttonsDict)
        {
            b.Value.gameObject.SetActive(isActive);
        }

    }
    public static void SetButtonActive(string buttonsName, UsableObjectInfo uoi, bool isActive = true)
    {
        string us = buttonsName.ToUpper();
        if (!buttonsDict.ContainsKey(us)) return;
        Button b = buttonsDict[us];
        if (b == null) return;
        b.gameObject.SetActive(isActive);
        b.onClick.RemoveAllListeners();
        if (isActive && uoi != null)
            b.onClick.AddListener(uoi.ExecUse);
    }
}
