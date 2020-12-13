using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : BaseController
{
    public GameInfoHolder site;
    public List<int> doorSiteIndexs;
    public List<Animator> doors;
    public static string STATE_CLOSE = "DoorClose";
    public static string STATE_OPEN = "DoorOpen";
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        doorSiteIndexs = new List<int>();
        doors = new List<Animator>();
        if (doors == null || doors.Count == 0)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Door");
            foreach (GameObject go in gos)
            {
                doors.Add(go.GetComponent<Animator>());
            }
        }
    }
    public override void RouterCallback()
    {
        foreach (Animator a in doors)
        {
            string n = a.transform.parent.gameObject.name;
            int sIndex = GameInfoHolder.gihInstance.SiteNameToIndex(n);
            doorSiteIndexs.Add(sIndex);
        }
        // SetDoorsState(3,STATE_CLOSE);
    }

    public void SetDoorsState(int siteIndex, string state)
    {
        for (int i = 0; i < doorSiteIndexs.Count; i++)
        {
            if (doorSiteIndexs[i] != siteIndex) continue;
            foreach (Animator an in doors)
            {
                an.Play(state);
            }
        }
    }
}
