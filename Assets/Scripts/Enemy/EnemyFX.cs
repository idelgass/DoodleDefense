using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// Using a static event in ProjectileBehavior will cause all enemies to make the check if they == targetEnemy
// every time any enemy is hit. This can hardly be performant
public class EnemyFX : MonoBehaviour
{
    [SerializeField] private Transform damageTextPos;

    private EnemyBehavior enemy;

    private void EnemyHit(object sender, ProjectileEventArgs e)
    {
        if(enemy == e.TargetEnemy)
        {
            GameObject damageTextBehavior = DamageTextController.Instance.Pooler.GetInstFromPool();
            TextMeshProUGUI damageTextMesh = damageTextBehavior.GetComponent<DamageTextBehavior>().DmgTextMesh;
            damageTextMesh.text = e.Damage.ToString();

            // Set text to follow enemy
            damageTextBehavior.transform.SetParent(damageTextPos);
            damageTextBehavior.transform.position = damageTextPos.position;
            damageTextBehavior.SetActive(true);
        }
    }

    private void Start()
    {
        enemy = GetComponent<EnemyBehavior>();
    }

    private void OnEnable()
    {
        ProjectileBehavior.OnHit += EnemyHit;
    }

    private void OnDisable()
    {
        ProjectileBehavior.OnHit -= EnemyHit;
    }
}
