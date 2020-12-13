using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillActionController : MonoBehaviour
{
    public static List<PlayerController> killablePlayers;
    public ButtonDisplayController killButtonDisplayCotroller;

    public static ButtonDisplayController killButtonDisplay;

    // Start is called before the first frame update
    void Start()
    {
        killablePlayers = new List<PlayerController>();
        killButtonDisplay = killButtonDisplayCotroller;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name);
        VisibleInsight vi = other.gameObject.GetComponent<VisibleInsight>();
        if (other.name == "MainPlayer" || (vi != null && !vi.isInPlayerSight)
            || (other.transform.parent && other.transform.parent.gameObject.name == "MainPlayer")) return;
        // Debug.Log("Killable player: " + other.name);
        killablePlayers.Add(other.gameObject.GetComponent<PlayerController>());
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
        //exec kill
        
    }
    // public static void ExecKill()
    // {
    //     if (killablePlayers.Count <= 0) return;
    //     killButtonDisplay.SetCooldownTime(10);
    //     while (cooldownTime > 0)
    //     {
    //         yield return new WaitForSeconds(1.0f);
    //         cooldownTime--;
    //         cooldownTimeDisplay.text = cooldownTime.ToString();
    //     }
    //     cooldownTime = 0;
    //     cooldownTimeDisplay.gameObject.SetActive(false);
    //     gameObject.GetComponent<Button>().interactable = true;
    // }

    // Update is called once per frame
    void Update()
    {

    }
}
