using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    public int killCount = 0;
    public int elapsedSeconds = 0;

    public int CTS_Nav_Count;
    public int CTS_Rect_Count;

    public static GameStatistics instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        elapsedSeconds = 0;
    }

    public void StartTimer(){
        IEnumerator c = CountTime();
        StartCoroutine(c);
    }

    IEnumerator CountTime(){
        while (true){
            if (!GameController.isPause){
                yield return new WaitForSeconds(1.0f);
                elapsedSeconds ++;
            }
            yield return new WaitForSeconds(1.0f);
            if (FinishController.isGameFinished) break;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
