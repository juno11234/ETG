using UnityEngine;
using System;
public class Enemy1 : EnemyBase
{
    [SerializeField] 
    Transform enemyGun; 
   
    public override void Attack()
    {
        if (Time.time > lastAttack + attakcCooldown)
        {
            GameObject bullet = bulletPool.GetBullet();
            bullet.transform.position = enemyGun.position;
            bullet.GetComponent<EnemyBullet>().Direction((Target.position - enemyGun.position).normalized);

            lastAttack = Time.time;
            
        }
        
    }


}
