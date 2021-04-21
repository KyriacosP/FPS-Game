using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController controller;

    private Vector3 move_Direction;

    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Velocity;

    void Awake() {
        controller = GetComponent<CharacterController>();
    }
	
	void Update () {
        MoveThePlayer();
	}

    void MoveThePlayer() {

        move_Direction = new Vector3(Input.GetAxis("Horizontal"), 0f,
                                     Input.GetAxis("Vertical"));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();

        controller.Move(move_Direction);


    } // move player

    void ApplyGravity() {

        vertical_Velocity -= gravity * Time.deltaTime;

        // jump
        PlayerJump();

        move_Direction.y = vertical_Velocity * Time.deltaTime;

    } // apply gravity

    void PlayerJump() {

        if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;
        }

    }

} // class


































