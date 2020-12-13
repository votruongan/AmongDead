using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public int index;
    public string playerId;
    public string playerName;
    public bool isImpostor;
    public float positionX;
    public float positionY;
    public int colorCode;
}

public class PlayerController : MonoBehaviour
{
    public PlayerInfo info;
    public Vector2 speed;
    public Vector2 speedDecay;
    public Vector2 speedMax;
    public int horizontalDir = 1;
    public int verticalDir = 1;
    public CharacterDisplayer displayer;
    public Rigidbody2D rigidbody;
    //0: up, 1: down, 2: left,  3: right
    public bool[] collisionSensor;
    public Vector3 targetToMove;
    public bool isMakingMovement;
    public bool isMoveToTarget;
    public bool isMovable;
    public TextMesh playerNameDisplay;

    public void SetPlayerInfo(PlayerInfo pInfo)
    {
        this.info = pInfo;
        this.transform.position = new Vector3(pInfo.positionX, pInfo.positionY, 0f);
        SetPlayerName(pInfo.playerName, pInfo.isImpostor);
        SetPlayerColor(ColorCodeConverter.ColorFromCode(pInfo.colorCode));
    }

    protected void Start()
    {
        isMovable = true;
        speedDecay = new Vector2(0.01f, 0.01f);
        speedMax = new Vector2(0.1f, 0.1f);
        collisionSensor = new bool[] { false, false, false, false };
        rigidbody = this.GetComponent<Rigidbody2D>();
        SetPlayerColor(Color.magenta);
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        transform.rotation = Quaternion.identity;
    }

    void OnCollisionStay2D(Collision2D collisionInfo)
    {
        transform.rotation = Quaternion.identity;
    }

    void OnCollisionExit2D(Collision2D collisionInfo)
    {
        transform.rotation = Quaternion.identity;
    }

    public void SetPlayerName(string name, bool isImpostor = false)
    {
        if (playerNameDisplay == null) playerNameDisplay = this.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
        playerNameDisplay.text = name;
        if (isImpostor) playerNameDisplay.color = Color.red;
    }
    
    protected void FixedUpdate()
    {
        if (isMoveToTarget)
        {
            if (Vector2.Distance(this.transform.position, targetToMove) < speedDecay.x)
            {
                isMoveToTarget = false;
                speed = Vector2.zero;
            }
            else
            {
                float moveX = Mathf.Abs(Mathf.Abs(targetToMove.x) - Mathf.Abs(this.transform.position.x));
                float moveY = Mathf.Abs(Mathf.Abs(targetToMove.y) - Mathf.Abs(this.transform.position.y));
                horizontalDir = targetToMove.x > this.transform.position.x ? 1 : -1;
                verticalDir = targetToMove.y > this.transform.position.y ? 1 : -1;
                speed.x = ((moveX > speedMax.x) ? (speedMax.x) : (moveX)) * horizontalDir;
                speed.y = ((moveY > speedMax.y) ? (speedMax.y) : (moveY)) * verticalDir;
            }
        }
        float ay = Mathf.Abs(speed.y);
        float ax = Mathf.Abs(speed.x);
        if (ay > 0.0f)
        {
            speed.y += speedDecay.y * verticalDir * -1;
            speed.y = (ay <= speedDecay.y) ? (0.0f) : (speed.y);
        }
        if (ax > 0.0f)
        {
            speed.x += speedDecay.x * horizontalDir * -1;
            speed.x = (ax <= speedDecay.x) ? (0.0f) : (speed.x);
        }
        if (speed == Vector2.zero)
        {
            if (isMakingMovement)
            {
                displayer.PlayAnimation("CharacterIdle");
                isMakingMovement = false;
            }
            return;
        }
        if (isMovable)
        {
            isMakingMovement = true;
            this.transform.Translate(new Vector2(speed.x, speed.y));
        }
    }

    public void SetPlayerColor(Color colorToChange)
    {
        displayer.ChangeColor(colorToChange, new Color(colorToChange.r - 0.2f, colorToChange.g - 0.2f, colorToChange.b - 0.2f, 1f));
    }

    void MoveMove()
    {
        displayer.PlayAnimation("CharacterMove");
    }

    public void TeleportTo(Vector3 target)
    {
        this.transform.position = target;
        return;
    }

    public void MoveTo(Vector3 target, bool isAutoSmooth = false)
    {
        if ((target.x - this.transform.position.x) * horizontalDir < 0f)
        {
            displayer.ChangeFacingDirection();
            horizontalDir = -horizontalDir;
        }
        MoveMove();
        if (Vector2.Distance(new Vector2(target.x, target.y), this.transform.position) > 5f && !isAutoSmooth)
        {
            this.transform.position = target;
            return;
        }
        isMoveToTarget = true;
        targetToMove = target;
        // Debug.Log("Player " + this.name + " Move To " + target);
    }

    public void MoveHorizontal(float val, int direction = 1)
    {
        MoveMove();
        if (horizontalDir != direction)
            displayer.ChangeFacingDirection();

        horizontalDir = direction;
        speed.x = val * direction;
    }

    public void MoveVertical(float val, int direction = 1)
    {
        MoveMove();
        verticalDir = direction;
        speed.y = val * direction;
    }
}
