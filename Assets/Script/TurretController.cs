using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class TurretController : MonoBehaviour
{
    public TurretData data;
    public TurretRuntimeData runtimeData = new();
    Collider2D[] colliders;
    [SerializeField] LayerMask targetLayer;
    private Transform Target;
    bool isInRange;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private Bullet BulletPrefab;
    Vector2 direction;
    float timer;
    TurretLeveler leveler;  

    private void Awake()
    {
        runtimeData.Initialize(data);
    }

    private void Start()
    {
       
        leveler = GetComponent<TurretLeveler>();
    }

    private void Update()
    {
        LookAtTarget();
        Weapon();
    }

    private void Weapon()
    {
        if (isInRange)
        {
           

            timer += Time.deltaTime;
            if (timer >= runtimeData.attackSpeed)
            {
                Bullet bulletSpawn = Instantiate(BulletPrefab,firingPoint.position,transform.rotation);
                bulletSpawn.direction = direction;
                bulletSpawn.damage = runtimeData.damage;
                
                bulletSpawn.OnHit += leveler.GainExpFlateRate;
                 timer = 0;
            }
        }
    }

    private void LookAtTarget()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, runtimeData.radius, targetLayer);

        isInRange = colliders.Length > 0;

        if (isInRange)
        {
           
            Target = GetBestTarget(colliders);
            direction = Target.position - firingPoint.position;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion rotation = Quaternion.Slerp(firingPoint.rotation, Quaternion.AngleAxis(angle, Vector3.forward), data.RotationSpeed * Time.deltaTime);

            firingPoint.rotation = rotation;
        }
        
    }



    Transform GetBestTarget(Collider2D[] colliders)
    {
        Transform best = null;
        int highestWaypoint = -1;
        float closestDistToNext = float.MaxValue;

        foreach (Collider2D col in colliders)
        {
            EnemiePatrol enemy = col.GetComponent<EnemiePatrol>();
            if (enemy == null) continue;

            float distToNext = Vector2.Distance(col.transform.position,
                               enemy.patrolPoints.Points[enemy.targetIndex].position);

            if (enemy.targetIndex > highestWaypoint ||
               (enemy.targetIndex == highestWaypoint && distToNext < closestDistToNext))
            {
                highestWaypoint = enemy.targetIndex;
                closestDistToNext = distToNext;
                best = col.transform;
            }
        }

        return best;
    }

   


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, runtimeData.radius);
    }



}
