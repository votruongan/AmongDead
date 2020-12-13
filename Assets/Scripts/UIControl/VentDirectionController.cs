using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentDirectionController : MonoBehaviour
{   
    public CameraController camera;
    public MainPlayerController mainPlayer;
    public Transform ventPanelObject;
    public VentController sourceVent;
    public VentController[] originalTargetVents;
    public VentController[] targetVents;
    public bool isInVent;

    void Start()
    {        
        // this.SetActive(false);
        isInVent = false;
        if (camera == null) camera = GameObject.Find("MainCameraContainer").GetComponent<CameraController>();
        if (mainPlayer == null) mainPlayer = GameObject.Find("MainPlayer").GetComponent<MainPlayerController>();
        SetVentController(false);
    }
    public void SelectGotoVent(int dir){
        // camera.lockToGameObject = targetVents[dir].gameObject;
        mainPlayer.TeleportTo(targetVents[dir].transform.position);
        int invertDir = (dir + 4) % 8;
        // Debug.Log(invertDir);
        VentController[] nvc = new VentController[8];
        nvc[invertDir] = sourceVent;
        if (targetVents[dir] == sourceVent) nvc = originalTargetVents;
        targetVents = nvc;
        DisplayArrow(targetVents);
    }
    IEnumerator execVentingOut(){
        mainPlayer.displayer.spriteRenderer.enabled = true;
        mainPlayer.displayer.PlayAnimation("JumpOutVent");
        yield return null;
    }
    IEnumerator execVentingIn(){
        mainPlayer.displayer.PlayAnimation("JumpOutVent");
        yield return new WaitForSeconds(0.3f);
        mainPlayer.displayer.spriteRenderer.enabled = false;
    }
    public void GetOutOfVent(){
        if (!isInVent) return;
        sourceVent = null;
        isInVent = false;
        mainPlayer.isMovable = true;
        ventPanelObject.gameObject.SetActive(false);
        StartCoroutine("execVentingOut");
    }
    public void SetVentController(bool isUsingVent, VentController sVent = null, VentController[] vcArray = null){
        if (isUsingVent == false){
            ventPanelObject.gameObject.SetActive(false);
            return;
        }
        isInVent = true;
        sourceVent = sVent;
        originalTargetVents = vcArray;
        mainPlayer.isMovable = false;
        StartCoroutine("execVentingIn");
        targetVents = vcArray;
        DisplayArrow(vcArray);
        ventPanelObject.gameObject.SetActive(true);
    }
    public void DisplayArrow(VentController[] vcArray = null){
        for (int i = 0; i < vcArray.Length; i++)
        {
            if (vcArray[i] == null){
                ventPanelObject.GetChild(i).gameObject.SetActive(false);
                continue;
            }
            // Debug.Log(i.ToString() + " " + ventPanelObject.GetChild(i).gameObject);
            ventPanelObject.GetChild(i).gameObject.SetActive(true);
        }        
    }
}
