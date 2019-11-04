using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_PlayerCameraCentor : MonoBehaviour
{

    public CharacterController controller;

    public GameObject Camera;

    public bool StayMiddleOfTrack;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (StayMiddleOfTrack == true)
        {
            Vector3 CameraPosition = controller.transform.position;
            CameraPosition.z += -5;
            CameraPosition.x = 0;

            Camera.transform.position = CameraPosition;
        }
        else
        {
            Vector3 CameraPosition = controller.transform.position;
            CameraPosition.z += -5;

            Camera.transform.position = CameraPosition;
        }


    }
    public void TurnLeft()
    {
        print("turn left");
    }

}
