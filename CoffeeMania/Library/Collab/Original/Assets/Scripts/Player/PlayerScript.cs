using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public class Controls
    {
        public KeyCode strafeLeft = KeyCode.A;
        public KeyCode strafeRight = KeyCode.D;
        public KeyCode turnLeft = KeyCode.LeftArrow;
        public KeyCode turnRight = KeyCode.RightArrow;
        public KeyCode jump = KeyCode.Space;
        public KeyCode slide = KeyCode.S;
        public KeyCode shoot = KeyCode.C;
    }

    public class Buff
    {
        public float timer;
        public delegate void Update();
        public Update update;
    }

    public float speed;
    public float strafeSpeed;

    private Controls controls = new Controls();
    private CharacterController controller;
    private bool invertControls = false;
    private List<Buff> buffs = new List<Buff>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        UpdateBuffs();
    }

    private void Move()
    {
        Vector3 direction = transform.forward;

        if (Input.GetKey(controls.strafeLeft))
        {
            direction += -transform.right;
        }

        if (Input.GetKey(controls.strafeRight))
        {
            direction += transform.right;
        }

        controller.Move(direction * speed * Time.deltaTime);
    }

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);

        // maybe implement check if buff already exists, then refresh timer instead of stacking another
    }

    private void UpdateBuffs()
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
