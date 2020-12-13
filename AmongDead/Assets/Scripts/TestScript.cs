using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public PlayerController playerController;
    public bool isMoved;
    // Start is called before the first frame update
    void Start()
    {
        playerController = this.GetComponent<PlayerController>();
        isMoved = true;
    }

    // Update is called once per frame 
    void Update()
    {
        if (this.transform.position.x >= -8.0f){
            playerController.MoveTo(new Vector2 (-22.1f,-1.5f), true);
        }
        if (this.transform.position.x <= -22.0f){
            playerController.MoveTo(new Vector2 (-7.9f,-1.5f), true);
        }
    }
}
