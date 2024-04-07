using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : MonoBehaviour
{
    [SerializeField] private float attackRange;

    public EnemyBehavior targetEnemy { get; set; }

    private List<EnemyBehavior> enemies;

    // TODO: Update this when I add different targeting modes
    private void SetTargetEnemy()
    {
        if(enemies.Count <= 0)
        {
            targetEnemy = null;
            return;
        }


        targetEnemy = enemies[0];
    }

    private void RotateTowardsTarget()
    {
        if (targetEnemy == null) return;

        Vector3 targetPos = targetEnemy.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, targetPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }


    private void OnDrawGizmos()
    {
        // Will need to have this logic whenever attackRange changes (e.g. upgrades)
        GetComponent<CircleCollider2D>().radius = attackRange;
        
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Start()
    {
        enemies = new List<EnemyBehavior>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyBehavior newEnemy = other.GetComponent<EnemyBehavior>();
            enemies.Add(newEnemy);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if(enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
            }
        }
    }

    private void Update()
    {
        SetTargetEnemy();
        RotateTowardsTarget();
    }
}
