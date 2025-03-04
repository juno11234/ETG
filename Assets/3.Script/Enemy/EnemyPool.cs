using UnityEngine;
using System.Collections.Generic;
public enum EnemyType { Enemy1,Enemy2,Enemy3}
public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    int poolSize = 10;

    [SerializeField]
    GameObject enemy1Prefab;
    
    [SerializeField]
    GameObject enemy2Prefab;

    [SerializeField]
    GameObject enemy3Prefab;
    Dictionary<EnemyType, Queue<GameObject>> enemyPools = new Dictionary<EnemyType,Queue<GameObject>>();

    void Start()
    {
        InitializePool(EnemyType.Enemy1, enemy1Prefab);
        InitializePool(EnemyType.Enemy2, enemy2Prefab);                    
        InitializePool(EnemyType.Enemy3, enemy3Prefab);                    
    }
    void InitializePool(EnemyType type,GameObject prefab)
    {
        Queue<GameObject> enemyQueue = new Queue<GameObject>();
        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = Instantiate(prefab);
            enemy.SetActive(false);
            enemyQueue.Enqueue(enemy);
        }

        enemyPools[type] = enemyQueue;
    }
public GameObject GetEnemy(EnemyType type)
    {
        if (enemyPools[type].Count > 0&&enemyPools.ContainsKey(type))
        {
            GameObject enemy = enemyPools[type].Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        else
        {
            return Instantiate(type == EnemyType.Enemy1 ? enemy1Prefab : enemy2Prefab);
        }
    }
    public void ReturnEnemy(GameObject enemy,EnemyType type)
    {
        enemy.SetActive(false);
        enemyPools[type].Enqueue(enemy);
    }
}
