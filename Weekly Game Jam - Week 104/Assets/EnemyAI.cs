using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float viewDistance = 10f;
    [SerializeField] float attackRadius = 2.5f;
    [SerializeField] float chaseRadius = 5f;
    [SerializeField] float checkRadius = 2f;

    [SerializeField] float rotationSpeed = 100f;

    [SerializeField] float damageAmount = 30f;
    [SerializeField] float damageCooldown = 2f;

    [SerializeField] float dwellDuration = 1.5f;

    Transform patrolArea = null;

    NavMeshAgent agent = null;

    PlayerHealth player = null;

    float currentDamageCooldown;

    bool foundPlayer = false;
    bool inChase = false;

    int patrolPointIndex = 0;

    bool canPatrol = true;

    bool coroutineRunning = false;

    bool canCheckGenerator = false;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerHealth>();

        patrolArea = GameObject.FindWithTag("Patrol Area").transform;

        currentDamageCooldown = damageCooldown;
    }

    void Update()
    {
        FindPlayer();

        if (foundPlayer)
            inChase = true;

        if(inChase && Vector3.Distance(transform.position, player.transform.position) <= chaseRadius)
        {
            if (!foundPlayer)
            {
                transform.rotation = Quaternion.RotateTowards(
                                     transform.rotation,
                                     Quaternion.LookRotation(player.transform.position - transform.position),
                                     rotationSpeed * Time.deltaTime
                                     );
            }

            agent.SetDestination(player.transform.position);
        }
        else
        {
            inChase = false;
        }

        //Patrol
        if (!foundPlayer && !inChase && canPatrol)
        {
            if (!coroutineRunning && Vector3.Distance(transform.position, patrolArea.GetChild(patrolPointIndex).position) <= agent.stoppingDistance)
            {
                StartCoroutine(Dwell());
            }

            agent.SetDestination(patrolArea.GetChild(GetTargetPatrolPointIndex()).position);
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRadius && foundPlayer &&
            player.Alive && currentDamageCooldown - Time.time < 0)
        {
            player.TakeDamage(damageAmount);

            currentDamageCooldown = Time.time + damageCooldown;
        }
    }

    private int GetTargetPatrolPointIndex()
    {
        if (Vector3.Distance(transform.position, patrolArea.GetChild(patrolPointIndex).position) > agent.stoppingDistance) 
        {
            return patrolPointIndex;
        }

        if (patrolPointIndex + 1 == patrolArea.childCount)
        {
            patrolPointIndex = 0;

            return 0;
        }

        return patrolPointIndex++;
    }

    private void FindPlayer()
    {
        foundPlayer = Physics.BoxCast(transform.position - Vector3.one, Vector3.one, transform.forward, Quaternion.identity, viewDistance, LayerMask.GetMask("Player"));
    }

    private IEnumerator Dwell()
    {
        canPatrol = false;
        coroutineRunning = true;

        yield return new WaitForSecondsRealtime(dwellDuration);

        canPatrol = true;
        coroutineRunning = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Camera.main.transform.forward * viewDistance);

        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}