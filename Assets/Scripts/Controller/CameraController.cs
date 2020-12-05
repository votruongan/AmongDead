using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class CameraController : MonoBehaviour
{
    float deltaTime = 0.0f;
    public Shaker shaker;
    public GameObject lockToGameObject;

    public ShakePreset[] shakePresets;

    // Start is called before the first frame update
    void Start()
    {
        //Shake only my shaker
        shaker.Shake(shakePresets[0]);
    }

    // Update is called once per frame
    protected void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        this.transform.position = lockToGameObject.transform.position + new Vector3(0.0f, 0.0f, -10f);
    }

    void OnGUI()
    {
        int w = Screen.width, h = Screen.height;

        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(0, 0, w, h * 2 / 100);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 5 / 100;
        style.normal.textColor = new Color(0.0f, 0.0f, 0.5f, 1.0f);
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
        GUI.Label(rect, text, style);
    }

    // 0: normal, 1: hard
    public void ShakeMode(int modeIndex)
    {
        shaker.Shake(shakePresets[modeIndex]);
    }
}
