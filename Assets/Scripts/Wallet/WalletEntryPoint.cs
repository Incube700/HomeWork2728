using UnityEngine;

public class WalletEntryPoint : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private WalletView _walletView;

    [Header("Input")]
    [SerializeField] private WalletDebugControls _walletDebugControls;
    [SerializeField] private WalletExampleInput _walletExampleInput;

    private WalletService _wallet;

    private void Awake()
    {
        _wallet = new WalletService();

        if (_walletView != null)
            _walletView.Construct(_wallet);

        if (_walletDebugControls != null)
            _walletDebugControls.Construct(_wallet);

        if (_walletExampleInput != null)
            _walletExampleInput.Construct(_wallet);
    }
}