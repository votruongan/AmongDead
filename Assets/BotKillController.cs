using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotKillController : MonoBehaviour
{
    public bool thisIsImpostor;

    // Start is called before the first frame update
    void Start()
    {
        thisIsImpostor = this.transform.parent.gameObject.GetComponent<EnemyAI>().info.isImpostor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.name);
        PlayerController pc = other.gameObject.GetComponent<MainPlayerController>();
        if (pc == null){
           pc = other.gameObject.GetComponent<EnemyAI>();
        }
        if (pc == null){
           pc = other.gameObject.GetComponent<PlayerController>();
        }
        if (pc == null) return;
        Debug.Log(pc.info);
        Debug.Log(thisIsImpostor);
        Debug.Log("WaitAndKill " + pc.ToString());
        if (!pc.enabled || pc.info.isImpostor == thisIsImpostor) return;
        StartCoroutine(WaitAndKill(pc));
    }
    IEnumerator WaitAndKill(PlayerController pc){
        float delay = Random.Range(0.4f,1.0f);
        yield return new WaitForSeconds(delay);
        if (this.enabled){
            pc.PlayAnimation("CharacterDie");
            pc.DisableChild();
            pc.enabled = false;
        }
    }
}
