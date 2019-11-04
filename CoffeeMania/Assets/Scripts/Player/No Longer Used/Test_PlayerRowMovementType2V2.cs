using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerRowMovementType2V2 : MonoBehaviour
{
    public CharacterController controller;

    public float speed; //movement speed

    bool AllowStrafing = true;

    public bool InvertControls = false;
    private KeyCode Right = KeyCode.D;//default control to strafe right
    private KeyCode Left = KeyCode.A;//default control to strafe left

    public bool ConstantlyForward = true;//while true player will constanlty move forward
    private Vector3 Direction = Vector3.zero;


    public bool LimitDistanceFromCentor = true;
    public float StrafeSpeed = 0.5f;
    public float MaxDistanceLeftFromCentor = -3;
    public float MaxDistanceRightFromCentor = 3;
    private float DistanceFromCentor = 0;

    float num;

    bool allowTurn = true;

    float center = 0;
    float centerOffset = 0;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //print("distance form centor" + DistanceFromCentor);

        //Test_PlayerTurnV2 turn = FindObjectOfType<Test_PlayerTurnV2>();





        if (controller.transform.forward == Vector3.forward)
        {
            //print("mforward");
            AllowStrafing = true;
        }
        else if (controller.transform.forward == Vector3.forward * -1)
        {
            //print("back");
            AllowStrafing = true;
        }
        else if (controller.transform.forward == Vector3.right)
        {
            //print("right");
            num = controller.transform.position.z / 50;
            num += 0.5f;
            int Inum = (int)num;
            DistanceFromCentor = (Inum * 50) - controller.transform.position.z;

            AllowStrafing = true;
        }
        else if (controller.transform.forward == Vector3.right * -1)
        {

            AllowStrafing = true;
        }
        else
        {
            AllowStrafing = false;


        }


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

            if (DistanceFromCentor < MaxDistanceRightFromCentor )
            {
                controller.Move(controller.transform.right * (1 * StrafeSpeed));
                DistanceFromCentor += (1 * StrafeSpeed);
            }
            
        }
        if (Input.GetKey(Left))
        {
            if (DistanceFromCentor > MaxDistanceLeftFromCentor )
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
