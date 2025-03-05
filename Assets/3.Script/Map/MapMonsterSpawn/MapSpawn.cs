using System.Collections;
using UnityEngine;

public class MapSpawn : MonoBehaviour
{
    EnemyPool enemyPool;

    [SerializeField]
    Transform[] enemySpawn1;

    [SerializeField]
    Transform[] enemySpawn2;

    [SerializeField]
    Transform[] enemySpawn3;

    [SerializeField]
    GameObject[] doors;

    [SerializeField]
    GameObject chest;

    float spawnDelay = 1f;

    BoxCollider2D boxColl;
    int remainingEnemy = 0;
    

    private void Start()
    {
        
        TryGetComponent<BoxCollider2D>(out boxColl);
        enemyPool = FindAnyObjectByType<EnemyPool>();
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
        if (chest != null)
            chest.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        
        if (coll.CompareTag("Player"))
        {
            
            StartCoroutine(SpawnEnemy());
            CloseDoor();
            Destroy(boxColl);
        }
    }

    IEnumerator SpawnEnemy()
    {
        Animator[] spawnAnimator = GetComponentsInChildren<Animator>();
        foreach (Animator ani in spawnAnimator)
        { ani.SetBool("Spawn",true); }

        yield return new WaitForSeconds(spawnDelay);
        
        foreach (Animator ani in spawnAnimator)
        { ani.SetBool("Spawn", false); }

        foreach (Transform spawn in enemySpawn1)
        {
            if (enemySpawn1 == null) continue;
            else
            {
                GameObject enemy1 = enemyPool.GetEnemy(EnemyType.Enemy1);
                enemy1.transform.position = spawn.position;
                remainingEnemy++;
                enemy1.GetComponent<Enemy1>().OnDeath += EnemyDefeated;
            }
        }

        foreach (Transform spawn in enemySpawn2)
        {
            if (enemySpawn2 == null) continue;
            else
            {
                GameObject enemy2 = enemyPool.GetEnemy(EnemyType.Enemy2);
                enemy2.transform.position = spawn.position;
                remainingEnemy++;
                enemy2.GetComponent<Enemy2>().OnDeath += EnemyDefeated;
            }
        }

        foreach (Transform spawn in enemySpawn3)
        {
            if (enemySpawn3 == null) continue;
            else
            {
                GameObject enemy3 = enemyPool.GetEnemy(EnemyType.Enemy3);
                enemy3.transform.position = spawn.position;
                remainingEnemy++;
                enemy3.GetComponent<Enemy1>().OnDeath += EnemyDefeated;
            }
        }
    }
    void CloseDoor()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }
    void OPenDoor()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }
    void EnemyDefeated()
    {
        remainingEnemy--;

        if (remainingEnemy <= 0)
        {
            OPenDoor();
            if (chest != null)
                chest.SetActive(true);
            else
            {
                return;
            }
        }
    }
}
