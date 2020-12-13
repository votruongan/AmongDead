using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatistics : MonoBehaviour
{
    public int killCount = 0;
    public int elapsedSeconds = 0;
    public static GameStatistics instance;
    //LoneWolf mode - kill count
    public int LW_KillCount;

    public int CTS_Nav_Count;
    public int CTS_Rect_Count;

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
        yield return new WaitForSeconds(1.0f);
        elapsedSeconds ++;
        Debug.Log(elapsedSeconds);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }
}
