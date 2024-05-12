using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopCloseButtonBehavior : MonoBehaviour
{

    public void CloseButtonCLick()
    {
        UIController.Instance.CloseShop();
    }
}
