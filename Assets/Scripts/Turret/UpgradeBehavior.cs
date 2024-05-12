using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBehavior : MonoBehaviour
{
    [SerializeField] private int initialCost;
    [SerializeField] private float costModifier;
    [SerializeField] private float damageIncrement;
    [SerializeField] private float delayReduce;

    public int DamageUpgradeCost { get; set; }
    public int AttackSpeedUpgradeCost { get; set; }

    private ProjectileAttackBehavior projectileAttackBehavior;

    public void TurretClick()
    {
        UIController.Instance.OpenUpgrade(gameObject);
    }

    public void IncreaseDamage()
    {
        if(CurrencyController.Instance.TotalCoins >= DamageUpgradeCost)
        {
            projectileAttackBehavior.Damage += damageIncrement;
            // Don't ever want a negative attack delay, think this would stop turret from shooting at all
            // May actually have a catch for this in the attack behavior but better safe than sorry
            CurrencyController.Instance.RemoveCoins(DamageUpgradeCost);
            DamageUpgradeCost = (int)(DamageUpgradeCost * costModifier);
        }
    }

    public void ReduceAttackDelay()
    {
        if(CurrencyController.Instance.TotalCoins >= AttackSpeedUpgradeCost)
        {
            // Don't ever want a negative attack delay, think this would stop turret from shooting at all
            // May actually have a catch for this in the attack behavior but better safe than sorry
            if(projectileAttackBehavior.AttackDelay >= delayReduce) projectileAttackBehavior.AttackDelay -= delayReduce;
            else projectileAttackBehavior.AttackDelay = 0;
            CurrencyController.Instance.RemoveCoins(AttackSpeedUpgradeCost);
            AttackSpeedUpgradeCost = (int)(AttackSpeedUpgradeCost * costModifier);
        }
    }

    // private void UpgradeTurret()
    // {
    //     if(CurrencyController.Instance.TotalCoins >= UpgradeCost)
    //     {
    //         projectileAttackBehavior.Damage += damageIncrement;
    //         // Don't ever want a negative attack delay, think this would stop turret from shooting at all
    //         // May actually have a catch for this in the attack behavior but better safe than sorry
    //         if(projectileAttackBehavior.AttackDelay >= delayReduce) projectileAttackBehavior.AttackDelay -= delayReduce;
    //         else projectileAttackBehavior.AttackDelay = 0;
    //         CurrencyController.Instance.RemoveCoins(UpgradeCost);
    //         UpgradeCost = (int)(UpgradeCost * costModifier);
    //     }
    // }


    private void Start()
    {
        projectileAttackBehavior = GetComponent<ProjectileAttackBehavior>();
        DamageUpgradeCost = initialCost; 
        AttackSpeedUpgradeCost = initialCost;
    }


    // private void Update()
    // {
    //     if(Input.GetKeyDown(KeyCode.D))
    //     {
    //         UpgradeTurret();
    //     }
    // }

}
