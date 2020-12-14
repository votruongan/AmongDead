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
    public static bool isPause;
    
    delegate void SetUpMode();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        mainPlayer = GameObject.Find("MainPlayer").GetComponent<MainPlayerController>();
        if (mainPlayer.info == null) mainPlayer.info = new PlayerInfo();
        mainPlayer.SetPlayerInfo(mainPlayer.info);
        isMultiplayer = ImmortalInfoHolder.GetBool("isMultiplayer");
        gameMode = ImmortalInfoHolder.GetInt("gameMode",-2810);
        switch (gameMode)
        {
            case 1: StartCoroutine(ExecGameInit(new SetUpMode(SetupLoneWolf))); break;
            case 2: StartCoroutine(ExecGameInit(new SetUpMode(SetupCaptureShip))); break;
            default: Debug.Log("Game Mode Undefined"); break;
        }
        isPause = true;
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
            pi.isImpostor = false;
            pi.playerName = ((int) (Random.Range(0.0f,23.5f) * 100000)).ToString();
            pis.Add(pi);
        }
        return pis.ToArray();
    }

    void SetupLoneWolf(){
        GameStatistics.instance.killCount = 0;
        if (isMultiplayer){

        }
        PlayerInfo[] pis = ExecCreateAround(0.5f, -1.5f, 10);
        mainPlayer.info.isImpostor = true;
        mainPlayer.SetPlayerInfo(mainPlayer.info);
        SetupPlayers(pis);
        TaskDisplayController.instance.AddNormalText(StringUtils.ExecTemplate(StringTemplate.LoneWolf_Task,"0"), Color.white);
    }
    public void HandleKill(PlayerController pc){
        if (isMultiplayer){

        }
        GameStatistics.instance.killCount ++;
        if (gameMode == 1){
            TaskDisplayController.instance.RemoveText(0);
            TaskDisplayController.instance.AddNormalText(StringUtils.ExecTemplate(StringTemplate.LoneWolf_Task,GameStatistics.instance.killCount.ToString()), Color.white);
            if (GameStatistics.instance.killCount == 5){
                isPause = true;
                FinishController.instance.DisplayFinish(true,GameStatistics.instance.killCount, GameStatistics.instance.elapsedSeconds);
            }
            return;
        }

    }
    public void LoneWolfAlarmed(){
        isPause = true;
        FinishController.instance.DisplayFinish(false,GameStatistics.instance.killCount, GameStatistics.instance.elapsedSeconds);
    }
    public void LoneWolfDetected(){
        TaskDisplayController.instance.RemoveText(0);
        TaskDisplayController.instance.AddNormalText(StringUtils.ExecTemplate(StringTemplate.LoneWolf_Alarm), Color.white);
    }
    IEnumerator ExecGameInit(SetUpMode callback){
        yield return new WaitForSeconds(1.0f);
        Debug.Log("SetupLoneWolf");
        Debug.Log(callback);
        callback();
    }

    void SetupCaptureShip(){
        if (isMultiplayer){

        }
        PlayerInfo[] humanPis = ExecCreateAround(0.5f, -1.5f, 5);
        PlayerInfo[] imposPis = ExecCreateAround(0.5f, -28.0f, 5, 3.0f, 1.5f);
        mainPlayer.info.isImpostor = false;
        mainPlayer.SetPlayerInfo(mainPlayer.info);
        foreach(PlayerInfo pi in imposPis){
            pi.isImpostor = true;
        }
        SetupPlayers(humanPis);
        SetupPlayers(imposPis);
        TaskDisplayController.instance.AddNormalText(StringTemplate.CTS_Task0, Color.white);
        TaskDisplayController.instance.AddNormalText(StringTemplate.CTS_Task1, Color.white);
    }

    public void CaptureArea(string area, bool isHumanCapture){
        if (isHumanCapture){
            if (TaskDisplayController.instance.spawnedText.Count == 1){
                //Victory
                isPause = true;
                FinishController.instance.DisplayFinish(true,GameStatistics.instance.killCount, GameStatistics.instance.elapsedSeconds);
                return;
            }
            TaskDisplayController.instance.RemoveText(0);
            TaskDisplayController.instance.RemoveText(0);
            if (area == "Navigation")
                TaskDisplayController.instance.AddNormalText(StringTemplate.CTS_Task1, Color.white);
            else
                TaskDisplayController.instance.AddNormalText(StringTemplate.CTS_Task0, Color.white);
            return;
        }
            if (TaskDisplayController.instance.spawnedText.Count == 1){
                //Lost
                isPause = true;
                FinishController.instance.DisplayFinish(false,GameStatistics.instance.killCount, GameStatistics.instance.elapsedSeconds);
                return;
            }
            TaskDisplayController.instance.RemoveText(0);
            TaskDisplayController.instance.RemoveText(0);
            if (area == "Navigation"){
                TaskDisplayController.instance.AddNormalText(StringTemplate.CTS_Task0, Color.white);
            }
            else{
                TaskDisplayController.instance.AddNormalText(StringTemplate.CTS_Task1, Color.white);
            }
            return;
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
            // if (CheckMainPlayer(pi.playerId)){
            //     mainPlayer.SetPlayerInfo(pi);
            //     continue;
            // }
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
        mainPlayer.SetPlayerName(ImmortalInfoHolder.GetString("PlayerName"));
        DelayStarter.instance.StartCounting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
