using System.Collections.Generic;
using UnityEngine;

public class PBPooling : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] int poolSize = 20;
    Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            Instantiate(bullet);
        }
    }
}
