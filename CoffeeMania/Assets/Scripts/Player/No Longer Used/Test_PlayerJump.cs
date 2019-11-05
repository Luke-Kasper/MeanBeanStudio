using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerJump : MonoBehaviour
{
    public CharacterController controller;

    public float jumpHight;

    public float gravity = 20.0f;

    public bool TurnOffGroundCheck = true;

    public bool UseGroundTag = false;


    private Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (TurnOffGroundCheck == true)
        {
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                moveDirection.y = jumpHight;
            }
        }
        else
        {
            if (controller.isGrounded)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirection.y = jumpHight;
                }

            }
        }
        moveDirection.y -= gravity * Time.deltaTime;

        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (UseGroundTag == true)
        {
            //checks if the collider has the "Obsticale" tag
            if (hit.collider.tag == "Ground")
            {
                //Debug.Log("Ground");
                if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
                {
                    moveDirection.y = jumpHight;
                }
            }

        }
    }

}
