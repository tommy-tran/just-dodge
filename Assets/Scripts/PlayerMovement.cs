using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float runSpeed = 5f;
    public float walkSpeed = 1f;

    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Move(h, v);
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = Input.GetButton("Shift") ? movement.normalized * walkSpeed * Time.deltaTime : movement.normalized * runSpeed * Time.deltaTime;
        if (movement != Vector3.zero)
        {
            playerRigidbody.MovePosition(transform.position + movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }
    }

    void Animating(float h, float v)
    {
        bool IsWalking = Input.GetButton("Shift");
        bool IsMoving = h != 0f || v != 0f;
        
        anim.SetBool("IsWalking", IsWalking);
        anim.SetBool("IsMoving", IsMoving);
    }

}