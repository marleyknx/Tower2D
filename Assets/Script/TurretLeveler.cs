
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
        public int cost;

        public static class StatUpgradeUtility
        {
            public static void ApplyUpgrade(StatUpgrade upgrade,TurretRuntimeData data )
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
                        data.attackSpeed = Mathf.Max(0.05f, data.attackSpeed - upgrade.amount);
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

    public bool isLevelReady { get; private set; }
    public event Action OnLevelReady, OnUpgrade;
    public int level;
    int maxLevel;
    public int MaxLevel => maxLevel;
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

    int testChoice = -1;



    private void Start()
    {
        runtimeData = GetComponent<TurretController>().runtimeData;
        RequiredExp = calculateRequiredXp(level + 1);
        Debug.Log($" runtime data {runtimeData.radius} ");
        maxLevel = Leveler.Count -1;
    }



    

    public void GainExpFlateRate(float xp)
    {

        if (isLevelReady) return;
        currentExp += xp;
       
        if(currentExp >= RequiredExp)
        {
            isLevelReady = true;
            OnLevelReady?.Invoke();
        }

        
    }

    public List<StatUpgrade> GetCurrentChoices()
    {
        if (level >= maxLevel) return null;
        return Leveler[level + 1].upgrades;
       
    }

    public bool TryBuyUpgrade(int choiceIndex)
    {
        // TODO, dans cet ordre, une opération par ligne :
        if (!isLevelReady) return false;
        
        
        List<StatUpgrade> choices = GetCurrentChoices();
      
        if (choices == null) return false;                              
        if (choiceIndex < 0 || choiceIndex >= choices.Count) return false;
        
        StatUpgrade chosen = choices[choiceIndex];
        if(GameManager.Instance.gold < chosen.cost)
        {
            Debug.LogError("vous n'avez pas assez");
            return false;
        }
        else
        {
            StatUpgradeUtility.ApplyUpgrade(chosen, runtimeData);
            GameManager.Instance.RemoveGold(chosen.cost);
            level++;
            currentExp = Mathf.RoundToInt(currentExp - RequiredExp);
            RequiredExp = calculateRequiredXp(level + 1);
            isLevelReady = false;
            OnUpgrade?.Invoke();
            
        }
        return false;
    }

   

   
    public void ResetLevel() => level = 0;
    public void ResetExperience() => currentExp = 0;

    int calculateRequiredXp(int targetLevel)
    {
        int solveForRequire = 0;
        for (int levelCycle = 1; levelCycle <= targetLevel; levelCycle++)
        {
            solveForRequire += (int)Mathf.Floor(targetLevel + additionMultiplier * Mathf.Pow(powerMultiplier, levelCycle / divisionMultiplier));
        }
        return solveForRequire / 4;
    }
   

}
