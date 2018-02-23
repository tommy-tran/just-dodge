using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float runSpeed = 5f;
    public float walkSpeed = 1f;

    public GameController gameController;
    Vector3 movement;
    Animator anim;
    Rigidbody playerRigidbody;
    public bool isAlive;

    void Awake()
    {
        isAlive = true;
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isAlive)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            Move(h, v);
            Animating(h, v);
        }

    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = Input.GetButton("Shift") ? movement.normalized * walkSpeed * Time.deltaTime : movement.normalized * runSpeed * Time.deltaTime;
        if (movement != Vector3.zero)
        {
            playerRigidbody.MovePosition(transform.position + movement);
            restrictMovement();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);
        }
    }

    void restrictMovement()
    {
        float x = Mathf.Clamp(playerRigidbody.position.x, -8f, 8f);
        float y = playerRigidbody.position.y;
        float z = Mathf.Clamp(playerRigidbody.position.z, -4.25f, 4.25f);
        playerRigidbody.position = new Vector3(x, playerRigidbody.position.y, z);
    }

    void Animating(float h, float v)
    {
        bool IsWalking = Input.GetButton("Shift");
        bool IsMoving = h != 0f || v != 0f;
        
        anim.SetBool("IsWalking", IsWalking);
        anim.SetBool("IsMoving", IsMoving);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && isAlive)
        {
            PlayerDeath();
        }
    }

    void PlayerDeath()
    {
        isAlive = false;
        anim.SetBool("IsDead", true);
        gameController.GameOver();
    }
}