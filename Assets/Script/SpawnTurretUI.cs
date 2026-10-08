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
        SpawnTurret.OnTurretPlaced += Deselect;
    }

    private void OnDisable()
    {
        foreach (var card in TurretCards)
            card.OnCardClicked -= HandleCardSelected;
        SpawnTurret.OnTurretPlaced -= Deselect;
    }
    private void Deselect()
    {
        CurrentSelectedCard?.SetHighlight(false);
        CurrentSelectedCard = null;
        OnTurretSelected?.Invoke(null);
    }

    private void HandleCardSelected(Card clicked)
    {
        // désélectionne l'ancienne
        CurrentSelectedCard?.SetHighlight(false);

        // si on reclique la même → déselect
        if (CurrentSelectedCard == clicked)
        {
            Deselect();
            return;
        }
       

            CurrentSelectedCard = clicked;
        CurrentSelectedCard.SetHighlight(true);
        OnTurretSelected?.Invoke(clicked.data);
    }
   
}
