using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnModes
{
    Standard,
    Double,
}

public class SpawnBehavior : MonoBehaviour
{
    [SerializeField] private SpawnModes spawnMode = SpawnModes.Standard;
    [SerializeField] private GameObject testEnemy;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int waveSize;
    [SerializeField] private float waveInterval;

    private float spawnTimer;
    private int enemiesRemaining;
    private int enemiesSpawned;
    private ObjectPooler pooler;
    private PathBehavior pathBehavior;

    private void SpawnEnemy(){
        GameObject newEnemy = pooler.GetInstFromPool();
        // Get reference to EnemyBehavior script on the specific enemy being taken from pool
        // Use this to set enemy's path to the ref stored on this game obj
        EnemyBehavior newEnemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        newEnemyBehavior.PathBehavior = pathBehavior;
        // Move enemy to location of first waypoint, might need local position on lhs
        newEnemyBehavior.transform.position = pathBehavior.Waypoints[0];

        newEnemy.SetActive(true);
    }

    private IEnumerator NextWave()
    {
        yield return new WaitForSeconds(waveInterval);
        enemiesRemaining = waveSize;
        spawnTimer = 0f;
        enemiesSpawned = 0;
    }

    private void RecordEnemy()
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            StartCoroutine(NextWave());
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        pathBehavior = GetComponent<PathBehavior>();

        enemiesRemaining = waveSize;
    }

    private void OnEnable()
    {
        EnemyBehavior.OnEndReached += RecordEnemy;
        EnemyBehavior.OnDeath += RecordEnemy;
    }
    
    private void OnDisable()
    {
        EnemyBehavior.OnEndReached -= RecordEnemy;
        EnemyBehavior.OnDeath -= RecordEnemy;
    }

    // Update is called once per frame
    private void Update()
    {
        if(enemiesSpawned < waveSize)
        {
            spawnTimer -= Time.deltaTime;
            if(spawnTimer <= 0)
            {
                spawnTimer = spawnInterval;
                enemiesSpawned++;
                SpawnEnemy();
            }
        }
    }
}
