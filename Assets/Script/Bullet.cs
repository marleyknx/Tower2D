using System;
using System.Net;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   [SerializeField] private float speed = 5f;
   public float damage = 2f;
    public Vector3 direction;
    public event Action<float> OnHit;
    
    void Update()
    {
        if (direction == Vector3.zero) return;
        transform.position += direction.normalized * speed * Time.deltaTime;
    }

    private void OnDisable()
    {
        OnHit = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            EnemyHealth health = collision.GetComponent<EnemyHealth>();

            if(health != null)
            {
                OnHit?.Invoke(5);
                health.takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }

}
