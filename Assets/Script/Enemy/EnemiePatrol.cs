using Unity.Android.Gradle.Manifest;
using UnityEngine;

public class EnemiePatrol : MonoBehaviour
{
  [HideInInspector] public  PatrolPoint patrolPoints;
    private Vector2 currentTarget;
  [HideInInspector]  public int targetIndex;

    public EnemyData enemyData;

     Vector3 CurrentDirection() => (currentTarget - (Vector2)transform.position);

   
   

    private void Update()
    {
        CheckDistanceToTarget();
        transform.position += CurrentDirection().normalized * enemyData.speed * Time.deltaTime;
    }

    private void CheckDistanceToTarget()
    {
        currentTarget = patrolPoints.Points[targetIndex].position;
        if (CurrentDirection().magnitude < .1f)
        {
            targetIndex++;
            if (targetIndex >= patrolPoints.Points.Count)
            {
                PlayerHealth playerHealth = FindFirstObjectByType<PlayerHealth>();
                playerHealth.takeDamage(enemyData.damage);
                //destroy
                GetComponent<EnemyHealth>().ReachedEnd();
                return;
            }
            currentTarget = patrolPoints.Points[targetIndex].position;
        }
    }



}
