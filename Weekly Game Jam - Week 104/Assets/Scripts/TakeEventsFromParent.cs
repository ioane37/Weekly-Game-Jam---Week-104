using UnityEngine;

public class TakeEventsFromParent : MonoBehaviour
{
    EnemyAI enemyAI;

    void Awake()
    {
        enemyAI = GetComponentInParent<EnemyAI>();
    }

    public void DealDamage()
    {
        enemyAI.DealDamage();
    }
}