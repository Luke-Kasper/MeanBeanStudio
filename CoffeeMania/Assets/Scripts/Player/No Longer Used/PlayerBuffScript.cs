using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuffScript : MonoBehaviour
{
    public class Buff
    {
        public float timer;

        public delegate void Update();
        public Update update;
    }

    public List<Buff> buffs = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);

        // maybe implement check if buff already exists, then refresh timer instead of stacking another
    }

    void Start()
    {
        
    }

    void Update()
    {
        for (int i = 0; i < buffs.Count; ++i)
        {
            buffs[i].update();

            if (buffs[i].timer < 0)
            {
                Buff temp = buffs[i];
                buffs.RemoveAt(i);
                temp = null;
                i--;
            }
        }
    }
}
