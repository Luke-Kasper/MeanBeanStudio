using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class keyCodegenerator : MonoBehaviour
{

    private string display;
    public GameObject code;
    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Generate()
    {
        int charAmount = Random.Range(10,10); //set those to the minimum and maximum length of your string
        for (int i = 0; i < charAmount; i++)
        {
            display += glyphs[Random.Range(0, glyphs.Length)];
            Debug.Log(display);
        }
    }
}
