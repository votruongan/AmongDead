
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

public class ActionRouterWebGL : MonoBehaviour
{
    #region SETUP_AREA

    [DllImport("__Internal")]
    private static extern string GetCacheData1(string hashString, string key);
    [DllImport("__Internal")]
    private static extern string GetCacheData2(string hashString, string key0, string key1);
    [DllImport("__Internal")]
    private static extern string GetCacheData3(string hashString, string key0, string key1, string key2);
    [DllImport("__Internal")]
    private static extern string GetCacheData4(string hashString, string key0, string key1, string key2, string key3);
    [DllImport("__Internal")]
    private static extern string ParseObjectValue(string str);
    [DllImport("__Internal")]
    private static extern void ReleaseCache(string str);
    private Dictionary<string, Action<string>> funcDict;

    public void HandleAction(string rawData)
    {
        // Dictionary<string,string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(data);
        string h = ParseObjectValue(rawData);
        string cmd = GetCacheData1(h, "msg");
        // string data = GetCacheData1(h, "data");
        funcDict[cmd](h);
    }
    #endregion
    void Start()
    {
        funcDict = new Dictionary<string, Action<string>>(){
            { "UPDATE_PLAYER", UpdatePlayerHandle },
            { "NEW_ROOM", UpdateRoomHandle },
            { "UPDATE_ROOM", UpdateRoomHandle },
            { "JOIN_ROOM", JoinRoomHandle },
        };
    }
    public GameInfoHolder gameInfoHolder;
    public GameLobbyController gameLobbyController;
    public void UpdateRoomHandle(string h)
    {
        // string h = ParseObjectValue(data);
        List<PlayerInfo> pInfos = new List<PlayerInfo>();
        // GameObject.Find("RoomcodeDisplay").GetComponent<Text>.text = data["id"].ToString();
        Debug.Log("Handle UpdateRoom, h:" + h);
        string rawData = GetCacheData1(h, "data");
        Debug.Log("Handle UpdateRoom, pCount raw:" + rawData);
        int pCount = int.Parse(GetCacheData2(h, "data", "memberCount"));
        Debug.Log("Handle UpdateRoom, pCount:" + pCount.ToString());
        //extract player info
        for (int i = 0; i < pCount; i++)
        {
            Debug.Log("Handle UpdateRoom, i:" + i.ToString());
            pInfos.Add(new PlayerInfo());
            pInfos[i].index = i;
            pInfos[i].playerId = GetCacheData4(h, "data", "players", i.ToString(), "id");
            Debug.Log("Handle UpdateRoom, playerId:" + pInfos[i].playerId);
            pInfos[i].playerName = GetCacheData4(h, "data", "players", i.ToString(), "name");
            Debug.Log("Handle UpdateRoom, playerName:" + pInfos[i].playerName);
            // pInfos[i].playerId = data["players"][i.ToString()]["id"].ToString();
            // pInfos[i].playerName = data["players"][i.ToString()]["name"].ToString();
        }
        //update info and display
        Debug.Log(pInfos.ToArray().Length);
        gameInfoHolder.SetPlayerInfo(pInfos);
        gameLobbyController.UpdateRoomInfo(pInfos.ToArray());
        PanelChangeController.pccInstance.GotoPanel((int)MainMenuPanel.GameLobby);
        NotiPanelControl.notiPanelInstance.CloseNotification();
    }
    public void JoinRoomHandle(string h)
    {
        Debug.Log("Handle JoinRoomHandle, h:" + h);
        if (h == null) return;
        string dat = GetCacheData1(h, "data");
        if (dat == "true")
        {
            NotiPanelControl.notiPanelInstance.DisplayNotification("Đang xử lý");
            PanelChangeController.pccInstance.GotoPanel((int)MainMenuPanel.GameLobby);
        }

    }
    IEnumerator ExecUpdatePlayer()
    {
        yield return new WaitForSeconds(0.2f);
    }
    public void UpdatePlayerHandle(string h)
    {
        if (h == null) return;
        Debug.Log("Handle UpdatePlayer, h:" + h);
        // string h = ParseObjectValue(data);
        if (gameInfoHolder.mainPlayerInfo == null) gameInfoHolder.mainPlayerInfo = new PlayerInfo();
        gameInfoHolder.mainPlayerInfo.playerId = GetCacheData2(h, "data", "id");
        Debug.Log("Handle UpdatePlayer, id:" + gameInfoHolder.mainPlayerInfo.playerId);
        gameInfoHolder.mainPlayerInfo.playerName = GetCacheData2(h, "data", "name");
        Debug.Log("Handle UpdatePlayer, name:" + gameInfoHolder.mainPlayerInfo.playerName);
        gameInfoHolder.mainPlayerInfo.colorCode = int.Parse(GetCacheData2(h, "data", "color"));
        Debug.Log("Handle UpdatePlayer, color:" + gameInfoHolder.mainPlayerInfo.colorCode);
        // gameInfoHolder.mainPlayerInfo.playerId = data["id"].ToString();
        // gameInfoHolder.mainPlayerInfo.playerName = data["name"].ToString();
        // gameInfoHolder.mainPlayerInfo.colorCode = int.Parse(data["color"].ToString());
        gameInfoHolder.UpdatePlayerInfoDisplay();
        StartCoroutine("ExecUpdatePlayer");
    }
}