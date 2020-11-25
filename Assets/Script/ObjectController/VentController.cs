using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentController : UsableObjectInfo
{
    public MainPlayerController mainPlayerController;
    public CameraController camera;
    public VentDirectionController ventDirectionControl;
    public VentController[] targetVents;

    public override void ExecUse(){
        this.GetComponent<Animator>().Play("VentOpen");
        if (!ventDirectionControl.isInVent)
            ventDirectionControl.SetVentController(true,this,targetVents);
        else
            ventDirectionControl.GetOutOfVent();
    }

    // Start is called before the first frame update
    void Start()
    {
        mainPlayerController = GameObject.Find("MainPlayer").GetComponent<MainPlayerController>();
        camera = GameObject.Find("MainCameraContainer").GetComponent<CameraController>();
        ventDirectionControl = GameObject.Find("CONTROLLERS").GetComponent<VentDirectionController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
