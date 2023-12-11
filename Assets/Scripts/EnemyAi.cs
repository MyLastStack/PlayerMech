using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] MechHealth player;

    int maxHP;
    int dmg = 10;
    bool alreadyHit = false;

    DamageableScript dmgScript;
    Rigidbody rb;

    public NavMeshAgent agent;
    public float range;

    public Transform centrePoint;


    void Start()
    {
        dmgScript = GetComponent<DamageableScript>();
        rb = GetComponent<Rigidbody>();

        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3.5f;

        maxHP = dmgScript.health;
    }

    void Update()
    {
        if (dmgScript.health == maxHP)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                Vector3 point;
                if (RandomPoint(centrePoint.position, range, out point))
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                    agent.SetDestination(point);
                }
            }
        }
        else
        {
            agent.SetDestination(player.transform.position);
            agent.speed = 50f;
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        MechHealth mh = collision.gameObject.GetComponent<MechHealth>();
        if (mh != null && !alreadyHit)
        {
            mh.TakeDamage(dmg);
            alreadyHit = true;
            gameObject.SetActive(false);
        }
    }
}
