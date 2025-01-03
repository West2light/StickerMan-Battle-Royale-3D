﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BehaviourState
{
    Idle,
    Run,
    Attack,
    Dead,
    Dance,
    Win
}
public enum TeamID
{
    A = 0,
    B = 1,
    C = 2,
    D = 3
}

public class Character : MonoBehaviour
{
    public TeamID teamID;
    public Animator animator;
    public BehaviourState state = BehaviourState.Idle;
    public float timerIdle;
    public float timerDead;
    public BaseWeapon[] weaponPrefabs;
    public BaseHat[] hatPrefabs;
    public BasePant[] pantPrefabs;
    public SkinnedMeshRenderer skinnedMesh;
    public float rangeAttack;
    public bool isRunning;
    public Rigidbody rigidbodyCharacter;
    public BaseWeapon usingWeapon;
    public BaseHat usingHat;
    protected bool isDead = false;

    protected const string ANIM_TRIGGER_IDLE = "Idle";
    protected const string ANIM_TRIGGER_RUN = "Run";
    protected const string ANIM_TRIGGER_ATTACK = "Attack";
    protected const string ANIM_TRIGGER_DEAD = "Dead";
    protected const string ANIM_TRIGGER_DANCE = "Dance";
    protected const string ANIM_TRIGGER_WIN = "Win";

    public Transform handTransform;
    public Transform headTransform;
    public float throwForce = 20f;
    public Transform throwPoint;

    public float currentHealth;
    public float maxHP;
    public string TeamTag;
    public string LastAttacker
    {
        get;
        private set;
    }
    #region Unity Methods
    protected virtual void Awake()
    {
        currentHealth = maxHP;
        rigidbodyCharacter = GetComponent<Rigidbody>();
        timerIdle = 0f;
    }

    protected virtual void OnEnable()
    {
        ReloadDefaultOutfit();
    }

    protected virtual void Update()
    {

        // CheckInput();
        UpdateIdle();

        //UpdateRun();
        // UpdateDance();
        if (isDead)
        {
            timerDead += Time.deltaTime;
            if (timerDead >= 5f)
            {
                gameObject.SetActive(false);
            }

        }

    }

    protected virtual void FixedUpdate()
    {
        UpdateRun();
    }
    #endregion

    #region Functions
    public virtual void ReloadDefaultOutfit()
    {
        EquipWeapon((WeaponId)GameDataUser.equippedWeapon);

        EquipPant((PantId)GameDataUser.equippedPant);

        EquipHat((HatId)GameDataUser.equippedHat);
    }

    public virtual void EquipWeapon(WeaponId weaponId)
    {
        if (weaponId == WeaponId.None)
        {
            if (usingWeapon != null)
            {
                Destroy(usingWeapon.gameObject);
                usingWeapon = null;
            }
            return;
        }

        for (int i = 0; i < weaponPrefabs.Length; i++)
        {
            BaseWeapon prefab = weaponPrefabs[i];

            if (prefab != null && prefab.id == weaponId)
            {
                if (usingWeapon != null)
                {
                    Destroy(usingWeapon.gameObject);
                    usingWeapon = null;
                }

                usingWeapon = Instantiate(prefab);
                usingWeapon.transform.SetParent(handTransform);
                usingWeapon.transform.localPosition = Vector3.zero;
                usingWeapon.transform.localRotation = Quaternion.identity;

                return;
            }
        }

    }
    public virtual void ChangeWeapon()
    {
        int randomIndex = Random.Range(0, weaponPrefabs.Length);
        //   Debug.LogFormat("WeaponPrefabs.Length =  {0}", weaponPrefabs.Length);
        BaseWeapon prefab = weaponPrefabs[randomIndex];
        if (prefab != null)
        {
            EquipWeapon(prefab.id);
        }
    }

