using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCloseButtonBehavior : MonoBehaviour
{
    public void UpgradeCloseButtonClick()
    {
        UIController.Instance.CloseUpgrade();
    }
}
