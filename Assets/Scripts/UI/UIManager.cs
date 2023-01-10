using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goldLabel;
    [SerializeField] TextMeshProUGUI gemLabel;

    [SerializeField] Sprite unclickedButtonBG;
    [SerializeField] Sprite clickedButtonBG;

    void Start()
    {
        Wallet.OnGoldValueChanged += Wallet_OnGoldValueChanged;
    }

    void Wallet_OnGoldValueChanged(Currency goldAmount)
    {
        goldLabel.text = goldAmount.ToString();
    }

    #region ButtonLogic
    public void OnButtonClick(Image buttonBGImage)
    {
        StartCoroutine(ClickButton(buttonBGImage));
    }

    IEnumerator ClickButton(Image buttonBGImage)
    {
        buttonBGImage.sprite = clickedButtonBG;
        yield return new WaitForSeconds(0.1f);
        buttonBGImage.sprite = unclickedButtonBG;
    }
    #endregion
}
