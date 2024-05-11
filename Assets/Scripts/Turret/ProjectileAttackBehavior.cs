using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Feels weird not having this be abstract

public class ProjectileAttackBehavior : MonoBehaviour
{
    [SerializeField] protected Transform spawnPosition;
    [SerializeField] protected float attackDelay;
    [SerializeField] protected float damage;

    protected ObjectPooler pooler;
    protected TurretBehavior turret;
    protected ProjectileBehavior loadedProjectileBehavior;
    protected float attackTimer;

    public float Damage { get; set; }
    public float AttackDelay { get; set; }

    protected virtual void SpawnProjectile()
    {
        GameObject newProjectile = pooler.GetInstFromPool();
        // position or local position on lhs?
        newProjectile.transform.position = spawnPosition.position;
        // Need projectile to rotate with turret
        newProjectile.transform.SetParent(spawnPosition);

        loadedProjectileBehavior = newProjectile.GetComponent<ProjectileBehavior>();
        loadedProjectileBehavior.AttackOwner = this;
        loadedProjectileBehavior.ResetProjectile();
        loadedProjectileBehavior.Damage = damage;
        
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
        Damage = damage;
        AttackDelay = attackDelay;
    }

    protected virtual void Update()
    {
        attackTimer -= Time.deltaTime;
        if(attackTimer <= 0 && turret.targetEnemy != null
            && turret.targetEnemy.Health > 0f && loadedProjectileBehavior == null)
        {
            attackTimer = AttackDelay;
            SpawnProjectile();
            // if (loadedProjectile == null) SpawnProjectile();
            // Releasing projectile from its parent
            loadedProjectileBehavior.transform.parent = null;
            loadedProjectileBehavior.SetTargetEnemy(turret.targetEnemy);
            loadedProjectileBehavior = null;
        }

        
    }
}
