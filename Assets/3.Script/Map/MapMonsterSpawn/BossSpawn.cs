using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject[] doors;

    [SerializeField]
    GameObject boss;

    BoxCollider2D boxColl;

    void Start()
    {
        TryGetComponent<BoxCollider2D>(out boxColl);
        boss.SetActive(false);
        foreach (GameObject door in doors)
        {
            door.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {            
            CloseDoor();
            Destroy(boxColl);
            BossActive();
        }
    }
    void CloseDoor()
    {
        foreach (GameObject door in doors)
        {
            door.SetActive(true);
        }
    }
    void BossActive()
    {
        boss.SetActive(true);
    }
}
