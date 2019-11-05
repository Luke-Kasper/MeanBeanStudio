using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    public float timer;
    public float radius;

    private GameObject player;
    private PlayerScript playerScript;
    private PlayerScript.Buff magnetBuff;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerScript = player.GetComponent<PlayerScript>();
            //Debug.Log("Magnet picked up");
            magnetBuff = new PlayerScript.Buff
            {
                name = "Magnet",
                timer = timer,
                Active = MagnetActive,
                Expire = MagnetExpire
            };

            playerScript.AddBuff(magnetBuff);

            Destroy(gameObject);
        }
    }

    private void MagnetActive()
    {
        //float beancount = 0;
        magnetBuff.timer -= Time.deltaTime;
        //Debug.Log("magnet active");
        Collider[] colliders = Physics.OverlapSphere(player.transform.position, radius);
        //Debug.Log("colliders found = " + colliders.Length);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Bean"))
            {
                //Debug.Log("beans found = " + ++beancount);
                collider.transform.position = Vector3.MoveTowards(collider.transform.position, player.transform.position, Time.deltaTime * playerScript.runSpeed * 2);
            }
        }
    }

    private void MagnetExpire()
    {

    }
}
