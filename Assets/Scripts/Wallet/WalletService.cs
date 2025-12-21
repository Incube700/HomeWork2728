using System;
using UnityEngine;

public class WalletService : MonoBehaviour
{
    // Событие - меняем валюту
    //1 - тип валюты 
    //2 - новое количество 

    public event Action<CurrencyType, int> CurrencyChanged;

    [SerializeField] private int _coins;
    [SerializeField] private int _gems;
    [SerializeField] private int _energy;

    public int GetAmount(CurrencyType type)
    {
        //вернём значние по типу 
        switch (type)
        {
            case CurrencyType.Coins:
                return _coins;

            case CurrencyType.Gems:
                return _gems;

            case CurrencyType.Energy:
                return _energy;

            default:
                return 0;
        }
    }

    public void Add(CurrencyType type, int value)
    {
        if (value <= 0)
            return;

        SetAmount(type, GetAmount(type) + value);
    }

    public bool TrySpend(CurrencyType type, int value)
    {
        if (value <= 0)
            return true;

        int current = GetAmount(type);

        if (current < value)
            return false;
        
        SetAmount(type, current - value);
        return true;
    }

    private void SetAmount(CurrencyType type, int newValue)
    {


        //записываем новое значение в нужное поле
        switch (type)
        {
            case CurrencyType.Coins:
                _coins = newValue;
                break;
            
            case CurrencyType.Gems:
                _gems = newValue;
                break;
            
            case CurrencyType.Energy:
                _energy = newValue;
                break;
        }
        
        CurrencyChanged?.Invoke(type, newValue);
    }
}