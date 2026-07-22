
using System.Collections.Generic;
using UnityEngine;
using System;
using static TurretLeveler.StatUpgrade;


public class TurretLeveler : MonoBehaviour
{
    [Serializable]
    public class StatUpgrade
    {
       

        public  StatType statType;
       

        public float amount;

        public static class StatUpgradeUtility
        {
            public static void ApplyUpgrade(StatUpgrade upgrade,TurretRuntimeData data)
            {
                switch (upgrade.statType)
                {
                    case StatType.radius:
                        
                        data.radius += upgrade.amount;
                        Debug.Log($" data {data.radius} + upgrades {upgrade.amount}");
                        break;

                    case StatType.damage:
                        data.damage += upgrade.amount;
                        break;

                    case StatType.attackSpeed:
                        data.attackSpeed -= upgrade.amount;
                        break;
                }
            }
        }
    }

    [Serializable] 
    public class Level
    {
        public List< StatUpgrade> upgrades;
    }

    public int level;
    public float currentExp;
    public float RequiredExp;
    public bool Test;
    [Space]
    
    TurretRuntimeData runtimeData = new();
    public List <Level> Leveler;

    [Header("Multiplier")]
    [Range(1f, 300f)]
    public float additionMultiplier = 300;
    [Range(2f, 4f)]
    public float powerMultiplier = 2;
    [Range(7f, 14f)]
    public float divisionMultiplier = 7;

   
   

    private void Start()
    {
        runtimeData = GetComponent<TurretController>().runtimeData;
        RequiredExp = calculateRequiredXp();
        Debug.Log($" runtime data {runtimeData.radius} ");
    }

   

    private  void LevelUp()
    {
        
        
        
      level++;
      foreach(var upgrades in Leveler[level].upgrades)
        StatUpgradeUtility.ApplyUpgrade(upgrades,runtimeData);
        
           
      currentExp = Mathf.RoundToInt(currentExp - RequiredExp);
      RequiredExp = calculateRequiredXp();
        
    }


    private void Update()
    {
        if (level >= Leveler.Count -1)
        {
            ResetExperience();
            return;
        }
           
        if (currentExp >= RequiredExp) LevelUp();
        
        //test
        if (Test)
        {
            LevelUp();
            Test = false;
        }

        
    }

    public void GainExpFlateRate(float XPGained) => currentExp += XPGained;
    public void ResetLevel() => level = 0;
    public void ResetExperience() => currentExp = 0;

    int calculateRequiredXp()
    {
        int solveForRequire = 0;
        for (int levelCycle = 1; levelCycle <= level; levelCycle++)
        {
            solveForRequire += (int)Mathf.Floor(level + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequire / 4;
    }
   

}
