using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed=5f;
    [SerializeField] float bullettime=10f;
    Rigidbody2D rigid;
    PBPooling bulletPool;


    void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigid);
        bulletPool = FindAnyObjectByType<PBPooling>();
    }

   
    public void Direction(Vector2 direction)
    {
        direction = direction.normalized;
        rigid.velocity = direction * bulletSpeed;
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Enemy") || coll.CompareTag("Wall"))
            { bulletPool.ReturnBullet(gameObject); }
    }


}
