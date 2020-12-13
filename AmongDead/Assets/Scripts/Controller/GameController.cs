using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MainPlayerController mainPlayer;
    public List<PlayerController> playerControllers;
    public GameObject prefabGamePlayer;
    public bool isMultiplayer = false;
    public int gameMode = 1;

    // Start is called before the first frame update
    void Start()
    {
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

    public void SetupLoneWolf(){
        if (isMultiplayer){

        }
        List<PlayerInfo> pis = new List<PlayerInfo>();
        const int n = 10;
        const float dd = 360/n;
        float deg = 0.0f;
        for (int i = 0; i < n; i++){
            PlayerInfo pi = new PlayerInfo();
            deg = i * dd;
            pi.positionX =  4 * Mathf.Cos(deg);
            pi.positionY =  -1.5f + 2 * Mathf.Sin(deg);
            pis.Add(pi);
        }
        SetupPlayers(pis.ToArray());
        TaskDisplayController.instance.AddNormalText("Kill everyone on this ship without being detected. (0/10)", Color.white);
    }
    public void SetupCaptureShip(){
        if (isMultiplayer){

        }

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
            go = Instantiate(prefabGamePlayer);
            playerControllers.Add(go.GetComponent<PlayerController>());
            playerControllers[playerControllers.Count - 1].SetPlayerInfo(pi);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
