using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLobbyController : MenuPanelControl
{
    public ImageColorChanger mainImageColorChange;
    public PlayerInfoRow[] playerInfoRows;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateRoomInfo(PlayerInfo[] pInfo)
    {
        for (int i = 0; i < playerInfoRows.Length; i++)
        {
            if (pInfo.Length - 1 >= i)
            {
                Color col = ColorCodeConverter.ColorFromCode(pInfo[i].colorCode);
                if (i == GameInfoHolder.gihInstance.mainPlayerIndex)
                {
                    mainImageColorChange.SetColor(col);
                }
                playerInfoRows[i].gameObject.SetActive(true);
                playerInfoRows[i].playerName.text = pInfo[i].playerName;
                playerInfoRows[i].colorChanger.SetColor(col);
            }
            else
                playerInfoRows[i].gameObject.SetActive(false);
        }
    }
}
