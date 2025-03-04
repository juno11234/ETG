using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float bulletSpeed = 5f;

    Rigidbody2D rigid;
    EnemyBulletPool bulletPool;
    
    void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigid);
        bulletPool = FindAnyObjectByType<EnemyBulletPool>();
    }

    public void Direction(Vector2 direction)
    {
        direction = direction.normalized;
        rigid.velocity = direction * bulletSpeed;
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player") && !PlayerControl.playerInvincible )
        {            
            bulletPool.ReturnBullet(gameObject);
        }
        if (coll.CompareTag("Wall")|| coll.CompareTag("Door"))
        {
            bulletPool.ReturnBullet(gameObject);
        }
    }
}
