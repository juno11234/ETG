using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed=5f;
    [SerializeField] float bullettime=10f;
    Rigidbody2D rigid;
    
    
    void Awake()
    {
        TryGetComponent<Rigidbody2D>(out rigid);
        
    }

   
    public void Direction(Vector2 direction)
    {
        direction = direction.normalized;
        rigid.velocity = direction * bulletSpeed;
    }


}
