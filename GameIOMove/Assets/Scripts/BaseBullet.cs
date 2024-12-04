using System.Collections;
using System.Diagnostics.Contracts;
using System.Threading;
using UnityEngine;


public class BaseBullet : MonoBehaviour
{
    public Transform model;
    public float speedMove;
    public float speedTurn;
    public float distanceToDestroy;
    public Rigidbody rb;
    public int index;

    public Character shooter;
    public bool isMoved = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();  // Lấy Rigidbody của viên đạn
    }
    private void Update()
    {
        Move();
        TrackingDeactive();

    }

    public void InitBullet()
    {
        rb = GetComponent<Rigidbody>();  // Lấy Rigidbody của viên đạn

    }
    private void Move()
    {
        // Position
        //Vector3 v = transform.position;
        //v.z += speedMove * Time.deltaTime;
        //transform.position = v;

        transform.Translate(0f, 0f, speedMove * Time.deltaTime);



        // Rotate
        Vector3 r = model.localEulerAngles;
        r.y += speedTurn * Time.deltaTime;
        model.localEulerAngles = r;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (shooter == null)
        {
            ReturnToPool();
            return;
        }
        if (shooter.CompareTag(other.transform.root.tag) == false)
        {
            Character targetCharacter = other.GetComponentInParent<Character>();
            if (targetCharacter != null && targetCharacter.enabled == true)
            {
                float damage = targetCharacter.maxHP / 3f;
                targetCharacter.TakeDamage(damage);
            }
            ReturnToPool();
        }
    }

    private void TrackingDeactive()
    {
        float distanceMoved = Vector3.Distance(shooter.transform.position, transform.position);
        if (distanceMoved >= distanceToDestroy)
        {
            ReturnToPool();
        }
    }


    private void ReturnToPool()
    {
        BulletPool.Instance.ReturnBullet(this);
    }
}
