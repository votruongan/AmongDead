using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaPlayerCounter : MonoBehaviour
{
    public int teamBalance;
    public int point;
    // Start is called before the first frame update
    void Start()
    {
        teamBalance = 0;

    }
    private void OnTriggerEnter2D(Collider2D other) {
        string n = other.gameObject.name.ToLower();
        if (!n.Contains("player")) return;
        if (other.gameObject.GetComponent<PlayerController>().info.isImpostor)
            teamBalance--;
        else
            teamBalance++;
    }
    private void OnTriggerExit2D(Collider2D other) {
        string n = other.gameObject.name.ToLower();
        if (!n.Contains("player")) return;
        if (other.gameObject.GetComponent<PlayerController>().info.isImpostor)
            teamBalance++;
        else
            teamBalance--;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        point += teamBalance / Mathf.Abs(teamBalance);
        if (Mathf.Abs(point) > 999){
            GameController.instance.CaptureArea(this.gameObject.name, point > 0);
        }
    }
}
