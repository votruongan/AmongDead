using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyValueArray
{
    public List<KeyValuePair<string, string>> data;
    public KeyValueArray() { data = new List<KeyValuePair<string, string>>(); }
    public void AddPair(string key, string value)
    {
        data.Add(new KeyValuePair<string, string>(key, value));
    }
    public KeyValuePair<string, string>[] ToArray() { return data.ToArray(); }
}
public class MainMenuController : MonoBehaviour
{
    public ServerCommunication serverCom;
    public Text playerNameInput;
    public Text roomIdInput;
    public bool SetName()
    {
        if (playerNameInput.text.Length < 1)
        {
            NotiPanelControl.notiPanelInstance.DisplayNotification("Mời bạn nhập tên người chơi để tiếp tục");
            return false;
        }
        if (GameInfoHolder.gihInstance.mainPlayerInfo != null) return true;
        string name = playerNameInput.text;
        KeyValueArray kva = new KeyValueArray();
        kva.AddPair("name", name);
        serverCom.SendRequest("SET_NAME", kva.ToArray());
        return true;
    }
    public void FindRoom()
    {
        bool r = SetName();
        if (!r) return;
        PanelChangeController.pccInstance.NextPanel();
    }

    public void JoinRoom()
    {
        bool r = SetName();
        if (!r) return;
        StartCoroutine("ExecJoinRoom");
    }
    public void CreateRoom()
    {
        bool r = SetName();
        if (!r) return;
        NotiPanelControl.notiPanelInstance.DisplayNotification("Đang xử lý");
        PanelChangeController.pccInstance.GotoPanel(2);
        StartCoroutine("ExecCreateRoom");
    }
    IEnumerator ExecCreateRoom()
    {
        while (GameInfoHolder.gihInstance.mainPlayerInfo == null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        serverCom.SendRequest("NEW_ROOM");
    }
    IEnumerator ExecJoinRoom()
    {
        while (GameInfoHolder.gihInstance.mainPlayerInfo == null)
        {
            yield return new WaitForSeconds(0.1f);
        }
        KeyValueArray kva = new KeyValueArray();
        kva.AddPair("room_id", roomIdInput.text);
        serverCom.SendRequest("JOIN_ROOM", kva.ToArray());
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
