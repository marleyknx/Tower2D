using System;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] GameObject ConditionPanel;
    [SerializeField] Text ConditionTxt;
    


    private void Awake() => ConditionPanel?.SetActive(false);


  
    private void OnDisable()
    {
        if(GameManager.Instance != null )
        GameManager.Instance.OnGameEnded -= Show;
    }
    private void Start()
    {
        GameManager.Instance.OnGameEnded += Show;
        

    }

    private void Show(GameResult result)
    {
        ConditionPanel.SetActive(true);
       ConditionTxt.text = result == GameResult.Victory ? "VICTORY" : "GAME OVER";
        

    }

    
}
