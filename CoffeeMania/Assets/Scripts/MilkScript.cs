using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkScript : MonoBehaviour
{
    public float timer;

    private GameObject player;
    private PlayerScript playerScript;
    private PlayerScript.Buff milkBuff;


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

            milkBuff = new PlayerScript.Buff
            {
                name = "Milk",
                timer = timer,
                Active = MilkActive,
                Expire = MilkExpire
            };
            playerScript.AddBuff(milkBuff);
            //FindObjectOfType<Score>().SetBeanMultiplier(2);
            FindObjectOfType<Score>().ScoreMultiplier(2);
            Destroy(gameObject);
        }
    }


    private void MilkActive()
    {
        milkBuff.timer -= Time.deltaTime;
    }

    private void MilkExpire()
    {
        //FindObjectOfType<Test_Score>().SetBeanMultiplier(1);
        print("milk run out");
    }

}
