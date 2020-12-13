using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PolyAndCode.UI;

public struct RoomListCellInfo{
    public string roomId;
    public string roomName;
    public string roomAmount;
}
public class RoomListScrollList : MenuPanelControl, IRecyclableScrollRectDataSource
{
    [SerializeField]
    RecyclableScrollRect _recyclableScrollRect;
    [SerializeField]
    private int _dataLength;
    public List<RoomListCellInfo> roomList = new List<RoomListCellInfo>();
    //Recyclable scroll rect's data source must be assigned in Awake.
    private void Awake()
    {
        InitData();
        _recyclableScrollRect.DataSource = this;
    }
    private void InitData(){
        RoomListCellInfo cellInfo;
        cellInfo.roomId = "1"; cellInfo.roomName = "ROOOOOOOOOOOOOOOOOO"; cellInfo.roomAmount = "10/10"; 
        for (int i = 0; i < 50; i++)
        {
            roomList.Add(cellInfo);            
        }
    }
    #region DATA-SOURCE
    /// <summary>
    /// Data source method. return the list length.
    /// </summary>
    public int GetItemCount()
    {
        return roomList.Count;
    }
    /// <summary>
    /// Called for a cell every time it is recycled
    /// Implement this method to do the necessary cell configuration.
    /// </summary>
    public void SetCell(ICell cell, int index)
    {
        //Casting to the implemented Cell
        var item = cell as RoomListCell;
        item.ConfigureCell(index,roomList[index]);
    }
    #endregion
}
