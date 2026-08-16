using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
     float currenthealth;
    
    [SerializeField] private EnemyData data;

    public event Action<float,float> OnHealthUpdate; // min , max

    public event Action OnDeath;
    public event Action OnRemove;

    public SquashEffect squash;
   
  

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
        squash.Initialize(transform.localScale);
        currenthealth = data.maxHealth;
    }

    private void Update()
    {
        
        transform.localScale = squash.UpdateSquashEffect(0.08f);
    }


   

 
    public void takeDamage(float amount)
    {
        currenthealth -= amount;
        OnHealthUpdate?.Invoke(currenthealth, data.maxHealth);
        squash.VisualHit(1.2f,.9f,1.5f);
        if(currenthealth <= 0) OnDeath?.Invoke();
    }

    

    private void Death()
    {
        GameManager.Instance.AddGold(data.goldReward);
        gameObject.SetActive(false);
        OnRemove?.Invoke();
    }

    public void ReachedEnd()
    {
        gameObject.SetActive(false);
        OnRemove?.Invoke();
    }    
   

}
