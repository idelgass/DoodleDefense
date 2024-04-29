using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

// TODO: Eventually want projectiles to not seek, and persist to edge of map if they miss.
// Need to make sure I dont consider any inactive gameObjects in collision checks. 
// Not currently making that distinction bc doing so will cause projectiles to "stick" to location where gameObject was deactivated
// Will just leave as is until properly addressing this feature

// TODO: Want to move responsibility for damage amount to TurretBehavior, dont want communicate back and forth over upgrades and damage bonuses

public class ProjectileEventArgs : EventArgs
{
    public float damage {get;}
    public EnemyBehavior targetEnemy {get;}
    public ProjectileEventArgs(float damage, EnemyBehavior targetEnemy)
    {
        this.damage = damage;
        this.targetEnemy = targetEnemy;
    }
}

public abstract class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float damage;
    // [SerializeField] protected float damageDistance;

    // Set in ProjectileAttackBehavior
    public ProjectileAttackBehavior AttackOwner { get; set; }
    public static event EventHandler<ProjectileEventArgs> OnHit;

    protected EnemyBehavior targetEnemy;

    public void SetTargetEnemy(EnemyBehavior enemy)
    {
        targetEnemy = enemy;
    }

    public void ResetProjectile()
    {
        targetEnemy = null;
        transform.localRotation = Quaternion.identity;
    }

    protected void RaiseOnHit(ProjectileEventArgs e)
    {
        if(OnHit != null) OnHit(this, e);
    }

    protected void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    protected abstract void MoveProjectile();


    protected virtual void Update()
    {
        if(targetEnemy != null)
        {
            MoveProjectile();
        }
    }
}
