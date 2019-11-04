using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerRowMovementType2V3 : MonoBehaviour
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



    private float RoundNum;// used duing caluation
    private int TileNumX;
    private int TileNumZ;
    private bool NegativeDirection = false;

    public bool Turning = false;

    int counter=0;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        RoundNum = controller.transform.position.z / 50;
        RoundNum += 0.5f;
        int TileNumZ = (int)RoundNum;

        RoundNum = controller.transform.position.x / 50;
        RoundNum += 0.5f;
        int TileNumX = (int)RoundNum;

        //print(counter + "| TileX:" + TileNumX + "TIleZ:" + TileNumZ+ "distance from centor:"+DistanceFromCentor);
        counter++;

        if (controller.transform.forward == Vector3.forward)
        {
            //print("forward");
            NegativeDirection = true;
            DistanceFromCentor = (TileNumX * 50) - controller.transform.position.x;
            Turning = true;
        }
        else if (controller.transform.forward == Vector3.forward * -1)
        {
            //print("back");
            NegativeDirection = false;
            DistanceFromCentor = (TileNumX * 50) - controller.transform.position.x;
            Turning = true;
        }
        else if (controller.transform.forward == Vector3.right)
        {
            //print("right");
            NegativeDirection = false;
            DistanceFromCentor = (TileNumZ * 50) - controller.transform.position.z;
            Turning = true;

        }
        else if (controller.transform.forward == Vector3.right * -1)
        {
            NegativeDirection = true;
            DistanceFromCentor = (TileNumZ * 50) - controller.transform.position.z;
            Turning = true;
        }
        else
        {
            Turning = false;
        }



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



        if (Turning == true)
        {

            if (NegativeDirection == false)
            {
                if (Input.GetKey(Right))
                {
                    if (DistanceFromCentor < MaxDistanceRightFromCentor)
                    {
                        controller.Move(controller.transform.right * (1 * StrafeSpeed));
                        DistanceFromCentor += (1 * StrafeSpeed);
                    }
                }
                if (Input.GetKey(Left))
                {
                    if (DistanceFromCentor > MaxDistanceLeftFromCentor)
                    {
                        controller.Move(controller.transform.right * (-1 * StrafeSpeed));
                        DistanceFromCentor += (-1 * StrafeSpeed);
                    }
                }
            }
            else
            {
                if (Input.GetKey(Right))
                {
                    if (DistanceFromCentor > MaxDistanceLeftFromCentor)
                    {
                        controller.Move(controller.transform.right * (1 * StrafeSpeed));
                        DistanceFromCentor += (1 * StrafeSpeed);
                    }
                }
                if (Input.GetKey(Left))
                {
                    if (DistanceFromCentor < MaxDistanceRightFromCentor)
                    {
                        controller.Move(controller.transform.right * (-1 * StrafeSpeed));
                        DistanceFromCentor += (-1 * StrafeSpeed);
                    }
                }
            }
        }
        else
        {


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
