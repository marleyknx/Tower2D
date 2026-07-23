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

    public Vector3 defaultScale = Vector3.one;
    Vector3 hitScale = Vector3.one;

    [SerializeField]  Vector3 ScalingSpeed;

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
        defaultScale = transform.localScale;
        currenthealth = data.maxHealth;
    }

    private void Update()
    {
        defaultScale = Vector3.SmoothDamp(defaultScale, hitScale, ref ScalingSpeed, 0.08f);
        transform.localScale = defaultScale;
    }


    public void VisualHit()
    {
        defaultScale = new Vector3(1.2f, .8f, 1.2f);


    }

 
    public void takeDamage(float amount)
    {
        currenthealth -= amount;
        OnHealthUpdate?.Invoke(currenthealth, data.maxHealth);
        VisualHit();
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
