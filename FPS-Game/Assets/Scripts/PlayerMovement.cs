using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public CharacterController controller;
    public float palyerSpeed = 12f;

    //Gravity
    private Vector3 velocity;
    public float g = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;

    //Jump
    public float jumpHeight = 3f;


    // Update is called once per frame
    void Update()
    {
        //check if we are on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y<0)
        {
            velocity.y = -2f;
        }

        float xMovement = Input.GetAxis("Horizontal");
        float zMovement = Input.GetAxis("Vertical");

        //move player
        Vector3 move = transform.right * xMovement + transform.forward * zMovement;
        controller.Move(move * palyerSpeed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * g);
        }

        //gravity
        velocity.y += g * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
