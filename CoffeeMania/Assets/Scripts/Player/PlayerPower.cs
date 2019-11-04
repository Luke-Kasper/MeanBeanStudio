using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    public Vector3 direction = new Vector3();
    public float lifeTime = 10;//how long the ult lasts in seconds
    private float timer = 0;//timer
    // Start is called before the first frame update
    void Start()
    {
        PlayerScript player = FindObjectOfType<PlayerScript>();//access the player script to get firection to move in
        direction = player.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScript player = FindObjectOfType<PlayerScript>(); // access the player script to access the player speed, so the play cant speed up faster than the ult and hit it

        transform.position += direction * (player.runSpeed+10) * Time.deltaTime; 

        Vector3 knifeHilt = transform.position;//par of the calulation for the area the ult hits
        Vector3 knifeTip = knifeHilt + transform.forward * 5;//par of the calulation for the area the ult hits

        Collider[] colliders = Physics.OverlapCapsule(knifeHilt, knifeTip, 5);// the radius the ult hits
        for (int i = 0; i < colliders.Length; ++i)//loops thought hte objects the ult hits
        {
            if (colliders[i].tag == "Obsticale")
            {
                Destroy(colliders[i].gameObject);
            }
            else
            {

            }
            print("test" +
                "");
            if (colliders[i].tag == "ChuckWall")
            {
                print("chickwall");
                Destroy(gameObject);

            }
        }

        timer += Time.deltaTime %60;//add onto the timer so the ult runs out over time
        if (timer > lifeTime)
        {
            Destroy(gameObject);// destroys the object when the timer runs out
        }

    }

}
