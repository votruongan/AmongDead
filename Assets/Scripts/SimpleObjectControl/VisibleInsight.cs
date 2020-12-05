using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleInsight : MonoBehaviour
{
    public bool isInPlayerSight;
    public Renderer renderer;
    public Renderer[] renderers;
    public int objectId;
    public int outSightCount = 0;
    void Start()
    {
        objectId = this.GetInstanceID();
        if (renderer == null)
            renderer = GetComponent<Renderer>();
        if (renderer == null)
            renderer = this.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        OutPlayerSight();
    }
    public void InPlayerSight()
    {
        isInPlayerSight = true;
        renderer.enabled = true;
        foreach (Renderer rdr in renderers)
        {
            rdr.enabled = true;
        }
    }
    public void OutPlayerSight()
    {
        foreach (Renderer rdr in renderers)
        {
            rdr.enabled = false;
        }
        renderer.enabled = false;
        isInPlayerSight = false;
        outSightCount = 0;
    }
}
