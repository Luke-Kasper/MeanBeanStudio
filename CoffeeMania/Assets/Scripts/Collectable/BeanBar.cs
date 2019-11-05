using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanBar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Transform bar = transform.Find("Bar");
        bar.localScale = new Vector3(1f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        Transform bar = transform.Find("Bar");
        float bearBar =FindObjectOfType<Score>().beanBarCounter;
        float maxBeanBar = FindObjectOfType<Score>().MaxBeanBarAmount;
        float barFillAmount = bearBar / maxBeanBar;

        bar.localScale = new Vector3(1f, barFillAmount);
    }
}
