using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScriptController : MonoBehaviour
{
    public List<PlayerController> inSights;
    public bool isMainPlayerInsight;
    public bool bodyDetected;
    // Start is called before the first frame update
    void Start()
    {
        inSights = new List<PlayerController>();
        isMainPlayerInsight = false;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "MainPlayer"){
            isMainPlayerInsight = true;
            return;
        }
        PlayerController pc = other.GetComponent<EnemyAI>();
        if (pc == null)
            PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
            inSights.Add(pc);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name == "MainPlayer"){
            isMainPlayerInsight = false;
            return;
        }
        PlayerController pc = other.GetComponent<EnemyAI>();
        if (pc == null)
            PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
            inSights.Remove(pc);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(PlayerController pc in inSights){
            if (pc.enabled == false){
                bodyDetected = true;
                break;
            }
        }
        if (bodyDetected && isMainPlayerInsight){
            
        }
    }
}
