using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : SingletonMonoBehavior<ShopController>
{
    [SerializeField] private GameObject turretButtonPrefab;
    [SerializeField] private Transform shopPanel;
    
    [Header("Turret Data")]
    [SerializeField] private TurretData[] turrets;

    private void CreateTurretButton(TurretData turretData)
    {
        GameObject newButton = Instantiate(turretButtonPrefab, shopPanel.position, Quaternion.identity);
        newButton.transform.SetParent(shopPanel);
        newButton.transform.localScale = Vector3.one; // Insulates from certain odd behaviors with grid layout

        TurretButtonBehavior turretButtonBehavior = newButton.GetComponent<TurretButtonBehavior>();
        turretButtonBehavior.InitializeTurretButton(turretData);
    }

    private void Start()
    {
        for (int i = 0; i < turrets.Length; i++)
        {
            CreateTurretButton(turrets[i]);
        }
    }
}
