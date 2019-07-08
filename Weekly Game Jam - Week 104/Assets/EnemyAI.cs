using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player = null;

    NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }

    private void FindPlayer()
    {

    }

    private void ChasePlayer()
    {
        
    }

    private void AttackPlayer()
    {

    }

    private void Patrol()
    {

    }
}