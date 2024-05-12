using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseButtonBehavior : MonoBehaviour
{

    public void CloseButtonCLick()
    {
        UIController.Instance.CloseShop();
    }
}
