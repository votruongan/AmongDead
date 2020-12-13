/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FieldOfView : MonoBehaviour
{

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask inSightMask;
    private Mesh mesh;
    private float fov;
    public float viewDistance = 10f;
    public float fogDistance = 50f;
    public int rayCount = 180;
    private Vector3 origin;
    private float startingAngle;
    public HashSet<VisibleInsight> inSights;
    public HashSet<VisibleInsight> newSights;
    public HashSet<VisibleInsight> outSights;
    const float INF_SMALL = -2810f;
    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        fov = 360f;
        origin = Vector3.zero;
        inSights = new HashSet<VisibleInsight>();
    }
    void FixedUpdate()
    {

    }
    private void Update()
    {
        float angle = startingAngle;
        float angleIncrease = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount * 2 + 1 + 1];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[vertices.Length * 3 + 6];

        // vertices[0] = origin;

        int vertexIndex = 0;
        int triangleIndex = 0;
        Vector3 vertex;
        VisibleInsight sightObject;
        newSights = new HashSet<VisibleInsight>();
        outSights = new HashSet<VisibleInsight>();
        for (int i = 0; i <= rayCount; i++)
        {
            // origin = this.transform.position;
            // Debug.Log(origin);
            RaycastHit2D raycastHit2D = Physics2D.Raycast(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, layerMask);
            RaycastHit2D[] sightHit2D = Physics2D.RaycastAll(origin, UtilsClass.GetVectorFromAngle(angle), viewDistance, inSightMask);
            bool execAddSight = sightHit2D != null && sightHit2D.Length > 0;
            if (raycastHit2D.collider == null)
            {
                // No hit
                vertex = origin + UtilsClass.GetVectorFromAngle(angle) * viewDistance;
            }
            else
            {
                // Hit object
                // Debug.Log(raycastHit2D.point.ToString() + origin.ToString());
                vertex = raycastHit2D.point;
            }
            if (execAddSight)
            {
                for (int j = 0; j < sightHit2D.Length; j++)
                {
                    if (Vector3.Distance(origin, sightHit2D[j].point) >= Vector3.Distance(origin, vertex))
                        continue;
                    // Debug.Log(raycastHit2D.collider);
                    sightObject = sightHit2D[j].collider.gameObject.GetComponent<VisibleInsight>();
                    if (sightObject != null)
                    {
                        newSights.Add(sightObject);
                        bool isNew = inSights.Add(sightObject);
                        if (isNew) sightObject.InPlayerSight();
                    }
                }
            }
            // Debug.DrawLine(origin, vertex);
            vertices[vertexIndex] = vertex;
            vertices[rayCount + vertexIndex] = origin + UtilsClass.GetVectorFromAngle(angle) * fogDistance;
            if (vertexIndex > 0)
            {
                triangles[triangleIndex + 0] = rayCount + vertexIndex - 1;
                triangles[triangleIndex + 1] = rayCount + vertexIndex;
                triangles[triangleIndex + 2] = vertexIndex - 1;

                triangles[triangleIndex + 3] = vertexIndex - 1;
                triangles[triangleIndex + 4] = rayCount + vertexIndex;
                triangles[triangleIndex + 5] = vertexIndex;

                triangleIndex += 6;
            }
            vertexIndex++;
            angle -= angleIncrease;
        }
        //Fix the gap
        triangles[triangleIndex + 0] = 0;
        triangles[triangleIndex + 1] = rayCount + vertexIndex - 1;
        triangles[triangleIndex + 2] = vertexIndex - 1;

        triangles[triangleIndex + 3] = 0;
        triangles[triangleIndex + 4] = rayCount + vertexIndex - 1;
        triangles[triangleIndex + 5] = rayCount + 1;

        triangleIndex += 6;
        // Debug.Log(inSights.Count.ToString() + ' ' + newSights.Count.ToString());
        foreach (VisibleInsight vi in inSights)
        {
            float d = Vector3.Distance(vi.gameObject.transform.position, origin);
            // Debug.Log(d);
            if (!newSights.Contains(vi))
            {
                outSights.Add(vi);
                vi.outSightCount++;
            }
        }
        foreach (VisibleInsight vi in outSights)
        {
            // Debug.Log("Remove insight object " + vi.outSightCount);
            vi.OutPlayerSight();
            inSights.Remove(vi);
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.bounds = new Bounds(origin, Vector3.one * 1000f);
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        startingAngle = UtilsClass.GetAngleFromVectorFloat(aimDirection) + fov / 2f;
    }

    public void SetFoV(float fov)
    {
        this.fov = fov;
    }

    public void SetViewDistance(float viewDistance)
    {
        this.viewDistance = viewDistance;
    }

}
