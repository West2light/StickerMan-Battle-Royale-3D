﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : Character
{
    private float inputVertical;
    private float inputHorizontal;
    private float durationFloatingScore;

    public RectTransform rangeUI;
    public Joystick joystick;
    public TMP_Text txPoint;
    public TMP_Text txAddPoint;
    public float speed = 5f;

    protected override void OnEnable()
    {

        base.OnEnable();
        TeamTag = "TeamA";

    }
    private void Awake()
    {
        durationFloatingScore = 0f;
    }
    private void Start()
    {
        txPoint.text = GameDataUser.point.ToString();
        UpdateScore();
    }

    protected override void Update()
    {
        base.Update();
        CheckInput();
        UpdateDance();
        CheckNearestEnemy();
        UpdateRange();
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
                CheckNearestEnemy();
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
    private void UpdateRange()
    {
        if (rangeUI != null && Camera.main != null)
        {
            rangeUI.sizeDelta = new Vector2(rangeAttack * 2f, rangeAttack * 2f);
        }
    }
    private void CheckNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;
        for (int i = 0; i < GameController.Instance.enemies.Count; i++)
        {
            float distanceTaget = Vector3.Distance(transform.position, GameController.Instance.enemies[i].transform.position);
            if (distanceTaget < shortestDistance)
            {
                shortestDistance = distanceTaget;
                nearestEnemy = GameController.Instance.enemies[i];
                nearestEnemy.CheckTargetPoint(distanceTaget <= rangeAttack);
            }
        }
        if (nearestEnemy != null)
        {
            RotateToward(nearestEnemy);
        }

    }

    public void UpdateScore()
    {
        durationFloatingScore += Time.deltaTime;
        if (durationFloatingScore >= 2f)
        {
            txAddPoint.gameObject.SetActive(false);
        }
        else
        {

            txAddPoint.gameObject.SetActive(true);
            float speed = 1f;
            txAddPoint.transform.position += Vector3.up * speed * Time.deltaTime;
            txPoint.text = GameDataUser.point.ToString();
        }
    }
}
