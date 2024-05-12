using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradePanelBehavior : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageButtonText;
    [SerializeField] private TextMeshProUGUI attackSpeedButtonText;
    public GameObject turret { get; set; }

    private UpgradeBehavior turretUpgradeBehavior;

    public void DamageUpgrade()
    {
        // Cost is handled in here
        turretUpgradeBehavior.IncreaseDamage();
        damageButtonText.text = turretUpgradeBehavior.DamageUpgradeCost.ToString();
    }

    public void AttackSpeedUpgrade()
    {
        // Cost is handled in here
        turretUpgradeBehavior.ReduceAttackDelay();
        attackSpeedButtonText.text = turretUpgradeBehavior.AttackSpeedUpgradeCost.ToString();
    }

    private void OnEnable()
    {
        turretUpgradeBehavior = turret.GetComponent<UpgradeBehavior>();
        damageButtonText.text = turretUpgradeBehavior.DamageUpgradeCost.ToString();
        attackSpeedButtonText.text = turretUpgradeBehavior.AttackSpeedUpgradeCost.ToString();
    }  
}
