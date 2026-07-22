using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currenthealth;
    public float maxHealth;

    public event Action<float, float> OnHealthUpdate; // min , max
   
    

    private void Start()
    {
        currenthealth = maxHealth;
    }


    public void takeDamage(float amount)
    {
        currenthealth -= amount;
        OnHealthUpdate?.Invoke(currenthealth, maxHealth);

        if (currenthealth <= 0) OnDeath();
    }

    private void OnDeath()
    {
        GameManager.Instance.InvokePlayerDeath();
    }
}
