using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    public List<Collider2D> areaList;
    public static AreaController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

}
