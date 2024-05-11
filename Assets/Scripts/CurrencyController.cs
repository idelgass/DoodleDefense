using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : SingletonMonoBehavior<CurrencyController>
{
    [SerializeField] private int initialCoins;

    // Private string CURRENCY_SAVE_KEY = "PLACEHOLDER_KEY";
    public int TotalCoins { get; set; }

    public void AddCoins(int amountToAdd)
    {
        TotalCoins += amountToAdd;
    }

    public void RemoveCoins(int amountToRemove)
    {
        // Will want to check whether player can afford anything before purchasing, so technically
        // this ought never happen. Keeping this check here anyway just as insurance
        if(TotalCoins >= amountToRemove) TotalCoins -= amountToRemove;
        else TotalCoins = 0;
    }

    private void Start()
    {
        TotalCoins = initialCoins;
    }
}
