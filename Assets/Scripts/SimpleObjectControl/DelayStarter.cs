using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayStarter : MonoBehaviour
{
    public int delayTime;
    public static DelayStarter instance;
    public Text TimeDisplayer;
    public Text TaskDisplayer;
    
    void Start(){
        instance = this;
    }

    public void StartCounting(){
        StartCoroutine(ExecDelayStart());
        int g = GameController.instance.gameMode;
        if (g == 1){
            TaskDisplayer.text = StringTemplate.LoneWolf_Description;
            return;
        }
        if (g == 2){
            TaskDisplayer.text = StringTemplate.CTS_Description;
            return;
        }
    }

    IEnumerator ExecDelayStart(){
        while(delayTime > 0){
            TimeDisplayer.text = delayTime.ToString();
            yield return new WaitForSeconds(1.0f);
            delayTime--;
        }
        GameController.isPause = false;
        GameStatistics.instance.StartTimer();
        gameObject.GetComponent<Animator>().Play("PanelClose");
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
