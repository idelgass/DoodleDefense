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
    // [SerializeField] private GameObject testEnemy;
    [SerializeField] private float spawnInterval;
    [SerializeField] private int waveSize;

    [Header("Enemy Type Settings")]
    [SerializeField] private int mediumSpawnThreshold; 
    [SerializeField] private int heavySpawnThreshold;
    [SerializeField] private ObjectPooler basicPooler;
    [SerializeField] private ObjectPooler medPooler;
    [SerializeField] private ObjectPooler heavyPooler;

    

    private float spawnTimer;
    private int enemiesRemaining;
    private int enemiesSpawned;
    private ObjectPooler activePooler;
    private PathBehavior pathBehavior;

    private int[] waveSizes = new int[]
    {
        50, // Default
        5, 10, 15, 20,
        5, 10, 15, 20,
        5, 10, 15, 20,
    };
    

    private void SpawnEnemy(){
        if(LevelController.Instance.WaveNumber >= mediumSpawnThreshold) activePooler = medPooler;
        if(LevelController.Instance.WaveNumber >= heavySpawnThreshold) activePooler = heavyPooler;
        GameObject newEnemy = activePooler.GetInstFromPool();
        // Get reference to EnemyBehavior script on the specific enemy being taken from pool
        // Use this to set enemy's path to the ref stored on this game obj
        EnemyBehavior newEnemyBehavior = newEnemy.GetComponent<EnemyBehavior>();
        newEnemyBehavior.PathBehavior = pathBehavior;
        // Move enemy to location of first waypoint, might need local position on lhs
        newEnemyBehavior.transform.position = pathBehavior.Waypoints[0];

        newEnemy.SetActive(true);
    }

    public void NextWave()
    {
        if(LevelController.Instance.WaveNumber < waveSizes.Length)
        {
            waveSize = waveSizes[LevelController.Instance.WaveNumber];
        }
        else
        {
            waveSize = waveSizes[0];
        } 
        enemiesRemaining = waveSize;
        spawnTimer = 0f;
        enemiesSpawned = 0;
    }

    private void WaveEnd()
    {
        LevelController.Instance.EndWave();
    }

    private void RecordEnemy(object sender, EnemyEventArgs e)
    {
        enemiesRemaining--;
        if (enemiesRemaining <= 0)
        {
            WaveEnd();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        activePooler = basicPooler;
        pathBehavior = GetComponent<PathBehavior>();
        enemiesRemaining = 0;
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
        if(LevelController.Instance.IsWaveActive && enemiesSpawned < waveSize)
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
