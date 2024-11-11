using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Character
{
    private float inputVertical;
    private float inputHorizontal;

    public Joystick joystick;

    public float speed = 5f;

    protected override void OnEnable()
    {

        base.OnEnable();
        TeamTag = "TeamA";
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
        UpdateDance();
        UpdateRun();
        //if (isAttacking)
        //{
        //    enemy = GameController.Instance.enemy;
        //    RotateToward(enemy);
        //}



        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            ChangeHat();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangePant();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            ChangeState(BehaviourState.Dead);
            return;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            return;
        }
    }

    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    if (isAttacking)
    //    {
    //        RotateToward(GameController.Instance.enemy);
    //    }
    //}
    protected override void UpdateRun()
    {
        base.UpdateRun();
        if (state == BehaviourState.Run)
        {
            if (!isRunning)
            {
                ChangeState(BehaviourState.Idle);
                return;
            }

            Vector3 rotateDir = new Vector3(inputHorizontal, 0.0f, inputVertical);
            Quaternion toRotation = Quaternion.LookRotation(rotateDir, Vector3.up);
            transform.localEulerAngles = toRotation.eulerAngles;

            //transform.Translate(rotateDir * speed * Time.deltaTime, Space.World);
            Vector3 moveDes = transform.position + transform.forward * speed * Time.fixedDeltaTime;
            GetComponent<Rigidbody>().MovePosition(moveDes);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                ChangeState(BehaviourState.Attack);
                return;
            }
        }

    }

    protected override void CheckInput()
    {
        base.CheckInput();
        if (joystick != null)
        {
            inputVertical = joystick.Vertical;
            inputHorizontal = joystick.Horizontal;
        }

        isRunning = inputVertical != 0 || inputHorizontal != 0;
    }
    protected override void UpdateIdle()
    {
        base.UpdateIdle();
        if (state == BehaviourState.Idle)
        {
            timerIdle += Time.deltaTime;

            if (isRunning)
            {
                ChangeState(BehaviourState.Run);
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                RotateToward(GameController.Instance.enemy);
                ChangeState(BehaviourState.Attack);
                return;
            }

            if (timerIdle >= 5f)
            {
                ChangeState(BehaviourState.Dance);
                return;
            }
        }
    }


    protected override void UpdateDance()
    {
        base.UpdateDance();
        if (state == BehaviourState.Dance)
        {
            if (isRunning)
            {
                ChangeState(BehaviourState.Run);
                return;
            }
        }
    }

    protected override void RotateToward(Character targetShooter)
    {
        base.RotateToward(targetShooter);
    }

    public void SetJoystick(Joystick newJoystick)
    {
        joystick = newJoystick;
    }
    protected override void BeginAttack()
    {
        base.BeginAttack();
        isAttacking = false;
    }


}
