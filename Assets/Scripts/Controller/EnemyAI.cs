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
    private bool sentMoveCommand ;
    private float tmpDistance;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        sentMoveCommand = false;
        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
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
        
/*
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Add force to move character.
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            currentTransform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            currentTransform.localScale = new Vector3(-1f, 1f, 1f);
        }
        */
    }
}
