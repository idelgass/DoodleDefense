using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will not have seeking projectile behavior, will instead follow a fixed vector
// Current ProjectileBehvaior class content will likely be moved to SeekingProjectileBehavior

public class VectoredProjectileBehavior : ProjectileBehavior
{

    [SerializeField] private float lifeTime;
    
    public Vector2 Direction {get; set;}

    private IEnumerator DelayedReturnToPool()
    {
        yield return new WaitForSeconds(lifeTime);
        gameObject.SetActive(false);
    }

    protected override void MoveProjectile()
    {
        Vector2 moveVector = Direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemy"))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if(enemy.Health > 0f)
            {
                enemy.TakeDamage(damage);
                RaiseOnHit(new ProjectileEventArgs(damage, enemy));
            }
            ReturnToPool();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(DelayedReturnToPool());
    }
}
