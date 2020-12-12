using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : PlayerController
{
    public Transform target;

    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    public CharacterDisplayer displayer;
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
    
    
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sentMoveCommand = false;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
        gameMode = GameController.instance.gameMode;
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

    void ExecScript(){
        if (GameController.instance.isMultiplayer){
            return;
        }
        int ind = (int) Random.Range(0f, UsableObjectController.allUsableObject.Length);
        this.target = UsableObjectController.allUsableObject[ind].transform;
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
            case 2: secs = 5.0f; break;
            default: Debug.Log("Game Mode Undefined"); return;
        }
        IEnumerator coroutine = WaitAndExecute(secs);
        StartCoroutine(coroutine);
    }

    protected void FixedUpdate()
    {
        base.FixedUpdate();
        if (path == null)
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            // displayer.PlayAnimation("CharacterIdle");
            reachedEndOfPath = true;
            return;
        }
        else
        {
            // displayer.PlayAnimation("CharacterMove");
            reachedEndOfPath = false;
        }
        tmpDistance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (tmpDistance < nextWaypointDistance)
        {
            currentWaypoint++;
            sentMoveCommand = false;
            return;
        }
        if (!sentMoveCommand){
            this.MoveTo((Vector2)path.vectorPath[currentWaypoint]);
        }
    }
}
