using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class TurretButtonEventArgs : EventArgs
{
    public TurretData LoadedTurret { get; }
    public TurretButtonEventArgs(TurretData loadedTurret)
    {
        LoadedTurret = loadedTurret;
    }
}

public class TurretButtonBehavior : MonoBehaviour
{
    [SerializeField] private Image turretIcon;
    [SerializeField] private TextMeshProUGUI turretCost;

    public static EventHandler<TurretButtonEventArgs> OnBuyTurret;

    public TurretData LoadedTurret { get; set; }

    public void InitializeTurretButton(TurretData turretData)
    {
        LoadedTurret = turretData;
        turretIcon.sprite = turretData.turretIcon;
        turretCost.text = turretData.turretCost.ToString();
    }

    public void BuyTurret()
    {
        if(CurrencyController.Instance.TotalCoins >= LoadedTurret.turretCost)
        {
            CurrencyController.Instance.RemoveCoins(LoadedTurret.turretCost);
            BuildController.Instance.ActivateCursor(LoadedTurret);
            UIController.Instance.CloseShop();
            // OnBuyTurret?.Invoke(this, new TurretButtonEventArgs(LoadedTurret));
        }
        Debug.Log("button click");
    }
}
