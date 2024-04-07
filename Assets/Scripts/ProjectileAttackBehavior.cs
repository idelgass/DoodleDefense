using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttackBehavior : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float attackDelay;

    private ObjectPooler pooler;
    private TurretBehavior turret;
    private ProjectileBehavior loadedProjectile;
    private float attackTimer;

    private void SpawnProjectile()
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



    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        turret = GetComponent<TurretBehavior>();
        attackTimer = 0f;
    }

    private void Update()
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
