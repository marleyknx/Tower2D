using System;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    [SerializeField]Text goldText;

    private void Start()
    {
        GameManager.Instance.OnGoldChanged += UpdateGoldUi;

    }

    private void OnDisable()
    {
        GameManager.Instance.OnGoldChanged -= UpdateGoldUi;
    }

    private void UpdateGoldUi(int gold)
    {
        goldText.text = gold.ToString();
    }
}
