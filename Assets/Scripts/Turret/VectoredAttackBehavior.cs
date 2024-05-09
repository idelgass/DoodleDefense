using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class VectoredAttackBehavior : ProjectileAttackBehavior
{   

    [Header("Spread")]
    [SerializeField] private bool hasSpread;
    [SerializeField] private float spreadAmount;

    protected override void SpawnProjectile()
    {

    }

    private void FireProjectile(Vector3 direction)
    {
        GameObject projectile = pooler.GetInstFromPool();
        projectile.transform.position = spawnPosition.position;

        VectoredProjectileBehavior vectoredProjectileBehavior = projectile.GetComponent<VectoredProjectileBehavior>();
        vectoredProjectileBehavior.Direction = direction;

        if(hasSpread)
        {
            float randomSpread = Random.Range(-spreadAmount, spreadAmount);
            Vector3 spread = new Vector3(0f, 0f, randomSpread);
            Quaternion spreadRot = Quaternion.Euler(spread);
            Vector2 spreadDirection = spreadRot * direction;
            vectoredProjectileBehavior.Direction = spreadDirection;

        }

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
