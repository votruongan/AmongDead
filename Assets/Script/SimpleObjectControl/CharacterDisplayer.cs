using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDisplayer : MonoBehaviour
{
    public Collider2D collider;
    public Animator animer;
    public bool isCollided = false;
    public bool isFacingRight;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        isFacingRight = true;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        if (animer == null)
            animer = this.GetComponent<Animator>();
        if (collider == null)
            collider = this.GetComponent<Collider2D>();
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        isCollided = true;
    }
    public void ChangeFacingDirection(){
        isFacingRight = !isFacingRight;
        RotateObject(0f,180f);
    }
    public void RotateObject(float xAngle, float yAngle)
    {
        this.transform.Rotate(xAngle, yAngle, 0f, Space.Self);
    }
    public void PlayAnimation(string animationName)
    {
        // Debug.Log(animationName);
        animer.Play(animationName);
    }
    public void ChangeColor(Color upColor, Color downColor){
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        sr.material.SetColor("_DesiredColor", upColor);
        sr.material.SetColor("_DesiredColor2", downColor);
    }
}
