using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public class Controls
    {
        public KeyCode strafeLeft = KeyCode.A;
        public KeyCode strafeRight = KeyCode.D;
        public KeyCode turnLeft = KeyCode.Q;//turn 90 degrees left
        public KeyCode turnLeftTwo = KeyCode.LeftArrow;//turn 90 degrees left
        public KeyCode turnRight = KeyCode.E;//turn 90 degrees right
        public KeyCode turnRightTwo = KeyCode.RightArrow;//turn 90 degrees right
        public KeyCode jump = KeyCode.Space;
        public KeyCode slide = KeyCode.S;
        public KeyCode shoot = KeyCode.C;
        public KeyCode ultPower = KeyCode.F;
    }


    public class Buff
    {
        public string name;
        public float timer;
        public delegate void Update();
        public Update Active;
        public Update Expire;
    }

    public int lifeTotal;
    public float runSpeed;
    public float turnSpeed;
    public float jumpSpeed;
    public float gravity;
    public GameObject projectile;
    public Controls controls = new Controls();

    private int life;
    private bool turning = false;
    private float verticalVelocity = 0;
    private float angleToTurn = 0; // total remaining angle to turn when the player is turning
    private float turnAngle = 0; // the speed at which the player is turning
    private CharacterController controller;
    private List<Buff> buffs = new List<Buff>();


    public GameObject EndScreen;
    //strafing
    public bool LimitDistanceFromCentor = true;
    public float MaxDistanceFromCentor = 5;
    private float DistanceFromCentor = 0;
    private float RoundNum;// used duing caluation
    private int TileNumX;
    private int TileNumZ;
    private bool NegativeDirection = false;

    private bool allowStrafeRight = true;
    private bool allowStrafeLeft = true;

    void Start()
    {
        life = lifeTotal;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        UpdateBuffs();
        Move();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //print(hit.collider.tag);
        if (hit.collider.CompareTag("Obstacle"))
        {
            //SceneManager.LoadScene(0);
            EndScreen.active = true;
            Time.timeScale = 0;
        }

        if (hit.collider.CompareTag("Wall"))
        {
        }

    }

    private void Move()
    {
        Vector3 direction = transform.forward;

        if (LimitDistanceFromCentor == true)
        {
            strafe();
        }
        else
        {
            allowStrafeLeft = true;
            allowStrafeRight = true;
        }

        if (Input.GetKey(controls.strafeLeft) && allowStrafeLeft == true)
        {
            direction += -transform.right;
        }

        if (Input.GetKey(controls.strafeRight) && allowStrafeRight == true)
        {
            direction += transform.right;
        }

        if (controller.isGrounded)
        {
            if (Input.GetKey(controls.slide))
            {
                controller.transform.localScale = new Vector3(1, 0.5f, 1);
            }
            else
            {
                controller.transform.localScale = Vector3.one;

                if (Input.GetKeyDown(controls.jump))
                {
                    verticalVelocity = jumpSpeed;
                }
            }



        }

        verticalVelocity -= gravity * Time.deltaTime;
        direction.y = verticalVelocity;
        controller.Move(direction * runSpeed * Time.deltaTime);

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
        else // if not already turning
        {
            // Turns the chracter left 
            if (Input.GetKeyDown(controls.turnLeft) || Input.GetKeyDown(controls.turnLeftTwo))
            {
                turning = true;
                angleToTurn = -90;
                turnAngle = Mathf.Lerp(0, angleToTurn, Time.deltaTime * turnSpeed);
            }
        
            // Turns the charcter right
            if (Input.GetKeyDown(controls.turnRight) || Input.GetKeyDown(controls.turnRightTwo))
            {
                turning = true;
                angleToTurn = 90;
                turnAngle = Mathf.Lerp(0, angleToTurn, Time.deltaTime * turnSpeed);
            }
        }



        if (Input.GetKeyDown(controls.ultPower))
        {
            Score score = FindObjectOfType<Score>();//access score script
            if (score.beanBarCounter >= score.MaxBeanBarAmount)//checks the bean bar is full
            {
                Instantiate(projectile, controller.transform.position + (controller.transform.forward * 5), new Quaternion());//creates the ult bean at the player

                score.beanBarCounter = 0;//resets the bean bar back to 0
            }
        }

    }
    private void strafe()
    {
        RoundNum = controller.transform.position.z / 50;
        if (RoundNum > 0)
        { RoundNum += 0.5f; }
        else
        { RoundNum -= 0.5f; }
        int TileNumZ = (int)RoundNum;

        RoundNum = controller.transform.position.x / 50;
        if (RoundNum > 0)
        { RoundNum += 0.5f; }
        else
        { RoundNum -= 0.5f; }
        int TileNumX = (int)RoundNum;

        if (controller.transform.forward == Vector3.forward)
        {   //print("forward");
            DistanceFromCentor = (TileNumX * 50) - controller.transform.position.x;
            NegativeDirection = false;
        }
        else if (controller.transform.forward == Vector3.forward * -1)
        {   //print("back");
            DistanceFromCentor = (TileNumX * 50) - controller.transform.position.x;
            NegativeDirection = true;
        }
        else if (controller.transform.forward == Vector3.right)
        {   //print("right");
            DistanceFromCentor = (TileNumZ * 50) - controller.transform.position.z;
            NegativeDirection = true;
        }
        else if (controller.transform.forward == Vector3.right * -1)
        {   //print("left");
            DistanceFromCentor = (TileNumZ * 50) - controller.transform.position.z;
            NegativeDirection = false;
        }
        //print("Z:" + TileNumZ + "X:" + TileNumX + " distance:" + DistanceFromCentor + "neg" + NegativeDirection);



        if (NegativeDirection == false)
        {
            if (DistanceFromCentor < MaxDistanceFromCentor)
            { allowStrafeLeft = true; }
            else
            { allowStrafeLeft = false; }

            if (DistanceFromCentor > -MaxDistanceFromCentor)
            { allowStrafeRight = true; }
            else
            { allowStrafeRight = false; }
        }
        else
        {

            if (DistanceFromCentor > -MaxDistanceFromCentor)
            { allowStrafeLeft = true; }
            else
            { allowStrafeLeft = false; }

            if (DistanceFromCentor < MaxDistanceFromCentor)
            { allowStrafeRight = true; }
            else
            { allowStrafeRight = false; }

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



