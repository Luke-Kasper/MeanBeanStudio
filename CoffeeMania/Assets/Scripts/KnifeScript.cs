using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeScript : MonoBehaviour
{
    public float timer;
    public float length;
    public float radius;

    private GameObject player;
    private PlayerScript playerScript;
    private PlayerScript.Buff knifeBuff;

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
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;
            playerScript = player.GetComponent<PlayerScript>();

            knifeBuff = new PlayerScript.Buff
            {
                name = "Knife",
                timer = timer,
                Active = KnifeActive,
                Expire = KnifeExpire
            };

            playerScript.AddBuff(knifeBuff);
        }
    }

    private void KnifeActive()
    {
        knifeBuff.timer -= Time.deltaTime;

        Vector3 knifeHilt = player.transform.position;
        Vector3 knifeTip = knifeHilt + (player.transform.forward * length);

        Collider[] colliders = Physics.OverlapCapsule(knifeHilt, knifeTip, radius);
        //Debug.Log("collider count = " + colliders.Length);
        for (int i = 0; i < colliders.Length; ++i)
        {
            if (colliders[i].CompareTag("Obstacle"))
            {
                Destroy(colliders[i].gameObject);
                timer = 0;
                break;
            }
        }
    }

    private void KnifeExpire()
    {

    }
}
