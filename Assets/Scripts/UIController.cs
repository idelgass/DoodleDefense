using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonMonoBehavior<UIController>
{
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject upgradeContainer;
    [SerializeField] private TextMeshProUGUI lifeTotal;
    [SerializeField] private TextMeshProUGUI coinTotal;
    [SerializeField] private TextMeshProUGUI waveCount;
    [SerializeField] private GameObject waveTutorial;
    [SerializeField] private GameObject shopTutorial;

    public void OpenShop()
    {
        shopTutorial.SetActive(false); // Temp
        shopContainer.SetActive(true);
        shopButton.SetActive(false);
    }

    public void CloseShop()
    {
        shopContainer.SetActive(false);
        shopButton.SetActive(true);
    }

    public void OpenUpgrade(GameObject turret)
    {
        UpgradePanelBehavior upgradePanelBehavior = upgradeContainer.GetComponentInChildren<UpgradePanelBehavior>();
        upgradePanelBehavior.turret = turret;
        upgradeContainer.SetActive(true);
        shopContainer.SetActive(false);
        shopButton.SetActive(false);
    }

    public void CloseUpgrade()
    {
        upgradeContainer.SetActive(false);
        shopButton.SetActive(true);
    }

    public void UpdateWave(int WaveNumber)
    {
        Debug.Log(WaveNumber);
        waveCount.text = WaveNumber.ToString();
    }

    public void DeactivateWaveTutorial() // Temp
    {
        waveTutorial.SetActive(false);
    }
    

    private void Update()
    {
        coinTotal.text = CurrencyController.Instance.TotalCoins.ToString();
        lifeTotal.text = LevelController.Instance.Life.ToString();
    }
}