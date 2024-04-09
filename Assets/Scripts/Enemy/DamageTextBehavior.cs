using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageTextBehavior : MonoBehaviour
{
    public TextMeshProUGUI DmgTextMesh => GetComponentInChildren<TextMeshProUGUI>();
    
    private EnemyBehavior enemy;

    public void ReturnToPool()
    {
        transform.SetParent(null);
        // Maybe should call to object pooler?
        gameObject.SetActive(false);
    }

    // TODO: Added below to try and fix damage numbers triggering on enemy respawn
    // Ended up getting a bunch of GameObject null ref errors, need to fix this
    // Shouldnt the animations already be deactivated if their parent obj is deactivated?
    // Doesnt make sense to me

    // private void Start()
    // {
    //     // This smells, but I need to ReturnToPool when enemy dies or
    //     // the animation can trigger when enemy is respawned from the pool
    //     enemy = transform.parent.parent.GetComponent<EnemyBehavior>();
    // }

    // private void OnEnable()
    // {
    //     enemy.OnThisDeath += ReturnToPool;
    // }

    // private void OnDisable()
    // {
    //     enemy.OnThisDeath -= ReturnToPool;
    // }
}
