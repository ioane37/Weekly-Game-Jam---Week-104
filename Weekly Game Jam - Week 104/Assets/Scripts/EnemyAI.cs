﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float viewDistance = 1000f;
    [SerializeField] float attackRadius = 2.5f;
    [SerializeField] float chaseRadius = 8f;
    [SerializeField] float checkRadius = 3f;

    [SerializeField] float rotationSpeed = 10f;

    [SerializeField] float damageAmount = 30f;
    [SerializeField] float damageCooldown = 2f;

    [SerializeField] float dwellDuration = 1.5f;

    Transform patrolArea = null;
    Transform generator = null;

    NavMeshAgent agent = null;

    Animator animator = null;

    PlayerHealth player = null;

    float currentDamageCooldown;

    int patrolPointIndex = 0;

    bool playerFound = false;

    RaycastHit hit; //TODO Use this to loose player

    enum State
    {
        Patrolling,
        Chasing,
        Attacking,
        Checking,
        LookingForPlayer
    };

    State state = State.Patrolling;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();

        player = FindObjectOfType<PlayerHealth>();

        patrolArea = GameObject.FindWithTag("Patrol Area").transform;
        generator = GameObject.FindWithTag("Generator").transform;

        currentDamageCooldown = damageCooldown;
    }

    void Update()
    {
        if (state == State.Patrolling)
        {
            StartCoroutine(Patroll());
        }
        else if (state == State.Chasing)
        {
            Chase();
            AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Chasing);
        }
        else if (state == State.Attacking)
        {
            Attack();
        }
        else if(state == State.Checking)
        {
            CheckGenerator();
        }
        else
        {
            Vector3 targetDir = player.transform.position - transform.position;

            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);

            CheckSetState();
        }
    }

    void FixedUpdate()
    {
        FoundPlayer();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player Collider") && !playerFound && !FacingTo(player.transform.position))
        {
            animator.SetTrigger("idle");

            state = State.LookingForPlayer;
        }
    }

    public void DealDamage()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > chaseRadius)
            return;

        player.TakeDamage(damageAmount);

        currentDamageCooldown = Time.time + damageCooldown;
    }

    private void FoundPlayer()
    {
        Vector3 direction = player.transform.position - transform.position;

        if (state != State.Chasing)
            direction = transform.forward;

        RaycastHit[] raycastHits = Physics.RaycastAll(transform.position, direction, viewDistance);

        int playerIndex = -1;
        int coverIndex = -1;

        for (int i = 0; i < raycastHits.Length; i++)
        {
            Transform hitTranform = raycastHits[i].transform;

            if (hitTranform.CompareTag("Cover"))
                coverIndex = i;
            if (hitTranform.CompareTag("Player"))
                playerIndex = i;
        }

        if(playerIndex == -1)
        {
            playerFound = false;

            return;
        }

        bool playerNotNear = Vector3.Distance(transform.position, player.transform.position) > chaseRadius;

        if (coverIndex < playerIndex && playerNotNear && coverIndex != -1)
        {
            state = State.Patrolling;

            AudioManager.Instance.ChangeMusic(AudioManager.SoundType.Background);

            playerFound = false;
        }
        else
        {
            playerFound = true;
        }
    }

    private void CheckGenerator()
    {
        FaceTo(generator.position);

        //TODO Play Check Animation
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

        if (playerFound)
        {
            state = State.Chasing;

            StopAllCoroutines();
        }

        if (distanceBetweeenPlayer <= attackRadius && playerFound)
        {
            state = State.Attacking;

            StopAllCoroutines();
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
            animator.SetTrigger("attack");
        }
        else if (!facingToPlayer)
        {
            FaceTo(player.transform.position);

            animator.SetTrigger("idle");
        }
        else
            animator.SetTrigger("idle");

        if (distanceBetweeenPlayer > attackRadius && distanceBetweeenPlayer <= chaseRadius)
        {
            state = State.Chasing;
        }
        else if (distanceBetweeenPlayer > chaseRadius && !FacingTo(player.transform.position))
        {
            state = State.Patrolling;
        }
    }

    private bool FacingTo(Vector3 targetPos)
    {
        return Vector3.Dot(transform.forward, (targetPos - transform.position).normalized) > 0.7f;
    }

    private void Chase()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= attackRadius && playerFound)
        {
            state = State.Attacking;
        }

        agent.SetDestination(player.transform.position);

        animator.SetTrigger("walk");
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

            animator.SetTrigger("idle");

            yield return new WaitForSecondsRealtime(dwellDuration);
        }

        agent.SetDestination(patrolArea.GetChild(GetTargetPatrolPointIndex()).position);

        animator.SetTrigger("walk");    
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