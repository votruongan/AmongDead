using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MainPlayerController mainPlayer;
    public List<PlayerController> playerControllers;
    public GameObject prefabRealPlayer;
    public GameObject prefabBotPlayer;
    public bool isMultiplayer = false;
    public int gameMode = 1;
    public static GameController instance;
    
    //LoneWolf mode - kill count
    public int LW_KillCount;

    public int CTS_ZoneCount;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        mainPlayer = GameObject.Find("MainPlayer").GetComponent<MainPlayerController>();
        isMultiplayer = ImmortalInfoHolder.GetBool("isMultiplayer");
        // gameMode = ImmortalInfoHolder.GetInt("gameMode",-2810);
        switch (gameMode)
        {
            case 1: SetupLoneWolf(); break;
            case 2: SetupCaptureShip(); break;
            default: Debug.Log("Game Mode Undefined"); break;
        }
        Debug.Log("GameController initialized");
    }

    public PlayerInfo[] ExecCreateAround(float rootX, float rootY, int numberOfPlayer, float radiusX = 4f, float radiusY = 2f){
        List<PlayerInfo> pis = new List<PlayerInfo>();
        int n = numberOfPlayer;
        float dd = 360/n;
        float deg = 0.0f;
        for (int i = 0; i < n; i++){
            PlayerInfo pi = new PlayerInfo();
            deg = i * dd;
            pi.positionX = rootX + radiusX * Mathf.Cos(deg);
            pi.positionY =  rootY + radiusY * Mathf.Sin(deg);
            pi.isBot = true;
            pis.Add(pi);
        }
        return pis.ToArray();
    }

    public void HandleKill(PlayerController pc){
        if (isMultiplayer){

        }
        if (gameMode == 1){
            LW_KillCount ++;
            TaskDisplayController.instance.RemoveText(0);
            TaskDisplayController.instance.AddNormalText(StringUtils.ExecTemplate(StringTemplate.LoneWolf_Task,LW_KillCount.ToString()), Color.white);
        }
    }

    public void LoneWolfDetected(){
        TaskDisplayController.instance.RemoveText(0);
        TaskDisplayController.instance.AddNormalText(StringUtils.ExecTemplate(StringTemplate.LoneWolf_Alarm), Color.white);
    }

    public void SetupLoneWolf(){
        LW_KillCount = 0;
        if (isMultiplayer){

        }
        PlayerInfo[] pis = ExecCreateAround(0.5f, -1.5f, 10);
        SetupPlayers(pis);
        TaskDisplayController.instance.AddNormalText(StringUtils.ExecTemplate(StringTemplate.LoneWolf_Task,"0"), Color.white);
    }
    public void SetupCaptureShip(){
        if (isMultiplayer){

        }
        PlayerInfo[] humanPis = ExecCreateAround(0.5f, -1.5f, 5);
        PlayerInfo[] imposPis = ExecCreateAround(0.5f, -28.0f, 5, 3.0f, 1.5f);
        foreach(PlayerInfo pi in imposPis){
            pi.isImpostor = true;
        }
        SetupPlayers(humanPis);
        SetupPlayers(imposPis);
        TaskDisplayController.instance.AddNormalText("Let Capture the ship. (0/10)", Color.white);
    }

    public bool CheckMainPlayer(string playerId){
        if (mainPlayer != null && playerId != null &&
         mainPlayer.info != null &&playerId == mainPlayer.info.playerId)
            return true;
        return false;
    }

    public void SetupPlayers(PlayerInfo[] pInfo){
        GameObject go;
        foreach (PlayerInfo pi in pInfo)
        {
            if (CheckMainPlayer(pi.playerId)){
                mainPlayer.SetPlayerInfo(pi);
                continue;
            }
            if (pi.isBot){
                go = Instantiate(prefabBotPlayer);
                playerControllers.Add(go.GetComponent<EnemyAI>());
            }
            else{
                go = Instantiate(prefabRealPlayer);
                playerControllers.Add(go.GetComponent<PlayerController>());
            }
            playerControllers[playerControllers.Count - 1].SetPlayerInfo(pi);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
