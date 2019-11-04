using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerColideObsticale : MonoBehaviour
{

    public CharacterController controller;

    bool UseObsticaleTag = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //it called every time the capsule hits something
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {

        if (UseObsticaleTag == true)
        {
            //checks if the collider has the "Obsticale" tag
            if (hit.collider.tag == "Obsticale")
            {
                Debug.Log("Test Hit Obsticale");
            }
            else
            {
                //checks if hit something in front of character (z direction only)
                if (hit.point.z > transform.position.z + controller.radius)
                {

                }
            }
        }



    }
}
