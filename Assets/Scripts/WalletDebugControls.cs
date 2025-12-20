using UnityEngine;

public class WalletDebugControls : MonoBehaviour
{
    [SerializeField] private WalletService _wallet;

    [Header("Amounts")]
    [SerializeField] private int _coinStep = 1;
    [SerializeField] private int _gemsStep = 5;
    [SerializeField] private int _energyStep = 1;

    private void Update()
    {
        // Coins
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _wallet.Add(CurrencyType.Coins, _coinStep);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            _wallet.TrySpend(CurrencyType.Coins, _coinStep);

        // Gems
        if (Input.GetKeyDown(KeyCode.Alpha3))
            _wallet.Add(CurrencyType.Gems, _gemsStep);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            _wallet.TrySpend(CurrencyType.Gems, _gemsStep);

        // Energy
        if (Input.GetKeyDown(KeyCode.Alpha5))
            _wallet.Add(CurrencyType.Energy, _energyStep);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            _wallet.TrySpend(CurrencyType.Energy, _energyStep);
    }
}