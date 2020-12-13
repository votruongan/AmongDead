using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerController : PlayerController
{
    public FieldOfView fieldOfView;
    public string isUsable = "";
    public UsableObjectInfo currentUsableObject;
    public Joystick joystick;
    public static MainPlayerController instance;
    public bool isVenting;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log(other.gameObject);
        UsableObjectInfo uoi = other.gameObject.GetComponent<UsableObjectInfo>();
        if (uoi == null) return;
        isUsable = other.gameObject.name;
        currentUsableObject = uoi;
        UIController.SetButtonActive(uoi.buttonName, uoi);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log(other.gameObject);
        UsableObjectInfo uoi = other.gameObject.GetComponent<UsableObjectInfo>();
        // if (other.gameObject.name == isUsable){
        if (uoi == null) return;
        isUsable = "";
        currentUsableObject = null;
        UIController.SetButtonActive(uoi.buttonName, uoi, false);
        // }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        // Debug.Log(other.gameObject);
        UsableObjectInfo uoi = other.gameObject.GetComponent<UsableObjectInfo>();
        if (uoi == null) return;
        // if (other.gameObject.name == isUsable){
        isUsable = "";
        currentUsableObject = null;
        UIController.SetButtonActive(uoi.buttonName, uoi);
        // }
    }
    public void UseObject()
    {
        if (currentUsableObject != null)
            currentUsableObject.ExecUse();

    }
    private void Start()
    {
        instance = this;
        base.Start();
        SetPlayerName("tester", true);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdate();
        if (GameController.isPause) return;
        if (Input.GetKey("w") || Input.GetKey("up") || joystick.Vertical > 0.1f)
        {
            MoveVertical(speedMax.y);
        }
        if (Input.GetKey("s") || Input.GetKey("down") || joystick.Vertical < -0.1f)
        {
            MoveVertical(speedMax.y, -1);
        }
        if (Input.GetKey("d") || Input.GetKey("right") || joystick.Horizontal > 0.1f)
        {
            MoveHorizontal(speedMax.x);
        }
        if (Input.GetKey("a") || Input.GetKey("left") || joystick.Horizontal < -0.1f)
        {
            MoveHorizontal(speedMax.x, -1);
        }
        if (Input.GetKey("e"))
        {
            UseObject();
        }
        if (Input.GetKey("f"))
        {
            UIController.instance.ExecKill();
        }
        fieldOfView.SetOrigin(this.transform.position + new Vector3(0.0f, -0.2f, 0.0f));
    }
}
