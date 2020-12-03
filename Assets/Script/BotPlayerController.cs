using UnityEngine;
using System.Collections;
using Pathfinding;

public class BotPlayerController : PlayerController
{
    AIPath aiPath;

    GameController parent;

    // Bot info
    PlayerInfo playerInfo;

    // Main user info
    PlayerInfo mainPlayerInfo;

    // Field of view
    FieldOfView fov;
    Camera mainCamera;

    public BotPlayerController(GameController gameController, string name, bool isImposter)
    {
        this.parent = gameController;
        this.playerInfo.playerName = name;
        this.playerInfo.isImpostor = isImposter;
    }

    // Use this for initialization
    void Start()
    {
        base.Start();
        SetPlayerName(this.name, this.playerInfo.isImpostor);
        SetPlayerInfo(this.playerInfo);
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
    }

    // MARK: - Set up methods

    private void SetUpFieldOfView()
    {
        
    }

    // MARK: - Update methods

    private void UpdateMainPlayerPosition()
    {
        mainPlayerInfo.positionX = this.parent.mainPlayer.info.positionX;
        mainPlayerInfo.positionY = this.parent.mainPlayer.info.positionY;
    }

    private bool IsMainPlayerInView()
    {


        return true;
    }
}
