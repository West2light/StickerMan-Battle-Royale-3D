using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : Character
{
    private NavMeshAgent agent;
    private Vector3 moveDestination;
    public float detectionRadius = 5f;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        TeamTag = "TeamB";
    }
    //protected override void FixedUpdate()
    //{
    //    base.FixedUpdate();
    //    RotateToward(GameController.Instance.player);
    //}
    public override void ChangeState(BehaviourState newState)
    {
        base.ChangeState(newState);

        agent.isStopped = (newState != BehaviourState.Run);
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
                    if (GameController.Instance.player.CompareTag(colliders[i].transform.root.tag))
                    {
                        Debug.Log("PlayerPosition=" + GameController.Instance.player.transform.position);
                        Vector3 direction = (GameController.Instance.player.transform.position - transform.position).normalized;
                        Debug.LogFormat("direction = {0}", direction);
                        float angle = Vector3.Angle(transform.position, direction);

                        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                        Debug.LogFormat("rotation = {0}, angle = {1}", rotation, angle);
                        transform.rotation = rotation;

                        ChangeState(BehaviourState.Attack);
                        return;
                    }
                }
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


        //float distance = Random.Range(5f, 10f);
        //Vector2 randomCircle = Random.insideUnitCircle * distance;

        //float v_RandomX = Mathf.Clamp(v.x + randomCircle.x, minX, maxX);
        //float v_RandomZ = Mathf.Clamp(v.z + randomCircle.y, minZ, maxZ);

        //Vector3 newPosition = new Vector3(v_RandomX, transform.position.y, v_RandomZ);
        //Debug.LogFormat("randomPos={0}, distance={1}", newPosition, Vector3.Distance(newPosition, transform.position));

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
}


