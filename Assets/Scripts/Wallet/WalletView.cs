using System;
using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private WalletService _wallet;
    [SerializeField] private Transform _container;
    [SerializeField] private WalletCurrencyRowView _rowPrefab;

    [Header("Icons")]
    [SerializeField] private Sprite _coinsIcon;
    [SerializeField] private Sprite _gemsIcon;
    [SerializeField] private Sprite _energyIcon;

    private readonly Dictionary<CurrencyType, WalletCurrencyRowView> _rows = new Dictionary<CurrencyType, WalletCurrencyRowView>();

    private void Start()
    {
        BuildUI();
        RefreshAll();
    }

    private void OnEnable()
    {
        if (_wallet != null)
            _wallet.CurrencyChanged += OnCurrencyChanged;
    }

    private void OnDisable()
    {
        if (_wallet != null)
            _wallet.CurrencyChanged -= OnCurrencyChanged;
    }

    private void BuildUI()
    {
        _rows.Clear();

        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
        {
            WalletCurrencyRowView row = Instantiate(_rowPrefab, _container);
            row.Setup(type, GetIcon(type));

            _rows.Add(type, row);
        }
    }

    private void RefreshAll()
    {
        foreach (CurrencyType type in Enum.GetValues(typeof(CurrencyType)))
        {
            OnCurrencyChanged(type, _wallet.GetAmount(type));
        }
    }

    private void OnCurrencyChanged(CurrencyType type, int newValue)
    {
        if (_rows.TryGetValue(type, out WalletCurrencyRowView row))
        {
            row.SetAmount(newValue);
        }
    }

    private Sprite GetIcon(CurrencyType type)
    {
        switch (type)
        {
            case CurrencyType.Coins:
                return _coinsIcon;

            case CurrencyType.Gems:
                return _gemsIcon;

            case CurrencyType.Energy:
                return _energyIcon;

            default:
                return null;
        }
    }
}
