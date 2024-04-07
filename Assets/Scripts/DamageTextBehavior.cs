using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DamageTextBehavior : MonoBehaviour
{
    public TextMeshProUGUI DmgText => GetComponentInChildren<TextMeshProUGUI>();

    public void ReturnToPool()
    {
        transform.SetParent(null);
        // Maybe should call to object pooler?
        gameObject.SetActive(false);
    }
}
