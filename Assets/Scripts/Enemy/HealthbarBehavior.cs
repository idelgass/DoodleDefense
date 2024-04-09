using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehavior : MonoBehaviour
{
    [SerializeField] public Image healthFillImage;

    public void UpdateHealthbar(float health, float maxHealth)
    {
        // Shouldn't need this if here but enemies were spawning with healthbars active
        // Think it had something to do with death but this seems to fix it
        if(health < maxHealth) gameObject.SetActive(true);
        healthFillImage.fillAmount = Mathf.Lerp(healthFillImage.fillAmount, 
            health/maxHealth, 2f);
    }

    public void ResetHealthbar()
    {
        healthFillImage.fillAmount = 100;
        gameObject.SetActive(false);
    }
}
