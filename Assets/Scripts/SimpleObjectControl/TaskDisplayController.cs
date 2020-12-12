using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskDisplayController : MonoBehaviour
{
    public int textDirection;
    public float textOffset;
    public GameObject prefabNormalText;
    public GameObject prefabWarningText;
    public PlayerController pc;
    public Animator taskPanelAnimator;
    public int currentIndex = 0;
    public bool isActive = true;
    public List<Text> spawnedText;
    public static TaskDisplayController instance;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
        currentIndex = 0;
        instance = this;
    }
    public void RemoveText(int textIndex)
    {
        if (textIndex > spawnedText.Count - 1 || textIndex < 0) return;
        for (int i = textIndex + 1; i < spawnedText.Count; i++)
        {
            RectTransform rt = spawnedText[i].gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector3(0f, textDirection * textOffset * i, 0f);
        }
        spawnedText.Remove(spawnedText[textIndex]);
    }
    public void AddWarningText(string textContent, Color col)
    {
        AppendDisplayText(prefabWarningText, textContent, col);
    }
    public void AddNormalText(string textContent, Color col)
    {
        AppendDisplayText(prefabNormalText, textContent, col);
    }
    public void AppendDisplayText(GameObject styledObject, string displayText, Color textColor, int currentNumber = 0, int limitNumber = 1)
    {
        GameObject gO = Instantiate(styledObject, this.transform.position, Quaternion.identity, this.transform);
        spawnedText.Add(gO.GetComponent<Text>());
        // spawnedText[spawnedText.Count - 1].text = displayText + " (" + currentNumber.ToString() + "/" + limitNumber.ToString() + ")";
        spawnedText[spawnedText.Count - 1].text = displayText;
        spawnedText[spawnedText.Count - 1].color = textColor;
        RectTransform rt = gO.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector3(0f, textDirection * textOffset * ++currentIndex, 0f);
    }
    public GameObject buttonTogglePanel;
    public Text buttonTPtext;
    public void ToggleTaskPanel()
    {
        isActive = !isActive;
        RectTransform rt = buttonTogglePanel.GetComponent<RectTransform>();
        if (isActive)
        {
            taskPanelAnimator.Play("PanelOpen");
            rt.anchoredPosition = new Vector3(0, 0f, 0f);
            buttonTPtext.text = "X";
            foreach (Text t in spawnedText)
            {
                t.gameObject.SetActive(true);
            }
            return;
        }
        taskPanelAnimator.Play("PanelClose");
        rt.anchoredPosition = new Vector3(0, Screen.height / 5, 0f);
        foreach (Text t in spawnedText)
        {
            t.gameObject.SetActive(false);
        }

        buttonTPtext.text = "...";
    }
}
