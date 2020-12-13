using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    //0: up, 1: down, 2: left,  3: right
    public int sensorIndex;
    public PlayerController attachedPlayer;

    void Start()
    {

    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        attachedPlayer.collisionSensor[sensorIndex] = true;
    }
    void OnCollisionExit(Collision collisionInfo)
    {
        attachedPlayer.collisionSensor[sensorIndex] = false;
    }
}
