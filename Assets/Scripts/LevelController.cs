using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private int startingLife = 100;

    public int Life {get; set;}

    public void ReduceLives()
    {
        Life--;
        if(Life <= 0)
        {
            Life = 0;
        }
    }

    private void Start()
    {
        Life = startingLife;
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
