using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;

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
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Enemy") || coll.CompareTag("Wall")|| coll.CompareTag("Door")||coll.CompareTag("MapObject"))
        {
            bulletPool.ReturnBullet(gameObject);
        }
    }


}
