using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : PlayerController
{
    public bool alarmMode;
    public Transform target;
    public float nextWaypointDistance = 3f;
    public Transform currentTransform;

    public Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    private bool sentMoveCommand;
    private float tmpDistance;
    private float lastDistance;
    private int gameMode;
    
    string[] gameModeShort  = {"LW", "CTS"};
    
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sentMoveCommand = false;
        foreach(string s in gameModeShort){
            this.transform.Find(s+"_ScriptController").gameObject.SetActive(false);
        }
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        gameMode = GameController.instance.gameMode;
        this.transform.Find(gameModeShort[gameMode - 1]+"_ScriptController").gameObject.SetActive(true);
        IEnumerator coroutine = WaitAndExecute(0.0f);
        StartCoroutine(coroutine);
    }

    void UpdatePath()
    {
        if (target == null)return;
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void AlarmMode(){
        if (gameMode != 1) return;
        gameMode = 11;
        this.target = UsableObjectController.alarmButton;
        Debug.Log("ALARM MODE CALLED");
    }

    void ExecScript(){
        if (GameController.instance.isMultiplayer || gameMode > 10){
            return;
        }
        int ind;
        switch (gameMode)
        {
            case 1: 
                ind = (int) Random.Range(0f, UsableObjectController.allUsableObject.Length);
                this.target = UsableObjectController.allUsableObject[ind].transform;
            break;
            case 2: 
                ind = (int) Random.Range(0f, AreaController.instance.areaList.Count);
                this.target = AreaController.instance.areaList[ind].transform;
            break;
            default: Debug.Log("Game Mode Undefined"); return;
        }
    }

    IEnumerator WaitAndExecute(float seconds){
        yield return new WaitForSeconds(seconds);
        ExecScript();
        ActAsScript();
    }

    void ActAsScript(){
        float secs = 0.0f;
        switch (gameMode)
        {
            case 1: secs = 15.0f; break;
            case 2: secs = 20.0f; break;
            default: Debug.Log("Game Mode Undefined"); return;
        }
        IEnumerator coroutine = WaitAndExecute(secs);
        StartCoroutine(coroutine);
    }

    protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (path == null || GameController.isPause || target == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        tmpDistance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (lastDistance == tmpDistance){
            // Debug.Log("Stuck " + tmpDistance.ToString());
            lastDistance = -1.0f;
            // target = null;
        }
        if (gameMode == 11){
            float targetDist = Vector2.Distance(rb.position, target.position);
            if (targetDist < 3.0f){
                // TODO - Finish game when alarm is triggered
                Debug.Log("ALARM TRIGGERED");
                GameController.instance.LoneWolfAlarmed();
            }
        }
        if (tmpDistance < nextWaypointDistance)
        {
            currentWaypoint++;
            sentMoveCommand = false;
            return;
        }
        lastDistance = tmpDistance;
        if (!sentMoveCommand){
            this.MoveTo((Vector2)path.vectorPath[currentWaypoint]);
        }
    }
}
