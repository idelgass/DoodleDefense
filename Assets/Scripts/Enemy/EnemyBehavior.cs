using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.Events;

public class EnemyEventArgs : EventArgs
{
    public EnemyBehavior Enemy { get; }

    public EnemyEventArgs(EnemyBehavior enemyBehavior)
    {
        Enemy = enemyBehavior;
    }
}

public class EnemyBehavior : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private int coins;
    [SerializeField] private int lives;

    [Header("Healthbar")]
    [SerializeField] private GameObject healthbarPrefab;
    [SerializeField] private Transform barTransform;

    // Set in SpawnBehavior
    public PathBehavior PathBehavior {get; set;}
    public int Coins => coins;
    public int Lives => lives;     
    public Vector3 CurrPointPos => PathBehavior.GetWaypointPos(waypointIndex);
    public float Health => health;
    public static event EventHandler<EnemyEventArgs> OnDeath;
    public static event EventHandler<EnemyEventArgs> OnEndReached;
    private HealthbarBehavior healthbarBehavior;
    // public UnityEvent OnEndReached;
    


    private int waypointIndex;

    private void CreateHealthBar()
    {
        GameObject healthbar = Instantiate(healthbarPrefab, barTransform.position, Quaternion.identity);
        healthbar.transform.SetParent(transform);
        healthbar.SetActive(false);
        
        // Consider using event handler with event args instead of storing a direct ref
        healthbarBehavior = healthbar.GetComponent<HealthbarBehavior>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            health = 0;
            Die();
        }
        healthbarBehavior.UpdateHealthbar(health, maxHealth);
    }


    private void Move()
    {
        transform.position = Vector3.MoveTowards(current:transform.position, target:CurrPointPos, 
            maxDistanceDelta:moveSpeed*Time.deltaTime);
    }

    private bool WayPointReached()
    {
        float distToNextPoint = (transform.position - CurrPointPos).magnitude;
        if(distToNextPoint < 0.1f)
        {
            return true;
        }
        return false;
    }

    // Enemies were taking damage when inactive, consider reset OnEnable
    private void ResetEnemy()
    {
        waypointIndex = 0;
        health = maxHealth;
        healthbarBehavior.ResetHealthbar();
    }

    // May want to move this method to ObjectPooler
    private void ReturnToPool()
    {
        ResetEnemy();
        gameObject.SetActive(false);
    }

    private void Die()
    {
        ReturnToPool();
        OnDeath?.Invoke(this, new EnemyEventArgs(this)); // Null check before invoking
    }

    private void UpdateWaypointIndex()
    {
        if(waypointIndex < PathBehavior.Waypoints.Length - 1)
        {
            waypointIndex++;
        }
        else
        {
            // if(OnEndReached != null) OnEndReached();
            OnEndReached?.Invoke(this, new EnemyEventArgs(this)); 
            ReturnToPool();
        }
    }

    private void Start()
    {
        waypointIndex = 0;
        health = maxHealth;
        CreateHealthBar();
    }

    // private void OnEnable()
    // {
    //     // TODO: This is redundant, alreeady calling reset.
    //     // Might be good practice to be safe though
    //     ResetEnemy();
    // }

    private void Update()
    {
        Move();
        if(Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(2f);
        }
        if(WayPointReached())
        {
            UpdateWaypointIndex();
        }
    }
}
