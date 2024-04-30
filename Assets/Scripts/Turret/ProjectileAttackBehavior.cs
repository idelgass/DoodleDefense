using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Feels weird not having this be abstract

public class ProjectileAttackBehavior : MonoBehaviour
{
    [SerializeField] protected Transform spawnPosition;
    [SerializeField] protected float attackDelay;

    protected ObjectPooler pooler;
    protected TurretBehavior turret;
    protected ProjectileBehavior loadedProjectile;
    protected float attackTimer;

    protected virtual void SpawnProjectile()
    {
        GameObject newProjectile = pooler.GetInstFromPool();
        // position or local position on lhs?
        newProjectile.transform.position = spawnPosition.position;
        // Need projectile to rotate with turret
        newProjectile.transform.SetParent(spawnPosition);

        loadedProjectile = newProjectile.GetComponent<ProjectileBehavior>();
        loadedProjectile.AttackOwner = this;
        loadedProjectile.ResetProjectile();
        
        newProjectile.SetActive(true);
    }

    // Was called in projectile behaviors
    // public void ResetAttack()
    // {
    //     loadedProjectile = null;
    // }



    protected void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        turret = GetComponent<TurretBehavior>();
        attackTimer = 0f;
    }

    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0 && turret.targetEnemy != null
            && turret.targetEnemy.Health > 0f && loadedProjectile == null)
        {
            attackTimer = attackDelay;
            SpawnProjectile();
            // if (loadedProjectile == null) SpawnProjectile();
            // Releasing projectile from its parent
            loadedProjectile.transform.parent = null;
            loadedProjectile.SetTargetEnemy(turret.targetEnemy);
            loadedProjectile = null;
        }

        
    }
}
