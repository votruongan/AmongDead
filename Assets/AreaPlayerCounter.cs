using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreaPlayerCounter : MonoBehaviour
{
    public int teamBalance;
    public int point;
    public bool isMainPlayerInside;
    public Slider captureProgress;
    // Start is called before the first frame update
    void Start()
    {
        teamBalance = 0;
        isMainPlayerInside = false;
        captureProgress = GameObject.Find("Progess_Tasks").GetComponent<Slider>();
        if (GameController.instance.gameMode != 2) {
            this.gameObject.SetActive(false);
            captureProgress.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        string n = other.gameObject.name.ToLower();
        if (!n.Contains("player")) return;
        if (other.gameObject.GetComponent<MainPlayerController>() != null){
            isMainPlayerInside = true;
        }
        if (other.gameObject.GetComponent<PlayerController>().info.isImpostor)
            teamBalance--;
        else
            teamBalance++;
    }
    private void OnTriggerExit2D(Collider2D other) {
        string n = other.gameObject.name.ToLower();
        if (!n.Contains("player")) return;
        if (other.gameObject.GetComponent<MainPlayerController>() != null){
            isMainPlayerInside = false;
            captureProgress.value = 0.0f;
        }
        if (other.gameObject.GetComponent<PlayerController>().info.isImpostor)
            teamBalance++;
        else
            teamBalance--;
    }
    private int lastTeam;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (teamBalance == 0) return;
        point += teamBalance / Mathf.Abs(teamBalance);
        if (isMainPlayerInside){
            captureProgress.value = ((float)Mathf.Abs(point) )/ 1000.0f;
        }
        if (Mathf.Abs(point) > 999 && point != lastTeam){
            GameController.instance.CaptureArea(this.gameObject.name, point > 0);
            lastTeam = point;
        }
    }
}
