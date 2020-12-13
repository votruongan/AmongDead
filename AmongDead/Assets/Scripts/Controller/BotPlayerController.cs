using UnityEngine;
using System.Collections;

public class BotPlayerController : PlayerController
{
    public GameController parent;

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
        // SetPlayerName(this.name, this.playerInfo.isImpostor);
        // SetPlayerInfo(this.playerInfo);
        mainCamera = Camera.main;
        mainPlayerInfo  = new PlayerInfo();
        if (parent == null)
            parent = GameObject.Find("CONTROLLERS").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        base.FixedUpdate();
        UpdateMainPlayerPosition();

        mainCamera = Camera.main;
    }

    // MARK: - Set up methods

    private void SetUpFieldOfView()
    {
        
    }

    // MARK: - Update methods

    private void UpdateMainPlayerPosition()
    {
        mainPlayerInfo.positionX = this.parent.mainPlayer.transform.position.x;
        mainPlayerInfo.positionY = this.parent.mainPlayer.transform.position.x;
    }

    private bool IsMainPlayerInView()
    {


        return true;
    }
}
