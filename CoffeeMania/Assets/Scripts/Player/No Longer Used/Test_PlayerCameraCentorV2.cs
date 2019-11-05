using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test_PlayerCameraCentorV2 : MonoBehaviour
{

    public CharacterController controller;

    public GameObject Camera;

    float PositionZ=0;
    float PositionX=0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {



    }
    public void TurnRight()
    {
        print("Camera turn right-");
    }
    public void TurnLeft()
    {
        print("Camera turn left-");
    }


}
