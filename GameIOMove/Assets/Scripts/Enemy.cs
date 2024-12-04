using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Character
{
    private NavMeshAgent agent;
    private Vector3 moveDestination;
    public float detectionRadius;
    public Image imgTargetPoint;

    protected override void OnEnable()
    {
        base.OnEnable();
        agent = GetComponent<NavMeshAgent>();
        TeamTag = "TeamB";
        EquipWeapon(WeaponId.Hammer);
        EquipHat(HatId.Luffy);

    }


    protected override void Update()
    {
        base.Update();
    }
    public override void ChangeState(BehaviourState newState)
    {
        base.ChangeState(newState);
        if (agent != null && agent.isActiveAndEnabled)
        {
            agent.isStopped = (newState != BehaviourState.Run);
        }

    }



    protected override void UpdateIdle()
    {

        if (state == BehaviourState.Idle)
        {
            /// Khi đối thủ bước vào tầm đánh:
            /// Physics.OverlapSphere

            timerIdle += Time.deltaTime;
            if (timerIdle >= 5f)
            {
                moveDestination = GetRandomMovePosition();
                ChangeState(BehaviourState.Run);
                return;
            }
            else
            {

                Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, GameController.Instance.layerBody);


                for (int i = 0; i < colliders.Length; i++)
                {
                    if (GameController.Instance.currentPlayer.CompareTag(colliders[i].transform.root.tag))
                    {
                        float distanceToTarget = Vector3.Distance(transform.position, colliders[i].transform.position);
                        if (distanceToTarget <= rangeAttack)
                        {
                            RotateToward(GameController.Instance.currentPlayer);
                            ChangeState(BehaviourState.Attack);
                        }
                    }
                }
            }
            if (GameController.Instance.currentPlayer.currentHealth <= 0)
            {
                ChangeState(BehaviourState.Win);
            }
        }
    }
    protected override void BeginAttack()
    {
        base.BeginAttack();
        if (state == BehaviourState.Attack)
        {
            if (GameController.Instance.currentPlayer.state == BehaviourState.Dead)
            {
                ChangeState(BehaviourState.Idle);
            }
        }
    }
    private Vector3 GetRandomMovePosition()
    {
        Vector3 v = transform.position;


        float maxX = GameController.Instance.maxX.transform.position.x;
        float minX = -maxX;
        float maxZ = GameController.Instance.maxZ.transform.position.z;
        float minZ = -maxZ;

        Vector3 random = Random.insideUnitCircle;
        Vector3 t = new Vector3(v.x + random.x, v.y, v.z + random.y);
        Vector3 dir = (t - v).normalized;
        float distance = Random.Range(5f, 10f);

        Vector3 newPosition = v + dir * distance;
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);




        NavMeshHit hit;
        if (NavMesh.SamplePosition(newPosition, out hit, 5.0f, NavMesh.AllAreas))
        {
            Debug.LogFormat("position={0}, distance={1}", hit.position, Vector3.Distance(hit.position, transform.position));
            return hit.position;
        }


        /// Random một vị trí đi tuần thỏa mãn:
        /// - nằm trong giới hạn map (giới hạn X và Z)
        /// - position.Y ko đổi
        /// - nằm trong vùng navMesh có thể đi được (NavMesh.SamplePosition)
        /// - cách vị trí hiện tại X = Random từ 5->10 đơn vị (Random.insideCircle)

        return v;
    }



    protected override void BeginRun()
    {
        base.BeginRun();
        agent.SetDestination(moveDestination);
    }

    protected override void UpdateRun()
    {
        if (state == BehaviourState.Run)
        {
            float distance = Vector3.Distance(transform.position, moveDestination);
            if (distance <= 0.1f)
            {
                ChangeState(BehaviourState.Idle);
            }
        }
    }
    protected override void Dead()
    {
        base.Dead();
        if (this.state == BehaviourState.Dead)
        {
            GameController.Instance.enemies.Remove(this);
            if (GameController.Instance.enemies.Count == 0)
            {
                GameController.Instance.currentPlayer.ChangeState(BehaviourState.Idle);
                GameController.Instance.ShowPopupDropItem();
            }
            GameController.Instance.point += 1;
            GameController.Instance.UpdateScore();
        }
        CheckTargetPoint(false);
    }
    public void CheckTargetPoint(bool isTarget)
    {
        imgTargetPoint.gameObject.SetActive(isTarget);
        imgTargetPoint.color = Color.gray;
    }

}


