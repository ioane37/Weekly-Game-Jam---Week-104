using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float viewDistance = 10f;
    [SerializeField] float attackRadius = 2.5f;
    [SerializeField] float chaseRadius = 5f;
    [SerializeField] float checkRadius = 2f;

    [SerializeField] float rotationSpeed = 10f;

    [SerializeField] float damageAmount = 30f;
    [SerializeField] float damageCooldown = 2f;

    [SerializeField] float dwellDuration = 1.5f;

    Transform patrolArea = null;
    Transform generator = null;

    NavMeshAgent agent = null;

    PlayerHealth player = null;

    float currentDamageCooldown;

    int patrolPointIndex = 0;

    RaycastHit hit; //TODO Use this to loose player

    enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Checking
    };

    State state = State.Patrolling;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        player = FindObjectOfType<PlayerHealth>();

        patrolArea = GameObject.FindWithTag("Patrol Area").transform;
        generator = GameObject.FindWithTag("Generator").transform;

        currentDamageCooldown = damageCooldown;
    }

    void Update()
    {
        FindPlayer();

        if (hit.transform != null)
            Debug.Log(hit.transform.name);

        if (state == State.Patrolling)
        {
            StartCoroutine(Patroll());
        }
        else if (state == State.Chasing)
        {
            Chase();
        }
        else if (state == State.Attacking)
        {
            Attack();
        }
        else
        {
            CheckGenerator();
        }
    }

    private void CheckGenerator()
    {
        FaceTo(generator.position);

        //TODO Playe Check Animation
        if (FacingTo(generator.position))
        {
            state = State.Patrolling;

            //OR Chasing or Attacking
        }

        CheckSetState();
    }

    private void FaceTo(Vector3 targetPos)
    {
        Quaternion targetRot = transform.rotation;
        targetRot.y = Quaternion.LookRotation(targetPos - transform.position).y;

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
            );
    }

    private void CheckSetState()
    {
        float distanceBetweeenPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceBetweeenPlayer <= chaseRadius && FacingTo(player.transform.position))
        {
            state = State.Chasing;
        }

        if (distanceBetweeenPlayer <= attackRadius && FacingTo(player.transform.position))
        {
            state = State.Attacking;
        }
    }

    private void Attack()
    {
        float distanceBetweeenPlayer = Vector3.Distance(transform.position, player.transform.position);

        bool canAttack = currentDamageCooldown - Time.time < 0 &&
                         player.Alive &&
                         distanceBetweeenPlayer <= attackRadius;

        bool facingToPlayer = FacingTo(player.transform.position);

        if (canAttack && facingToPlayer)
        {
            player.TakeDamage(damageAmount);

            currentDamageCooldown = Time.time + damageCooldown;
        }
        else if (!facingToPlayer)
        {
            FaceTo(player.transform.position);
        }

        if (distanceBetweeenPlayer <= chaseRadius)
        {
            state = State.Chasing;
        }
        else if (!FacingTo(player.transform.position))
        {
            state = State.Patrolling;
        }
    }

    private bool FacingTo(Vector3 targetPos)
    {
        return Vector3.Dot(transform.forward, (targetPos - transform.position).normalized) > 0.7f;
    }

    private bool FindPlayer()
    {
        return Physics.BoxCast(transform.position - Vector3.one, Vector3.one, transform.forward, out hit, Quaternion.identity, viewDistance);
    }

    private void Chase()
    {
        agent.SetDestination(player.transform.position);

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRadius)
        {
            state = State.Attacking;
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

    private IEnumerator Patroll()
    {
        CheckSetState();

        //Dwell
        if (Vector3.Distance(transform.position, patrolArea.GetChild(patrolPointIndex).position) <= agent.stoppingDistance)
        {
            if (Vector3.Distance(transform.position, generator.position) <= checkRadius)
            {
                state = State.Checking;
            }

            yield return new WaitForSecondsRealtime(dwellDuration);
        }

        agent.SetDestination(patrolArea.GetChild(GetTargetPatrolPointIndex()).position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Camera.main.transform.forward * viewDistance);

        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
}