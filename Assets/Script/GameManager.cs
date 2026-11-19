using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameResult {Victory,Defeate}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public event Action<int> OnGoldChanged;
    public event Action<GameResult> OnGameEnded;
    public event Action<bool> OnPauseChanged;   // pour un futur menu pause
    
    private bool gameEnded;
    public int gold { get;private set; }

  

    public void EndGame(GameResult result)
    {
       
        if (gameEnded) return;
       
        gameEnded = true;
       
        SetPaused(true);
       
        OnGameEnded?.Invoke(result);
    }

    public void SetPaused(bool paused)
    {
        
        if(paused) Time.timeScale = 0f; else Time.timeScale = 1.0f;
       
        OnPauseChanged?.Invoke(paused);
    }
    
    public void Retry()
    {
        SetPaused(false);
        SceneManager.LoadScene(1);
    }
    public void MainMenu()
    {
        SetPaused(false);
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        
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

}
