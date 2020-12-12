using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickController : MonoBehaviour
{
    public GameObject[] rows;
    public List<Image> buttons;
    public GameInfoHolder gameInfoHolder;
    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < rows.Length; j++)
        {
            for (int i = 0; i < 4; i++)
            {
                buttons.Add(rows[j].transform.GetChild(i).GetChild(0).gameObject.GetComponent<Image>());
                buttons[j * 4 + i].color = ColorCodeConverter.colorArray[j * 4 + i];
                buttons[j * 4 + i].gameObject.GetComponent<Button>().onClick.AddListener(() => PickColor(j * 4 + i));
            }
        }
    }

    public void PickColor(int colorCode)
    {
        if (gameInfoHolder.mainPlayerInfo == null) return;
        gameInfoHolder.mainPlayerInfo.colorCode = colorCode;

    }
}
