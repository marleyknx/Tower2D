using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    
    [System.Serializable]
    public class Wave
    {
        public float delayBeforeWave = 2f;
        public List<GameObject> prefabGO = new List<GameObject>();
        public float timeBetweenSpawn = 1f;

        [Header("Fin de vague")]
        public bool waitForEnemiesDead = false; 
        public float delayBeforeNextWave = 3f;
    }

    [Header("Vagues")]
    public List<Wave> waves = new List<Wave>();

    [Header("Paramètres globaux")]
    public bool endless = false;
    [Header("Debug")]
    [SerializeField] private int currentWaveIndex = 0;
    [SerializeField] private int activeEnemyCount = 0;
    public int endlessCount;

    private void Start()
    {
        StartCoroutine(RunWaves());
    }


    private IEnumerator RunWaves()
    {
        do
        {
            currentWaveIndex = 0;
                endlessCount++;
            
            foreach (Wave wave in waves)
            {
                yield return new WaitForSeconds(wave.delayBeforeWave);
                yield return StartCoroutine(SpawnWave(wave));
               
                if (wave.waitForEnemiesDead)
                    yield return new WaitUntil(() => activeEnemyCount <= 0);
                else
                    yield return new WaitForSeconds(wave.delayBeforeNextWave);

                currentWaveIndex++;

            }
        } while (endless);

       
        yield return new WaitUntil(() => activeEnemyCount <= 0); GameManager.Instance.InvokePlayerWin();
    }

    private IEnumerator SpawnWave(Wave wave)
    {
            
        if (wave.prefabGO.Count == 0) yield break;
        

        int extraEnemies = (endless) ? endlessCount : 0;

        int totalToSpawn = wave.prefabGO.Count + extraEnemies;

        for (int i = 0; i < totalToSpawn; i++)
        {

            GameObject prefabToSpawn = wave.prefabGO[i % wave.prefabGO.Count];

            SpawnEnemy(prefabToSpawn);

            Debug.Log($"Spawn de l'ennemi {i + 1} sur {totalToSpawn}");
            yield return new WaitForSeconds(wave.timeBetweenSpawn);
        }
       
    }

    private void SpawnEnemy(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, GetSpawnPoint(), Quaternion.identity);

       
        go.GetComponent<EnemiePatrol>().patrolPoints = FindFirstObjectByType<PatrolPoint>();

        activeEnemyCount++;

        
        EnemyHealth health = go.GetComponent<EnemyHealth>();
        health.OnRemove += () => activeEnemyCount--;
    }

    private Vector3 GetSpawnPoint()
    {
      
        return FindFirstObjectByType<PatrolPoint>().Points[0].position;
    }
}

