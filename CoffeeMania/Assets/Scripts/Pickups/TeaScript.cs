using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeaScript : MonoBehaviour
{
    public float timer;

    private GameObject player;
    private PlayerScript playerScript;
    private PlayerScript.Buff teaBuff;

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

            teaBuff = new PlayerScript.Buff
            {
                name = "Tea",
                timer = timer,
                Active = TeaActive,
                Expire = TeaExpire
            };

            playerScript.AddBuff(teaBuff);

            KeyCode temp = playerScript.controls.strafeLeft;
            playerScript.controls.strafeLeft = playerScript.controls.strafeRight;
            playerScript.controls.strafeRight = temp;

            temp = playerScript.controls.turnLeft;
            playerScript.controls.turnLeft = playerScript.controls.turnRight;
            playerScript.controls.turnRight = temp;

            Destroy(gameObject);
        }
    }

    private void TeaActive()
    {
        teaBuff.timer -= Time.deltaTime;
    }

    private void TeaExpire()
    {
        KeyCode temp = playerScript.controls.strafeLeft;
        playerScript.controls.strafeLeft = playerScript.controls.strafeRight;
        playerScript.controls.strafeRight = temp;

        temp = playerScript.controls.turnLeft;
        playerScript.controls.turnLeft = playerScript.controls.turnRight;
        playerScript.controls.turnRight = temp;
    }
}
