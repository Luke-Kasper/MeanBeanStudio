using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarScript : MonoBehaviour
{
    public float timer;

    private GameObject player;
    private PlayerScript playerScript;
    private PlayerScript.Buff sugarBuff;

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

            sugarBuff = new PlayerScript.Buff
            {
                name = "Sugar",
                timer = timer,
                Active = SugarActive,
                Expire = SugarExpire
            };

            playerScript.AddBuff(sugarBuff);

            playerScript.runSpeed *= 1.5f;

            Destroy(gameObject);
        }
    }

    private void SugarActive()
    {
        sugarBuff.timer -= Time.deltaTime;
    }

    private void SugarExpire()
    {
        playerScript.runSpeed /= 1.5f;
    }
}
