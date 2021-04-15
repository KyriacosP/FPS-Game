using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Movement
    public CharacterController controller;
    public float playerSpeed = 12f;
    public float acceleration = 30f;
    //Gravity
    private Vector3 velocity;
    public float g = -9.81f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;
    //Jump
    public float jumpHeight = 3f;
    public Transform cam;

    void Awake() {
        cam = GameObject.FindWithTag("FpsCamera").transform;
    }

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
        controller.Move(move * playerSpeed * Time.deltaTime);

        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * g);
        }
        //Acceleration
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded) {
            playerSpeed = acceleration;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && isGrounded) {
            playerSpeed = 12f;
        }
        //crouch
        if (Input.GetKeyDown(KeyCode.C) && isGrounded) {
            cam.transform.localPosition=new Vector3 (0f,0f,0f);
        }
        if (Input.GetKeyUp(KeyCode.C) && isGrounded) {
            cam.transform.localPosition=new Vector3 (0f,4.27f,0f);
        }
        //gravity
        velocity.y += g * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
