using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] private Image healthImage;
    [SerializeField] float LerpSpeed;
    float displayRatio = 1;
    float TargetRatio = 1;


  



    private void OnEnable()
    {
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        TargetRatio = 1f;
        displayRatio = 1f;
        playerHealth.OnHealthUpdate += UpdateHealthUi;
    }

    private void OnDisable()
    {
        playerHealth.OnHealthUpdate -= UpdateHealthUi;
    }

    private void Update()
    {
        displayRatio = Mathf.Lerp(displayRatio, TargetRatio, LerpSpeed * Time.deltaTime);
        healthImage.fillAmount = displayRatio;
    }

    private void UpdateHealthUi(float pCurrrent, float pMax)
    {
        TargetRatio = pCurrrent / pMax;


    }
}
