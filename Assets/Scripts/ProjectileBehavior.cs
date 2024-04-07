using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Eventually want projectiles to not seek, and persist to edge of map if they miss.
// Need to make sure I dont consider any inactive gameObjects in collision checks. 
// Not currently making that distinction bc doing so will cause projectiles to "stick" to location where gameObject was deactivated
// Will just leave as is until properly addressing this feature

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float damageDistance;

    // Set in ProjectileAttackBehavior
    public ProjectileAttackBehavior AttackOwner { get; set; }

    private EnemyBehavior targetEnemy;

    public void SetTargetEnemy(EnemyBehavior enemy)
    {
        targetEnemy = enemy;
    }

    private void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position,
            moveSpeed * Time.deltaTime);
        float distanceToTarget = (targetEnemy.transform.position - transform.position).magnitude;
        // Testing, need to reset targetEnemy if enemy dies so that it isnt targeted by hanging projectiles on coming out of the enemy pool
        if(!targetEnemy.gameObject.activeInHierarchy)
        {
            targetEnemy = null;
            ReturnToPool();
        }
        if(distanceToTarget < damageDistance)
        {
            // Will need to change this per TODO up top, but for now this will save me some headaches
            if(targetEnemy.gameObject.activeInHierarchy) targetEnemy.TakeDamage(damage);
            // AttackOwner.ResetAttack();
            ReturnToPool();
        }
    }
    
    private void RotateProjectile()
    {
        Vector3 enemyPos = targetEnemy.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }

    public void ResetProjectile()
    {
        targetEnemy = null;
        transform.localRotation = Quaternion.identity;
    }

    private void ReturnToPool()
    {
        gameObject.SetActive(false);
    }


    private void Update()
    {
        if(targetEnemy != null)
        {
            RotateProjectile();
            MoveProjectile();
        }
    }
}
