using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject lado, medioLado;
    Animator animLado, animMedioLado;
    public float lastTimeJump;
    public Character character;
    public float minJumpingTime = 0.5f;
    public Rigidbody2D rb;

    private void Start ()
    {
        animLado = lado.GetComponent<Animator> ();
        animMedioLado = medioLado.GetComponent<Animator> ();
        rb = GetComponent<Rigidbody2D> ();
    }

    private void Update ()
    {
        if (Input.GetKeyDown (KeyCode.Mouse1)) Tackle ();
        if (Input.GetKeyDown (KeyCode.Mouse0)) Jump ();
        if (Input.GetKeyUp (KeyCode.Mouse0)) StopJump ();

        if (isJumping && rb.velocity.y <= 0f) CheckGround ();
    }

    bool isJumping = false;

    public Transform groundChecker;
    RaycastHit2D [] groundHits = new RaycastHit2D [1];
    public LayerMask groundLayer;

    void CheckGround ()
    {
        if (Physics2D.CircleCastNonAlloc (groundChecker.position, 0.5f, Vector2.right, groundHits, 1f, groundLayer) > 0)
        {
            TouchGround ();
        }
        else print ("no ground");
    }

    void TouchGround ()
    {
        isJumping = false;
        animLado.SetTrigger ("TouchGround");
    }

    void Jump ()
    {
        isJumping = true;
        lastTimeJump = Time.time;
        animLado.ResetTrigger ("TouchGround");
        animLado.Play ("Salto");
        rb.velocity = Vector3.up * 6;
        //Invoke ("TouchGround", 4f);
    }

    void StopJump ()
    {
        var jumpingTime = minJumpingTime + lastTimeJump - Time.time;
        float t = Mathf.Clamp (jumpingTime, 0f, minJumpingTime);
        Invoke ("AnimationStopJump", t);
    }

    void AnimationStopJump ()
    {
        character.TriggerAnimation ("JumpStop");
    }

    private void PrepareTackle ()
    {
        animLado.Play ("Placaje");
        Invoke ("Tackle", 0.19f);
    }

    public void Tackle ()
    {
        medioLado.SetActive (true);
        lado.SetActive (false);
        Invoke ("EndTackle", 1.2f);
    }

    void EndTackle ()
    {
        medioLado.SetActive (false);
        lado.SetActive (true);
    }

}
