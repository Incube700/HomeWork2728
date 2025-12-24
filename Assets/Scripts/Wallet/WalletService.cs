using System;
using System.Collections.Generic;

public class WalletService
{
    public event Action<CurrencyType, int> CurrencyChanged;

    private readonly Dictionary<CurrencyType, int> _amounts;

    public WalletService()
    {
        _amounts = new Dictionary<CurrencyType, int>
        {
            { CurrencyType.Coins, 0 },
            { CurrencyType.Gems, 0 },
            { CurrencyType.Energy, 0 }
        };
    }

    // Если захочешь стартовые значения
    public WalletService(Dictionary<CurrencyType, int> startAmounts)
    {
        _amounts = new Dictionary<CurrencyType, int>();

        foreach (var pair in startAmounts)
            _amounts[pair.Key] = pair.Value;
    }

    public IEnumerable<CurrencyType> GetCurrencies()
    {
        return _amounts.Keys;
    }

    public int GetAmount(CurrencyType type)
    {
        if (_amounts.TryGetValue(type, out int value))
            return value;

        return 0;
    }

    public void Add(CurrencyType type, int value)
    {
        if (value <= 0)
            return;

        SetAmount(type, GetAmount(type) + value);
    }

    public bool TrySpend(CurrencyType type, int value)
    {
        // фикс по замечанию: value <= 0 не должно считаться успехом
        if (value <= 0)
            return false;

        int current = GetAmount(type);

        if (current < value)
            return false;

        SetAmount(type, current - value);
        return true;
    }

    private void SetAmount(CurrencyType type, int newValue)
    {
        if (_amounts.ContainsKey(type) == false)
            _amounts.Add(type, 0);

        _amounts[type] = newValue;
        CurrencyChanged?.Invoke(type, newValue);
    }
}