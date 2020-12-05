using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour
{
    public Image imageHolder;
    // Start is called before the first frame update
    void Start()
    {
        if (imageHolder == null) 
            imageHolder = gameObject.GetComponent<Image>();
    }
    public void SetColor(Color upColor, Color downColor){
        if (imageHolder == null) 
            imageHolder = gameObject.GetComponent<Image>();
        imageHolder.material.SetColor("_DesiredColor", upColor);
        imageHolder.material.SetColor("_DesiredColor2", downColor);
    }

    public void SetColor(Color mainColor){
        SetColor(mainColor,new Color(mainColor.r-0.3f, mainColor.g-0.3f, mainColor.b-0.3f,1f));
    }
}
