using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIController : SingletonMonoBehavior<UIController>
{
    [SerializeField] private GameObject shopContainer;
    [SerializeField] private GameObject shopButton;
    [SerializeField] private GameObject upgradeContainer;

    public void OpenShop()
    {
        shopContainer.SetActive(true);
        shopButton.SetActive(false);
    }

    public void CloseShop()
    {
        shopContainer.SetActive(false);
        shopButton.SetActive(true);
    }

    public void OpenUpgrade()
    {
        upgradeContainer.SetActive(true);
        shopContainer.SetActive(false);
        shopButton.SetActive(false);
    }

    public void CloseUpgrade()
    {
        upgradeContainer.SetActive(false);
        shopButton.SetActive(true);
    }
}