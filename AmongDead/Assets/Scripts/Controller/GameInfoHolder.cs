using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoHolder : MonoBehaviour
{
    public static GameInfoHolder gihInstance;
    public Dictionary<string, int> dictionaryNameIndex;
    public string rawString;
    // public static Dictionary<string,int> siteNameToIndex
    public List<BaseController> subscribedController;
    public int mainPlayerIndex;
    public PlayerInfo mainPlayerInfo;
    public List<PlayerInfo> players;
    public GameLobbyController gameLobbyController;
    public GameController gameController;
    public void SetPlayerInfo(List<PlayerInfo> pInfos)
    {
        players = pInfos;
        UpdatePlayerInfoDisplay();
    }
    public void UpdatePlayerInfoDisplay()
    {
        if (gameLobbyController != null) gameLobbyController.UpdateRoomInfo(players.ToArray());
        if (gameController != null) Debug.Log("GameController called");
    }
    public void SendPlayerInfoSync()
    {
        KeyValueArray kva = new KeyValueArray();
        kva.AddPair("data", "{ color:" + mainPlayerInfo.colorCode.ToString() + "}");
        ServerCommunication.Instance.SendRequest("SET_PLAYER_INFO", kva.ToArray());
    }
    public void SetRawString(string str)
    {
        rawString = str;
        if (str.Length <= 0) return;
        dictionaryNameIndex = new Dictionary<string, int>();
        string[] lines = str.Split(';');
        foreach (var item in lines)
        {
            string[] dat = item.Split('=');
            dictionaryNameIndex.Add(dat[0], int.Parse(dat[1]));
            // GameObject go = GameObject.Find(dat[0] + "Room");
            // Debug.Log(go);
        }
        foreach (BaseController bc in subscribedController)
        {
            bc.RouterCallback();
        }
        return;
    }
    void Start()
    {
        if (gihInstance == null)
        {
            gihInstance = this;
            mainPlayerIndex = -1;
        }
        SetRawString(rawString);
        if (dictionaryNameIndex == null || dictionaryNameIndex.Count == 0)
        {
            // dictionaryNameIndex = new List<string>();dictionaryNameIndex.Add("UpperEngineRoom");dictionaryNameIndex.Add("MedBayRoom");dictionaryNameIndex.Add("CafeteriaRoom");dictionaryNameIndex.Add("WeaponsRoom");dictionaryNameIndex.Add("LifeSupportRoom");dictionaryNameIndex.Add("NavigationRoom");dictionaryNameIndex.Add("AdminRoom");dictionaryNameIndex.Add("ShieldsRoom");dictionaryNameIndex.Add("CommunicationsRoom");dictionaryNameIndex.Add("StorageRoom");dictionaryNameIndex.Add("ElectricalRoom");dictionaryNameIndex.Add("LowerEngineRoom");dictionaryNameIndex.Add("ReactorRoom");dictionaryNameIndex.Add("SecurityRoom");
        }
    }

    public int SiteNameToIndex(string sName)
    {
        if (sName.Length > 4) sName = sName.Substring(0, sName.Length - 4);
        if (dictionaryNameIndex != null && dictionaryNameIndex.ContainsKey(sName)) return dictionaryNameIndex[sName];
        return -1;
    }

}