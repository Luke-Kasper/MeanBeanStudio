using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;



public class Score : MonoBehaviour
{
    public float beans = 0;//total number of beans collected in a run   
    public float beanBarCounter = 0;//the number of beans currently in the ult bar
    public float MaxBeanBarAmount = 10;//the max number of beans to fill the ult bar
    public float BeanMultiplier = 1;//muliys the new collected beans by this ammount // not used in game so far
    public float score;// the score

    public Text scoreText;//used for hud
    public Text beanText;//used for hud

    public bool ScoreAdd = true;
    float scoreAddAmount = 20;
    float timeBetweenAdd = 0.5f;//in seconds

    float timer = 0;
    float scoreTimer = 0;

    GameObject beanBar;
    float barPercent = 0;

    // Start is called before the first frame update
    void Start()
    {
        beans = 0;
        BeanMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score" + score.ToString();
        beanText.text = "Bean" + beans.ToString();

        barPercent = 0.5f;


        //print("the bean:"+bean);
        if (ScoreAdd == true)
        {

            timer += (Time.deltaTime % 60);
            if ((scoreTimer + timeBetweenAdd) <= timer)
            {
                score += scoreAddAmount;
                scoreTimer += timeBetweenAdd;
            }

        }
        //print("score" + score+"deltatime"+ timer);

    }

    public void ScoreMultiplier(float multiplierAmount)
    {
        score= score * multiplierAmount;//multipies the score 
    }

    public void AddBean(float addAmount)
    {
        addAmount = addAmount * BeanMultiplier;
        beans += addAmount;

        save SaveSystem = GetComponent<save>();
        SaveSystem.currentTotalBeanCount += 1;
        SaveSystem.SaveFile();

        for (int i=0; i < addAmount; i++)//loop to check if the bean bar is full
        {
            if (beanBarCounter < MaxBeanBarAmount)//if not full add to it
            {
                beanBarCounter++;
            }
            else
            {
                break;
            }
        }

    }

    public void SetBeanMultiplier(float newMultiplier)
    {
        BeanMultiplier = newMultiplier;
    }




}
