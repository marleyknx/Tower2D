using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<int> OnGoldChanged;
    public event Action OnDeath;
    public event Action OnWin;
    public int gold { get;private set; }

    private void OnEnable()
    {
        OnDeath += GameOver;
        OnWin += Win;

    }

    private void OnDisable()
    {
        OnDeath -= GameOver;
        OnWin -= Win;
    }



    private void Win()
    {
        //Win Logic
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

       
    }

    private void Start()
    {
        AddGold(100);
    }

    public void AddGold(int amount)
    {
        gold += amount;
        OnGoldChanged?.Invoke(gold);
    }
    public void RemoveGold(int amount)
    {
        gold -= amount;
        OnGoldChanged?.Invoke(gold);
    }

    public void InvokePlayerDeath()
    {
        OnDeath?.Invoke();
    }

    public void GameOver()
    {
        // game over logic
    }
}
