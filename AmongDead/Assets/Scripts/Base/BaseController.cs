using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    public GameInfoHolder registerToRouter;
    protected void Start()
    {
        if (registerToRouter != null) registerToRouter.subscribedController.Add(this);
    }

    public virtual void RouterCallback(){
        Debug.Log("Base Controller Callback From Router");
    }
}
