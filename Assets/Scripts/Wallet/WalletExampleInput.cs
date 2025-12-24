using UnityEngine;

public class WalletExampleInput : MonoBehaviour
{
    private WalletService _wallet;

    public void Construct(WalletService wallet)
    {
        _wallet = wallet;
    }

    private void Update()
    {
        if (_wallet == null)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
            _wallet.Add(CurrencyType.Coins, 1);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _wallet.TrySpend(CurrencyType.Coins, 1);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            _wallet.Add(CurrencyType.Gems, 5);
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

    private void OnCurrencyChanged(CurrencyType type, int newValue)
    {
        Debug.Log(type + " = " + newValue);
    }
}