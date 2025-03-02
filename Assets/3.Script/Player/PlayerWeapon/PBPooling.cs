using System.Collections.Generic;
using UnityEngine;

public class PBPooling : MonoBehaviour
{
    [SerializeField] 
    GameObject bullet;
    
    [SerializeField] 
    int poolSize = 20;
    
    Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj=Instantiate(bullet);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }
    public GameObject GetBullet()
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = Instantiate(bullet);
        }
        obj.SetActive(true);
        return obj;
    }
    public void ReturnBullet(GameObject obj)
    {
        pool.Enqueue(obj);
        obj.SetActive(false);

    }
}
