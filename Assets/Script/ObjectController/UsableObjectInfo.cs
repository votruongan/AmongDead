using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsableObjectInfo : MonoBehaviour
{
    public string buttonName;
    private int colorDir = -1;
    public GameObject prefabGame;
    public Sprite usedSprite;
    public bool isActive;
    public bool needFocus;

    public bool isMainPlayerIn;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    protected void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        Color col = spriteRenderer.color;
        spriteRenderer.color = col;
        // sprite = this.GetComponent<SpriteRenderer>().sprite;
        // Vector3 extents = sprite.bounds.extents;
        // lineRenderer = this.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        // List<Vector3> boundingPoints = new List<Vector3>();
        // boundingPoints.Add(new Vector3(extents.x, -extents.y, 0f));
        // boundingPoints.Add(new Vector3(-extents.x, extents.y, 0f));
        // boundingPoints.Add(-extents);
        // boundingPoints.Add(extents);
        // lineRenderer.SetPositions(boundingPoints.ToArray());
        // Debug.Log(lineRenderer.positionCount);
    }
    public virtual void ExecUse()
    {
        Debug.Log("Main Player triggered object: " + this.gameObject.name);
    }

    public void MakeActiveObject()
    {
        isActive = true;
    }
    public void MakeStaticObject()
    {
        isActive = false;
        spriteRenderer.color = Color.white;
        colorDir = -1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isMainPlayerIn = true;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            isMainPlayerIn = false;

        }
    }
    int skipFrames = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isActive || !needFocus)
        {
            return;
        }
        Color col = spriteRenderer.color;
        if (col.r >= 1.0f || col.b >= 1.0f)
        {
            col.r = 1.0f;
            col.g = 1.0f;
            col.b = 1.0f;
            skipFrames++;
            colorDir = -1;
            if (skipFrames < 30) return;
        }
        col.r += colorDir * 0.025f;
        col.g += colorDir * 0.025f;
        col.b += colorDir * 0.025f;
        if (col.r <= 0.0f || col.b <= 0.0f)
        {
            col.r = 0.0f;
            col.g = 0.0f;
            col.b = 0.0f;
            colorDir = 1;
            skipFrames = 0;
        }
        spriteRenderer.color = col;
    }
}
