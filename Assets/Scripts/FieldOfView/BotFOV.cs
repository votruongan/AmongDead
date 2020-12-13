using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotFOV : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask inSightMask;
    private float fov;
    public float viewDistance = 10f;
    public float fogDistance = 50f;
    public int rayCount = 180;
    private Vector3 origin;
    private float startingAngle;
    public HashSet<PlayerController> inSights;
    public HashSet<PlayerController> newSights;
    public HashSet<PlayerController> outSights;
    const float INF_SMALL = -2810f;
    public PlayerController thisPC;
    private void Start()
    {
        fov = 360f;
        origin = Vector3.zero;
        inSights = new HashSet<PlayerController>();
        thisPC = this.gameObject.GetComponent<PlayerController>();
    }
    
        public static Vector3 GetVectorFromAngle(float angle) {
            // angle = 0 -> 360
            float angleRad = angle * (Mathf.PI/180f);
            return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
        }

        public static float GetAngleFromVectorFloat(Vector3 dir) {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;

            return n;
        }

        public static int GetAngleFromVector(Vector3 dir) {
            dir = dir.normalized;
            float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (n < 0) n += 360;
            int angle = Mathf.RoundToInt(n);

            return angle;
        }

    private void Update()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        PlayerController sightObject;
        newSights = new HashSet<PlayerController>();
        outSights = new HashSet<PlayerController>();
        
        // Debug.Log("raycount: " + rayCount.ToString());
        for (int i = 0; i <= rayCount; i++)
        {
            // origin = this.transform.position;
            // Debug.Log(origin);
            RaycastHit2D[] sightHit2D = Physics2D.RaycastAll(origin, GetVectorFromAngle(angle), viewDistance, inSightMask);

            if (sightHit2D != null && sightHit2D.Length > 0)
            {
                for (int j = 0; j < sightHit2D.Length; j++)
                {
                    sightObject = sightHit2D[j].collider.gameObject.GetComponent<PlayerController>();
                    if (sightObject == null)
                        sightObject = sightHit2D[j].collider.gameObject.GetComponent<EnemyAI>();
                    if (sightObject == null)
                        sightObject = sightHit2D[j].collider.gameObject.GetComponent<MainPlayerController>();
                    if (sightObject != null && sightObject != thisPC)
                    {
                        Debug.Log("Sighted: " + sightObject.ToString());
                        newSights.Add(sightObject);
                        bool isNew = inSights.Add(sightObject);
                        // if (isNew) sightObject.InPlayerSight();
                    }
                }
            }
            angle -= angleIncrease;
        }
        Debug.Log(inSights.Count.ToString() + " _-_ " + newSights.Count.ToString());
        foreach (PlayerController vi in inSights)
        {
            float d = Vector3.Distance(vi.gameObject.transform.position, origin);
            // Debug.Log(d);
            if (!newSights.Contains(vi))
            {
                inSights.Remove(vi);
                // outSights.Add(vi);
                // vi.outSightCount++;
            }
        }
        // foreach (PlayerController vi in newSights)
        // {
        //     if (!inSights.Contains(vi))
        //     {
        //         inSights.Add(vi);
        //     }
        // }
        Debug.Log("insight: " + inSights.ToString());
    }
}
