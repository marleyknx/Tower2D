using System;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
   public TurretData data;
    [SerializeField] Text goldText;
    public event Action<Card> OnCardClicked;

    [SerializeField] private Image background;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color selectedColor;

    private void Start()
    {
        goldText.text = data.Cost.ToString();
    }

    public void Select()
    {
        OnCardClicked?.Invoke(this);
    }

    public void SetHighlight(bool isSelected)
    {
        background.color = isSelected ? selectedColor : normalColor;
    }
}
