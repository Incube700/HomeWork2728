using System.Collections.Generic;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private WalletCurrencyRowView _rowPrefab;

    [Header("Icons")]
    [SerializeField] private Sprite _coinsIcon;
    [SerializeField] private Sprite _gemsIcon;
    [SerializeField] private Sprite _energyIcon;

    private WalletService _wallet;
    private readonly Dictionary<CurrencyType, WalletCurrencyRowView> _rows =
        new Dictionary<CurrencyType, WalletCurrencyRowView>();

    public void Construct(WalletService wallet)
    {
        _wallet = wallet;

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

        // подчистим контейнер, чтобы не плодить дубли при перезапуске/сценах
        for (int i = _container.childCount - 1; i >= 0; i--)
            Destroy(_container.GetChild(i).gameObject);

        foreach (CurrencyType type in _wallet.GetCurrencies())
        {
            WalletCurrencyRowView row = Instantiate(_rowPrefab, _container);
            row.Setup(type, GetIcon(type));

            _rows.Add(type, row);
        }
    }

    private void RefreshAll()
    {
        foreach (CurrencyType type in _wallet.GetCurrencies())
            OnCurrencyChanged(type, _wallet.GetAmount(type));
    }

    private void OnCurrencyChanged(CurrencyType type, int newValue)
    {
        if (_rows.TryGetValue(type, out WalletCurrencyRowView row))
            row.SetAmount(newValue);
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
