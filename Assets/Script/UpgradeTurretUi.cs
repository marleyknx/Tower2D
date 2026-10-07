using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTurretUi : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] Text levelText;
    [SerializeField] Slider xpBar;
    [SerializeField] Button[] choiceButtons;
    [SerializeField] Text[] choiceTexts;


    TurretLeveler current;

    private void OnEnable() => TurretSelector.OnTurretClicked += Show;
    private void OnDisable() => TurretSelector.OnTurretClicked -= Show;
    private void Start() => panel.SetActive(false);


    private void Show(TurretLeveler leveler)
    {

        if (current != null)
        {

            current.OnUpgrade -= Refresh;
            current.OnLevelReady -= Refresh;
        }

        current = leveler;
        if (current == null)
        {
            panel.SetActive(false);
            return;
        }


        current.OnLevelReady += Refresh;
        current.OnUpgrade += Refresh;

        panel.SetActive(true);
        Refresh();
    }

    private void Refresh()
    {
       var currentChoice =  current.GetCurrentChoices();
        levelText.text = " Nv " + current.level;
        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (choiceButtons.Length < 0 || i >= currentChoice.Count) choiceButtons[i].gameObject.SetActive(false);
            else
            {
                TurretLeveler.StatUpgrade choice = currentChoice[i];

                choiceButtons[i].gameObject.SetActive(true);
                choiceTexts[i].text = $"{choice.statType} +{choice.amount} — {choice.cost}g";
                choiceButtons[i].interactable = current.isLevelReady;

            }
        }
    }

    private void Update()
    {
        // TODO : si current n'est pas null → mettre à jour la barre d'XP
        if (current != null) xpBar.value = current.currentExp / current.RequiredExp; 
        //        (la valeur d'un Slider va de 0 à 1 par défaut)
    }

    public void OnChoiceClicked(int index)
    {
        // TODO : demander l'achat à la tourelle courante
        if(current != null)
        {
            current.TryBuyUpgrade(index);
        }
    }
}
