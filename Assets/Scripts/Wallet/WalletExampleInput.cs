using UnityEngine;

public class WalletExampleInput : MonoBehaviour
{
    [SerializeField] private WalletService _wallet;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _wallet.Add(CurrencyType.Coins, 1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _wallet.TrySpend(CurrencyType.Coins, 1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            _wallet.Add(CurrencyType.Gems, 5);
    }

    private void OnEnable()
    {
        _wallet.CurrencyChanged += OnCurrencyChanged;
    }

    private void OnDisable()
    {
        _wallet.CurrencyChanged -= OnCurrencyChanged;
    }

    private void OnCurrencyChanged(CurrencyType type, int newValue)
    {
        Debug.Log(type + " = " + newValue);
    }
}