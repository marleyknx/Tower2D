using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.GPUSort;

public class SpawnTurretUI : MonoBehaviour
{
    
    [SerializeField] List<Card> TurretCards;
   


    [SerializeField] private Card CurrentSelectedCard;
    public static event Action<TurretData> OnTurretSelected;


    private void Start()
    {
       
    }

    public void OnEnable()
    {
        foreach (var card in TurretCards)
            card.OnCardClicked += HandleCardSelected;
    }

    private void OnDisable()
    {
        foreach (var card in TurretCards)
            card.OnCardClicked -= HandleCardSelected;
    }

    private void HandleCardSelected(Card clicked)
    {
        // désélectionne l'ancienne
        CurrentSelectedCard?.SetHighlight(false);

        // si on reclique la même → déselect
        if (CurrentSelectedCard == clicked)
        {
            CurrentSelectedCard = null;
            OnTurretSelected?.Invoke(null);
            return;
        }

        CurrentSelectedCard = clicked;
        CurrentSelectedCard.SetHighlight(true);
        OnTurretSelected?.Invoke(clicked.data);
    }
   
}
