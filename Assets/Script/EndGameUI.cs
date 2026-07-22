using UnityEngine;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] GameObject WinPanel, LosePanel;
    private bool win, lose;


    private void Awake()
    {
        WinPanel.SetActive(false);
        LosePanel.SetActive(false);
    }

    private void OnEnable()
    {
        GameManager.Instance.OnDeath += OpenLosePanel;
        GameManager.Instance.OnWin += OpenWinPanel;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnDeath -= OpenLosePanel;
        GameManager.Instance.OnWin -= OpenWinPanel;
    }

    void OpenWinPanel()
    {
        win = !win;
       WinPanel.SetActive(win);
    }

    void OpenLosePanel()
    {
        lose = !lose;
        LosePanel.SetActive(lose);
    }
}
