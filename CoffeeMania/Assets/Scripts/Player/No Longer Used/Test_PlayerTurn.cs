using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerTurn : MonoBehaviour
{
    public CharacterController controller;

    public float TurnSpeed = 2;

    private bool TurnButtonDown = false;

    private float Degrees = 0;

    public Vector3 PathDirection = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        Test_PlayerCameraCentor PlayerCamera = GetComponent<Test_PlayerCameraCentor>();
        PlayerCamera.TurnLeft();


    }

    // Update is called once per frame
    void Update()
    {
       
        if (TurnButtonDown == false)
        {
            //turn 90 degrees
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
            {
                Degrees += 90;
                //controller.transform.Rotate(new Vector3(0, 90, 0));
                TurnButtonDown = true;
            }

            if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                Degrees += -90;
                //controller.transform.Rotate(new Vector3(0, -90, 0));
                TurnButtonDown = true;
            }
        }


        if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            TurnButtonDown = false;
        }

        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            TurnButtonDown = false;
        }


        if (Degrees == 0)
        {
        }
        else
        {
            if (Degrees > 0)
            {
                //turn right 90 degrees
                float RotationVal = Mathf.Lerp(0, 90, Time.deltaTime* TurnSpeed);
                if ((Degrees - RotationVal) < 0)
                {
                    controller.transform.Rotate(new Vector3(0, Degrees, 0));
                    Degrees = 0;
                }
                else
                {
                    controller.transform.Rotate(new Vector3(0, RotationVal, 0));
                    Degrees -= RotationVal;
                }

            }
            else
            {
                //turn left 90 degrees
                float RotationVal = Mathf.Lerp(0, -90, Time.deltaTime * TurnSpeed);
                if ((Degrees + RotationVal) > 0)
                {
                    controller.transform.Rotate(new Vector3(0, Degrees, 0));
                    Degrees = 0;
                }
                else
                {
                    controller.transform.Rotate(new Vector3(0, RotationVal, 0));
                    Degrees -= RotationVal;
                }
            }
        }
    }
}
