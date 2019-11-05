using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public float selfDestructTimer;

    private MapScript mapScript;
    private bool selfDestructActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        mapScript = this.transform.parent.gameObject.GetComponent<MapScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (selfDestructActivated)
        {
            selfDestructTimer -= Time.deltaTime;

            if (selfDestructTimer == 0)
            {
                mapScript.SpawnTile();
                mapScript.DeleteTile();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
            selfDestructActivated = true;
    }
}
