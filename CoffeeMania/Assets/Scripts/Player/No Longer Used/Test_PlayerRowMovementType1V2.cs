using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerRowMovementType1V2 : MonoBehaviour
{

    public CharacterController controller;

    public float speed; //movement speed

    private int row = 0;

    public int numberOfRows = 3;



    private float DistanceFromCentor = 0;
    public float DistanceBetweenRows = 10;

    private bool keydown = false;

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
                if (row < (numberOfRows - 2))
                {
                    row++;
                }
                keydown = true;
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (row >-1)
                {
                    row--;
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


        if (DistanceFromCentor == (row * DistanceBetweenRows))
        {

        }
        else
        {
            if (DistanceFromCentor > (row * DistanceBetweenRows))
            {
                controller.Move(Vector3.left * -1);
                DistanceFromCentor--;
            }
            else
            {
                controller.Move(Vector3.left * +1);
                DistanceFromCentor++;
            }
        }



        Vector3 direction = (Vector3.forward);

        direction *= Time.deltaTime * speed;
        controller.Move(direction);
    }
}
