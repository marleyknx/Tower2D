using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    [SerializeField] private Image healthImage;
    private EnemyHealth enemyHealth;
    [SerializeField] float LerpSpeed;
    float displayRatio = 1;
    float TargetRatio = 1;
   

    private void OnEnable()
    {
        if (enemyHealth == null) enemyHealth = GetComponent<EnemyHealth>(); //security
        TargetRatio = 1f;
        displayRatio = 1f;
        enemyHealth.OnHealthUpdate += UpdateHealthUi;
    }

    private void OnDisable()
    {
        enemyHealth.OnHealthUpdate -= UpdateHealthUi;
    }

    private void Update()
    {
        displayRatio = Mathf.Lerp(displayRatio, TargetRatio, LerpSpeed * Time.deltaTime);
        healthImage.fillAmount = displayRatio;
    }

    private void  UpdateHealthUi(float pCurrrent, float pMax)
    {
        TargetRatio = pCurrrent / pMax;

       
    }
}
