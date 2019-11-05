using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed; //movement speed

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        float h_input = Input.GetAxis("Horizontal");
        float v_input = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h_input, 0, v_input);

        direction *= Time.deltaTime * speed;
        controller.Move(direction);

    }
}
