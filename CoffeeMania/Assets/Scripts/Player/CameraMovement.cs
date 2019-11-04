using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public CharacterController controller;

    public GameObject playerCamera;

    public float MaxDistanceLeftFromCentor = -10;
    public float MaxDistanceRightFromCentor = 10;
    private float DistanceFromCentor = 0;

    private float RoundNum;// used duing caluation
    private int TileNumX;
    private int TileNumZ;

    public float CameraDistanceBehind = 5;
    public float CameraHightBehind = 2;

    public float angle = 0;


    public float cameraTrunSpeed = 2;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RoundNum = controller.transform.position.z / 50;

        if (RoundNum < 0)
        {RoundNum -= 0.5f;}
        else
        {RoundNum += 0.5f;}

        TileNumZ = (int)RoundNum;

        RoundNum = controller.transform.position.x / 50;
        if (RoundNum < 0)
        { RoundNum -= 0.5f; }
        else
        { RoundNum += 0.5f; }
        TileNumX = (int)RoundNum;



        if (controller.transform.forward == Vector3.forward)
        {
            //print("forward");
            DistanceFromCentor = (TileNumX * 50) - controller.transform.position.x;

            //playerCamera.transform.position = new Vector3(TileNumX * 50,CameraHightBehind, controller.transform.position.z - CameraDistanceBehind);
            //playerCamera.transform.eulerAngles = new Vector3(angle, 0, 0);

            CameraTransformPosition(TileNumX * 50, controller.transform.position.z - CameraDistanceBehind, 0);
        }
        else if (controller.transform.forward == Vector3.forward * -1)
        {
            //print("back");
            DistanceFromCentor = (TileNumX * 50) - controller.transform.position.x;

            //playerCamera.transform.position = new Vector3(TileNumX * 50, CameraHightBehind, controller.transform.position.z + CameraDistanceBehind);
            //playerCamera.transform.eulerAngles = new Vector3(angle, 180, 0);

            CameraTransformPosition(TileNumX * 50, controller.transform.position.z + CameraDistanceBehind, 180);
        }
        else if (controller.transform.forward == Vector3.right)
        {
            //print("right");
            DistanceFromCentor = (TileNumZ * 50) - controller.transform.position.z;

            //playerCamera.transform.position = new Vector3(controller.transform.position.x - CameraDistanceBehind,CameraHightBehind, TileNumZ * 50);
            //playerCamera.transform.eulerAngles = new Vector3(angle, 90, 0);

            CameraTransformPosition(controller.transform.position.x - CameraDistanceBehind, TileNumZ * 50, 90);
        }
        else if (controller.transform.forward == Vector3.right * -1)
        {
            //print("left");
            DistanceFromCentor = (TileNumZ * 50) - controller.transform.position.z;

            //playerCamera.transform.position = new Vector3(controller.transform.position.x + CameraDistanceBehind, CameraHightBehind, TileNumZ * 50);
            //playerCamera.transform.eulerAngles = new Vector3(angle, -90, 0);

            CameraTransformPosition(controller.transform.position.x + CameraDistanceBehind, TileNumZ * 50, -90);
        }


    }

    void CameraTransformPosition(float X,float Z,float direction)
    {

        playerCamera.transform.position = new Vector3(X, CameraHightBehind,Z);

        if (playerCamera.transform.eulerAngles.y == direction)
        {

        }
        else
        {
            //if (direction == -90)
            //{
            //    playerCamera.transform.eulerAngles = new Vector3(angle, Mathf.Abs(Mathf.Lerp(playerCamera.transform.eulerAngles.y, direction, Time.deltaTime * cameraTrunSpeed)), 0);
            //    print("mm");

            //}
            //else
            //{
            //    playerCamera.transform.eulerAngles = new Vector3(angle, Mathf.Abs(Mathf.Lerp(playerCamera.transform.eulerAngles.y, direction, Time.deltaTime * cameraTrunSpeed)), 0);
            //    //print("angle =" + playerCamera.transform.eulerAngles.y + "direction" + direction);
            //}

        }

        playerCamera.transform.eulerAngles = new Vector3(angle, direction, 0);
    }


}

