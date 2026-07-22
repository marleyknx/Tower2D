using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "TowerDefense/TurretData")]
public class TurretData : ScriptableObject
{
   
    [Header("Stats")]
    public float radius = 2;
    public float damage = 5f;
    public float attackSpeed = 1;
    [Range(0,1)]
    public float RotationSpeed = 1;
    public GameObject turret;
    [Space]
    public int Cost = 80;
}

public enum StatType
{
    radius,
    damage,
    attackSpeed,
    RotationSpeed
}

[System.Serializable]
public class TurretRuntimeData
{
    public float damage;
    public float radius;
    public float attackSpeed;

    public void Initialize(TurretData data)
    {
        damage = data.damage;
        radius = data.radius;
        attackSpeed = data.attackSpeed;
    }
}