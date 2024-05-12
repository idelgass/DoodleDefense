using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtonBehavior : MonoBehaviour
{

    public void ShopButtonCLick()
    {
        UIController.Instance.OpenShop();
    }
}
