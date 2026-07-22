using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
     float currenthealth;
    
    [SerializeField] private EnemyData data;

    public event Action<float,float> OnHealthUpdate; // min , max

    public event Action OnDeath;

    private void OnEnable()
    {
        OnDeath += Death;
    }

    private void OnDisable()
    {
        OnDeath -= Death;
    }

    private void Start()
    {
        currenthealth = data.maxHealth;
    }

    
    public void UpdateHealth()
    {
        
    }

    public void takeDamage(float amount)
    {
        currenthealth -= amount;
        OnHealthUpdate?.Invoke(currenthealth, data.maxHealth);

        if(currenthealth <= 0) OnDeath?.Invoke();
    }

    

    private void Death()
    {
        GameManager.Instance.AddGold(data.goldReward);
        gameObject.SetActive(false);
    }
    
   

}
