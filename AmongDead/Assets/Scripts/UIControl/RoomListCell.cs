using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;
public class RoomListCell : MonoBehaviour, ICell
{

    public Text roomIdText;
    public Text roomNameText;
    public Text roomAmountText;

    private int _cellIndex;
    public void ConfigureCell(int index, RoomListCellInfo data){
        roomIdText.text = data.roomId;
        roomNameText.text = data.roomName;
        roomAmountText.text = data.roomAmount;
    }
}
