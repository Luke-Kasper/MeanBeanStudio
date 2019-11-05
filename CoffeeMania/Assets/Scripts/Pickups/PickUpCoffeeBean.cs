using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoffeeBean : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<Score>().AddBean(1);
            //print(score);
            Destroy(gameObject);
        }
        else
        {
        }
    }
}
