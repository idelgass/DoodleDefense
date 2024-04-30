using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class VectoredAttackBehavior : ProjectileAttackBehavior
{   

    protected override void SpawnProjectile()
    {

    }

    private void FireProjectile(Vector3 direction)
    {
        GameObject projectile = pooler.GetInstFromPool();
        projectile.transform.position = spawnPosition.position;

        VectoredProjectileBehavior vectoredProjectileBehavior = projectile.GetComponent<VectoredProjectileBehavior>();
        vectoredProjectileBehavior.Direction = direction;
        projectile.SetActive(true);
    }

    protected override void Update()
    {
        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0 && turret.targetEnemy != null
            && turret.targetEnemy.Health > 0f && loadedProjectile == null)
        {
            attackTimer = attackDelay;
            Vector3 targetDirection = turret.targetEnemy.transform.position - transform.position;
            FireProjectile(targetDirection);
        }
    }

}
