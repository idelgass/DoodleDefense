using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : SingletonMonoBehavior<LevelController>
{
    [SerializeField] private int startingLife = 100;
    [SerializeField] private GameObject spawner;

    public int Life {get; set;}
    public int WaveNumber { get; set; }
    public bool IsWaveActive => isWaveActive;

    private SpawnBehavior spawnBehavior;
    private bool isWaveActive = false;

    public void ReduceLives(object sender, EnemyEventArgs e)
    {
        Life -= e.Enemy.Lives;
        if(Life <= 0)
        {
            Life = 0;
        }
    }

    public void StartWave()
    {
        UIController.Instance.DeactivateWaveTutorial(); // Temp
        if(!isWaveActive)
        {
            spawnBehavior.NextWave();
            isWaveActive = true;        
        }
    }

    public void EndWave()
    {
        if(isWaveActive)
        {
            isWaveActive = false;
            UIController.Instance.UpdateWave(++WaveNumber);
        }
    }

    private void Start()
    {
        Life = startingLife;
        WaveNumber = 1;
        spawnBehavior = spawner.GetComponent<SpawnBehavior>();
    }

    private void OnEnable()
    {
        EnemyBehavior.OnEndReached += ReduceLives;
    }
    private void OnDisable()
    {
        EnemyBehavior.OnEndReached += ReduceLives;
    }

}
