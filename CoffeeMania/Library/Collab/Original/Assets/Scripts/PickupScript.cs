using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float spawnProbability;
    public List<GameObject> pickupSet = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value < spawnProbability)
        {
            GameObject pickup = Instantiate(pickupSet[Random.Range(0, pickupSet.Count)]);
            pickup.transform.SetParent(this.transform);
            pickup.transform.localPosition = Vector3.zero;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
