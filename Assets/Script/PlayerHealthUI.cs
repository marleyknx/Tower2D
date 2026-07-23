using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    PlayerHealth playerHealth;

    [SerializeField] private Image healthImage;
    [SerializeField] private Image healthDamageImage;

    [SerializeField] float LerpSpeed;
    float displayRatio = 1;
    float TargetRatio = 1;

    float currentAlpha, maxHalpha,velocityAlpha;
  



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

        currentAlpha = Mathf.SmoothDamp(currentAlpha, maxHalpha, ref velocityAlpha, 0.1f);
        Color alpha = healthDamageImage.color;
        alpha.a = currentAlpha;
        healthDamageImage.color = alpha;
        
    }

   

    private void UpdateHealthUi(float pCurrrent, float pMax)
    {
        TargetRatio = pCurrrent / pMax;
        currentAlpha = .5f;

    }
}
