using UnityEngine;

public class WalletDebugControls : MonoBehaviour
{
    [SerializeField] private WalletService _wallet;

    [Header("Amounts")]
    [SerializeField] private int _coinStep = 1;
    [SerializeField] private int _gemsStep = 5;
    [SerializeField] private int _energyStep = 2;

    private void Update()
    {
        // Coins
        if (Input.GetKeyDown(KeyCode.Alpha1))
            AddAndLog(CurrencyType.Coins, _coinStep);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SpendAndLog(CurrencyType.Coins, _coinStep);

        // Gems
        if (Input.GetKeyDown(KeyCode.Alpha3))
            AddAndLog(CurrencyType.Gems, _gemsStep);

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SpendAndLog(CurrencyType.Gems, _gemsStep);

        // Energy
        if (Input.GetKeyDown(KeyCode.Alpha5))
            AddAndLog(CurrencyType.Energy, _energyStep);

        if (Input.GetKeyDown(KeyCode.Alpha6))
            SpendAndLog(CurrencyType.Energy, _energyStep);
    }

    private void AddAndLog(CurrencyType type, int value)
    {
        int before = _wallet.GetAmount(type);

        _wallet.Add(type, value);

        int after = _wallet.GetAmount(type);
        Debug.Log($"{type}: +{value} (было {before} → стало {after})");
    }

    private void SpendAndLog(CurrencyType type, int value)
    {
        int before = _wallet.GetAmount(type);

        bool success = _wallet.TrySpend(type, value);

        if (success == false)
        {
            int missing = value - before;
            if (missing < 0)
                missing = 0;

            Debug.Log($"{type}: не удалось списать {value} (есть {before}, не хватает {missing})");
            return;
        }

        int after = _wallet.GetAmount(type);
        Debug.Log($"{type}: -{value} (было {before} → стало {after})");
    }

}