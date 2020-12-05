using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public MainPlayerController mainPlayer;
    public List<PlayerController> playerControllers;
    public GameObject prefabGamePlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetupPlayers(PlayerInfo[] pInfo){
        GameObject go = Instantiate(prefabGamePlayer);
        foreach (PlayerInfo pi in pInfo)
        {
            if (pi.playerId == mainPlayer.info.playerId){
                mainPlayer.SetPlayerInfo(pi);
                continue;
            }
            playerControllers.Add(go.GetComponent<PlayerController>());
            playerControllers[playerControllers.Count - 1].SetPlayerInfo(pi);            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
