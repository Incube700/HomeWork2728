using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WalletCurrencyRowView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _amountText;

    public void Setup(CurrencyType type, Sprite icon)
    {
        if (_icon != null)
            _icon.sprite = icon;
    }

    public void SetAmount(int value)
    {
        if (_amountText != null)
            _amountText.text = value.ToString();
    }
}