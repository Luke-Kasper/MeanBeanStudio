using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_PickUpTeaBag : MonoBehaviour
{
    Test_PlayerRowMovementType2V1 Player;
    private bool Invert = false;
    float timer = 0;
    float InvertLeangth = 5;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Invert == true)
        { 
            if (timer < InvertLeangth)
            {
                Player.InvertControls = true;
                print("controls inverted");
            }
            else
            {
                Player.InvertControls = false;
                Invert = false;
                timer = 0;
                Destroy(gameObject);
            }
            timer += Time.deltaTime ;
        }
        else
        {
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player = FindObjectOfType<Test_PlayerRowMovementType2V1>();
            timer = 0;
            Invert = true;
        }
        else
        {
        }
    }
}
