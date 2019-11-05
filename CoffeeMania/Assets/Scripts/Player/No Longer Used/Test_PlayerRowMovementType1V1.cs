using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerRowMovementType1V1 : MonoBehaviour
{

    public CharacterController controller;

    public float speed; //movement speed

    private int row = 1;

    public int numberOfRows = 3;


    private bool keydown = false;

    private Vector3 Test_LocationVector = Vector3.left * 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (keydown == false)
        { 
            if (Input.GetKey(KeyCode.A))
            {
                if (row > 0)
                {
                    row--;
                    controller.Move(Vector3.left * 10);

                }
                keydown = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (row < (numberOfRows - 1))
                {
                    row++;
                    controller.Move(Vector3.left * -10);

                }
                keydown = true;
            }

        }
        else
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                print("keys up");
                keydown = false;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                print("keys up");
                keydown = false;
            }

        }


        Vector3 direction = (Vector3.forward);

        direction *= Time.deltaTime * speed;
        controller.Move(direction);
    }
}