    public virtual void EquipHat(HatId hatId)
    {
        //Debug.Log("EquipHat=" + hatId);
        //Debug.Log("character =" + this);
        if (hatId == HatId.None)
        {
            if (usingHat != null)
            {
                Destroy(usingHat.gameObject);
                usingHat = null;
            }
            return;
        }
        for (int i = 0; i < hatPrefabs.Length; i++)
        {
            BaseHat prefab = hatPrefabs[i];
            if (prefab != null && prefab.id == hatId)
            {
                if (usingHat != null)
                {
                    Destroy(usingHat.gameObject);
                    usingHat = null;
                }
                usingHat = Instantiate(prefab);
                usingHat.transform.SetParent(headTransform);
                usingHat.transform.localPosition = Vector3.zero;
                usingHat.transform.localRotation = Quaternion.identity;
                return;
            }
        }
    }
    public virtual void ChangeHat()
    {
        int randomIndex = Random.Range(0, hatPrefabs.Length);
        BaseHat prefab = hatPrefabs[randomIndex];
        if (prefab != null)
        {
            EquipHat(prefab.id);
        }
    }
    public virtual void EquipPant(PantId pantId)
    {
        if (pantId == PantId.None)
        {
            if (skinnedMesh.material != null)
            {
                skinnedMesh.material = null;
            }
            return;
        }
        for (int i = 0; i < pantPrefabs.Length; i++)
        {
            BasePant prefabPant = pantPrefabs[i];
            if (prefabPant.material != null && prefabPant.id == pantId)
            {
                if (skinnedMesh.material != null)
                {
                    skinnedMesh.material = null;
                }
                skinnedMesh.material = prefabPant.material;
                return;
            }

        }
    }
    public virtual void ChangePant()
    {
        int ramdomIndex = Random.Range(0, pantPrefabs.Length);
        BasePant prefabPant = pantPrefabs[ramdomIndex];
        if (prefabPant.material != null)
        {
            EquipPant(prefabPant.id);

        }
    }
    public virtual void ChangeState(BehaviourState newState)
    {
        if (state != newState)
        {
            //Debug.LogFormat("{0} to {1}", state, newState);
            state = newState;

            switch (state)
            {
                case BehaviourState.Idle: BeginIdle(); break;
                case BehaviourState.Run: BeginRun(); break;
                case BehaviourState.Attack: BeginAttack(); break;
                case BehaviourState.Dance: BeginDance(); break;
                case BehaviourState.Win: BeginWin(); break;
                case BehaviourState.Dead: Dead(); break;

            }
        }
    }

    protected virtual void CheckInput()
    {

    }
    #endregion

    #region Idle
    protected virtual void BeginIdle()
    {
        timerIdle = 0f;
        state = BehaviourState.Idle;
        animator.SetTrigger(ANIM_TRIGGER_IDLE);
    }

    protected virtual void UpdateIdle()
    {

    }
    #endregion
    protected virtual void BeginWin()
    {

        state = BehaviourState.Win;
        animator.SetTrigger(ANIM_TRIGGER_WIN);
    }
    protected virtual void UpdateWin()
    {
    }
    #region Run
    protected virtual void BeginRun()
    {
        state = BehaviourState.Run;
        animator.SetTrigger(ANIM_TRIGGER_RUN);
    }

    protected virtual void UpdateRun()
    {


    }
    #endregion

    #region Attack
    protected virtual void BeginAttack()
    {
        state = BehaviourState.Attack;
        animator.SetTrigger(ANIM_TRIGGER_ATTACK);
    }

    protected virtual void UpdateAttack()
    {


    }
    protected virtual void RotateToward(GameObject gameObject)
    {
        float distance = Vector3.Distance(transform.position, gameObject.transform.position);
        if (distance <= rangeAttack)
        {
            Vector3 direction = (gameObject.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
    }
    protected virtual void RotateToward(Character targetShooter)
    {
        float distance = Vector3.Distance(transform.position, targetShooter.transform.position);
        if (distance <= rangeAttack)
        {
            Vector3 direction = (targetShooter.transform.position - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
    }
    protected virtual void BeginDance()
    {
        state = BehaviourState.Dance;
        animator.SetTrigger(ANIM_TRIGGER_DANCE);
    }
    protected virtual void UpdateDance()
    {

    }
    public void Shoot()
    {
        usingWeapon.CreateBullet(this);
    }
    #endregion

    #region Dance

    #endregion

    #region Dead
    protected virtual void Dead()
    {
        isDead = true;
        timerDead = 0f;
        state = BehaviourState.Dead;
        animator.SetTrigger(ANIM_TRIGGER_DEAD);
    }
    #endregion

    //public virtual void TakeDamage(float damage)
    //{

    //    currentHealth -= damage;
    //    if ((int)currentHealth <= 0f)
    //    {

    //        ChangeState(BehaviourState.Dead);
    //        return;
    //    }
    //}
    public virtual void TakeDamage(float damage, GameObject gameObject)
    {
        currentHealth -= damage;
        if ((int)currentHealth <= 0)
        {
            LastAttacker = gameObject.tag;
            ChangeState(BehaviourState.Dead);
            return;
        }
    }
}
