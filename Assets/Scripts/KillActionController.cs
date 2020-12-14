using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillActionController : MonoBehaviour
{
    public static List<PlayerController> killablePlayers;
    public ButtonDisplayController killButtonDisplayCotroller;

    public static ButtonDisplayController killButtonDisplay;

    public bool mainIsImpostor;

    // Start is called before the first frame update
    void Start()
    {
        killablePlayers = new List<PlayerController>();
        killButtonDisplay = killButtonDisplayCotroller;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name);
        mainIsImpostor = this.transform.parent.gameObject.GetComponent<MainPlayerController>().info.isImpostor;
        VisibleInsight vi = other.gameObject.GetComponent<VisibleInsight>();
        if ( MainPlayerController.instance.isVenting || other.name == "MainPlayer" || (vi != null && !vi.isInPlayerSight)
            || (other.transform.parent && other.transform.parent.gameObject.name == "MainPlayer")) return;
        PlayerController pc = other.gameObject.GetComponent<PlayerController>();
        if (!pc.enabled || pc.info.isImpostor == mainIsImpostor) return;
        killablePlayers.Add(pc);
        if (killablePlayers.Count > 0) UIController.SetButtonActive("Kill", null);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "MainPlayer" || (other.transform.parent && other.transform.parent.gameObject.name == "MainPlayer")) return;
        // Debug.Log("Killable exit player: " + other.name);
        killablePlayers.Remove(other.gameObject.GetComponent<PlayerController>());
        if (killablePlayers.Count <= 0) UIController.SetButtonActive("Kill", null, false);
    }
    public static void KillFirstPlayer()
    {
        if (killablePlayers.Count <= 0) return;
        Debug.Log("Killing: " + killablePlayers[0].gameObject.name);
        //disable bot controller -> exec kill animation
        killablePlayers[0].PlayAnimation("CharacterDie");
        killablePlayers[0].enabled = false;
        //send to gamecontroller
        GameController.instance.HandleKill(killablePlayers[0]);
        killablePlayers.RemoveAt(0);
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
