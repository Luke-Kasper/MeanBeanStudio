using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerRowMovementType2V1 : MonoBehaviour
{
    public CharacterController controller;

    public float speed; //movement speed

    public bool InvertControls = false;
    private KeyCode Right = KeyCode.D;//default control to strafe right
    private KeyCode Left = KeyCode.A;//default control to strafe left

    public bool ConstantlyForward = true;//while true player will constanlty move forward
    private Vector3 Direction = Vector3.zero;


    public bool LimitDistanceFromCentor = true;
    public float StrafeSpeed = 0.5f;
    public float MaxDistanceLeftFromCentor = -10;
    public float MaxDistanceRightFromCentor = 10;
    private float DistanceFromCentor = 0;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //print("distance form centor" + DistanceFromCentor);

        if (InvertControls == true)
        {
            Right = KeyCode.A;
            Left = KeyCode.D;
        }
        else
        {
            Right = KeyCode.D;
            Left = KeyCode.A;
        }

        if (Input.GetKey(Right))
        {
            if (DistanceFromCentor < 10)
            {
                controller.Move(controller.transform.right * (1 * StrafeSpeed));
                DistanceFromCentor += (1 * StrafeSpeed);
            }
        }
        if (Input.GetKey(Left))
        {
            if (DistanceFromCentor > -10)
            {
                controller.Move(controller.transform.right * (-1 * StrafeSpeed));
                DistanceFromCentor += (-1 * StrafeSpeed);
            }
        }

        if (ConstantlyForward == true)
        {
            Direction = (controller.transform.forward);
        }
        else
        {
            Direction = Vector3.zero;
        }

        Direction *= Time.deltaTime * speed;
        controller.Move(Direction);
    }
}
