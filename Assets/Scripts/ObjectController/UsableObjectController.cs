using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObjectController : MonoBehaviour
{
    public List<Transform> usableMountPositions;
    public List<bool> isMountUsed;
    // Start is called before the first frame update
    public GameObject[] prefabUsabeObjects;
    public List<GameObject> createdUsableObjects;
    public static GameObject[] allUsableObject;
    public static UsableObjectController instance;
    void Start()
    {
        if (usableMountPositions == null || usableMountPositions.Count == 0)
        {
            usableMountPositions = new List<Transform>();
            GameObject[] gOs = GameObject.FindGameObjectsWithTag("UsableObjectMount");
            for (int i = 0; i < gOs.Length; i++)
            {
                usableMountPositions.Add(gOs[i].transform);
                isMountUsed.Add(false);
            }
        }
        allUsableObject = GameObject.FindGameObjectsWithTag("UsableObject");
        instance = this;
        createdUsableObjects = new List<GameObject>();
    }
    public void MountObject(int objectIndex, int mountIndex)
    {
        if (objectIndex >= prefabUsabeObjects.Length || objectIndex < 0) return;
        if (mountIndex >= usableMountPositions.Count || mountIndex < 0) return;
        GameObject gO = Instantiate(prefabUsabeObjects[objectIndex], usableMountPositions[mountIndex].position, Quaternion.identity, usableMountPositions[mountIndex]);
        createdUsableObjects.Add(gO);
    }
    public void MountObject(int objectIndex, Vector2 position)
    {
        if (prefabUsabeObjects.Length <= objectIndex || objectIndex < 0) return;
        for (int i = 0; i < usableMountPositions.Count; i++)
        {
            if (isMountUsed[i]) continue;
            if (Vector2.Distance(position, usableMountPositions[i].position) < 0.1f)
            {

            }
        }
    }
}
