using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerColideWall : MonoBehaviour
{
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "Wall")
        {
            //checks if hit something in front of character (z direction only)
            if (hit.point.z > transform.position.z + controller.radius)
            {
                print("hit (forward)");
            }
            if (hit.point.x > transform.position.z + controller.radius)
            {
                print("hit (right)");
            }
            if (hit.point.x < transform.position.z + controller.radius)
            {
                print("hit (LEft)");
            }
        }
    }
}