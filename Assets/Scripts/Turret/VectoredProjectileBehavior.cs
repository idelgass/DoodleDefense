using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class will not have seeking projectile behavior, will instead follow a fixed vector
// Current ProjectileBehvaior class content will likely be moved to SeekingProjectileBehavior

public class VectoredProjectileBehavior : ProjectileBehavior
{
    private Vector2 Direction {get; set;}

    protected override void MoveProjectile()
    {
        Vector2 moveVector = Direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(moveVector);
    }

    private void OnTriggerEnter2d(Collider2D other)
    {
        if(other.CompareTag("enemy"))
        {
            EnemyBehavior enemy = other.GetComponent<EnemyBehavior>();
            if(enemy.Health > 0f)
            {
                enemy.TakeDamage(damage);
            }
            ReturnToPool();
        }
    }
}
