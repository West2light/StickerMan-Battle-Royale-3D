using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum BehaviourState
{
    Idle,
    Run,
    Attack,
    Dead,
    Dance
}

public class Character : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 5f;
    public BehaviourState state = BehaviourState.Idle;
    public float timerIdle;
    public BaseWeapon[] weaponPrefabs;
    public BaseHat[] hatPrefabs;
    public BasePant[] pantPrefabs;
    public SkinnedMeshRenderer skinnedMesh;

    public bool isRunning;
    public Rigidbody rigidbodyCharacter;
    public BaseWeapon usingWeapon;
    public BaseHat usingHat;

    protected const string ANIM_TRIGGER_IDLE = "Idle";
    protected const string ANIM_TRIGGER_RUN = "Run";
    protected const string ANIM_TRIGGER_ATTACK = "Attack";
    protected const string ANIM_TRIGGER_DEAD = "Dead";
    protected const string ANIM_TRIGGER_DANCE = "Dance";

    public Transform handTransform;
    public Transform headTransform;
    public float throwForce = 20f;
    public Transform throwPoint;

    public float heal = 100f;

    public string TeamTag;
    protected bool isAttacking = false;

    #region Unity Methods
    protected virtual void Awake()
    {
        rigidbodyCharacter = GetComponent<Rigidbody>();
        timerIdle = 0f;
    }

    protected virtual void Start()
    {
        ReloadDefaultOutfit();
    }

    protected virtual void Update()
    {

        // CheckInput();
        UpdateIdle();
        //UpdateRun();
        // UpdateDance();

    }

    protected virtual void FixedUpdate()
    {
        UpdateRun();
    }
    #endregion

    #region Functions
    public void ReloadDefaultOutfit()
    {
        EquipWeapon((WeaponId)GameDataUser.equippedWeapon);

        EquipPant((PantId)GameDataUser.equippedPant);

        EquipHat((HatId)GameDataUser.equippedHat);
    }

    public virtual void EquipWeapon(WeaponId weaponId)
    {
        if (weaponId == WeaponId.None)
        {
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
        Debug.LogFormat("WeaponPrefabs.Length =  {0}", weaponPrefabs.Length);
        BaseWeapon prefab = weaponPrefabs[randomIndex];
        if (prefab != null)
        {
            EquipWeapon(prefab.id);
        }
    }

    public virtual void EquipHat(HatId hatId)
    {
        if (hatId == HatId.None)
        {
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
    protected virtual void RotateToward(Character targetShooter)
    {
        Vector3 direction = (targetShooter.transform.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
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
        state = BehaviourState.Dead;
        animator.SetTrigger(ANIM_TRIGGER_DEAD);
    }
    #endregion

    public virtual void TakeDamage(float damage)
    {
        heal -= damage;
        if ((int)heal <= 0f)
        {
            ChangeState(BehaviourState.Dead);
            return;
        }
    }
}
