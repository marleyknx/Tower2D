using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TowerDefense/EnemyData")]
public class EnemyData : ScriptableObject
{
    [Header("Stats")]
    public float maxHealth = 100f;
    public float speed = 5f;
    public int goldReward = 50;
    public float damage = 5f;
}
