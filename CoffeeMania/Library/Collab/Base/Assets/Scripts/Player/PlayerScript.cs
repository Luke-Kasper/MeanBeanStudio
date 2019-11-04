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
        public Update Active;
        public Update Expire;
    }

    public int life;
    public float speed;

    private bool turning = false;
    private float angleToTurn = 0; // total remaining angle to turn when the player is turning
    private float turnAngle = 0; // the speed at which the player is turning
    private float gravity = Physics.gravity.y;
    private CharacterController controller;
    private Controls controls = new Controls();
    private List<Buff> buffs = new List<Buff>();

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdateBuffs();
        Move();
    }

    private void Move()
    {
        Vector3 direction = transform.forward;

        //if (controller.isGrounded)
        //{
        //    direction.y = -0.01f;
        //}
        //else
        //{
        //    direction.y += gravity;
        //}

        if (Input.GetKey(controls.strafeLeft))
        {
            direction += -transform.right;
        }

        if (Input.GetKey(controls.strafeRight))
        {
            direction += transform.right;
        }

        if (Input.GetKeyDown(controls.turnLeft))
        {
            turning = true;
            angleToTurn = -90;
            turnAngle = Mathf.Lerp(0, angleToTurn, Time.deltaTime * speed / 2);
        }

        if (Input.GetKeyDown(controls.turnRight))
        {
            turning = true;
            angleToTurn = 90;
            turnAngle = Mathf.Lerp(0, angleToTurn, Time.deltaTime * speed / 2);
        }

        if (Input.GetKeyDown(controls.jump))
        {
            if (controller.isGrounded)
            {
                direction.y += 20;
            }
        }

        if (Input.GetKey(controls.slide))
        {
            controller.transform.localScale = new Vector3(1, 0.5f, 1);
        }
        else
        {
            controller.transform.localScale = Vector3.one;
        }

        controller.Move(direction * speed * Time.deltaTime);
        
        // if the player is turning
        if (turning)
        {
            if (Mathf.Abs(angleToTurn) < Mathf.Abs(turnAngle))
            {
                turning = false;
                turnAngle = angleToTurn;
            }

            transform.Rotate(Vector3.up, turnAngle);
            angleToTurn -= turnAngle;
        }
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
            buffs[i].Active();

            if (buffs[i].timer < 0)
            {
                buffs[i].Expire();

                Buff temp = buffs[i];
                buffs.RemoveAt(i);
                temp = null;
                i--;
            }
        }
    }
}
