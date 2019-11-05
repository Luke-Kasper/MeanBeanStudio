using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PlayerSlide : MonoBehaviour
{
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            controller.transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            controller.transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
