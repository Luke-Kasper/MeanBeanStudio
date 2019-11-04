using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    // Start is called before the first frame update

    public float totalBeanCount;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public GameData(float beanCount)
    {
        totalBeanCount = beanCount;

    }

}
