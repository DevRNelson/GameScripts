using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float jumpHeight = 1f;
    private bool isGrounded;
    public float gravity = -9.8f; // Corrected gravity value to -9.8f
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();   
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }
    
    // Receives inputs for InputManager and applies them to Controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        
        if(isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        
        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);
    }

    public void Jump()
    {
        if(isGrounded){
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -1.0f * gravity); // Corrected jump calculation
        }
    }

    void Update()
    {
        isGrounded = controller.isGrounded;
        if(lerpCrouch)
        {
            crouchTimer +=Time.deltaTime;
            float p = crouchTimer / 1;
            p *=p;
            if (crouching)
                controller.jumpHeight = Mathf.Lerp(controller.height, 1, p);
                else
                controller.height = Mathf.Lerp(controller.height, 2, p);

                if (p > 1)
                {
                    lerpCrouch = false;
                    crouchTimer = 0f;
                }
        }
    }

    publicvoid Crouch ()
    {
        crouching = !crouching;
        crouchTimer = 0; 
        lerpCrouch = true;
    }

    public void Sprint()
    {
        sprinting = !sprinting;
        if (sprinting)
            speed = 8;
            else
            speed = 5;
    }
}
