using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Want to redo this class so it functions more like the homing sub darts from BTD

public class SeekingProjectileBehavior : ProjectileBehavior
{
    [SerializeField] private float damageDistance;

    protected override void MoveProjectile()
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
        else if(distanceToTarget < damageDistance)
        {
            // Will need to change this per TODO up top, but for now this will save me some headaches
            // if(targetEnemy.gameObject.activeInHierarchy) 
            targetEnemy.TakeDamage(Damage);
            RaiseOnHit(new ProjectileEventArgs(Damage, targetEnemy));
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

    protected override void Update()
    {
        if(targetEnemy != null)
        {
            RotateProjectile();
            MoveProjectile();
        }
    }
}
