using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : EnemyBase
{
    [SerializeField]
    Transform[] firePoints;

    [SerializeField]
    Transform pattern2fire;

    [SerializeField]
    float attackCool2=3f;

    [SerializeField]
    int HpForPattern=30;

    BossHPUI hpBar;
   


    float lastAttack2 = 0f;
    int bulletCount = 5;
    int circleBullet = 30;
    float spreadAngle = 30f;    


    public void Initialize(BossHPUI hpBarRef)
    {
        hpBar = hpBarRef;
        hpBar.Initialize(hp);
        
    }

    public override void Attack()
    {
        if (Time.time > lastAttack + attakcCooldown)
        {            
            AttackPattern1();
            lastAttack = Time.time;
        }
        if (Time.time > lastAttack2 + attackCool2)
        {
            AttackPattern2();
            lastAttack2 = Time.time;
        }
        if (Hp<HpForPattern)
        {
            attackCool2 = 2f;
            attakcCooldown = 0.5f;
            bulletCount = 8;
            circleBullet = 50;
        }

    }
    void AttackPattern1()//¿ø»Ô
    {

        foreach (Transform firePoint in firePoints)
        {
            float startAngle = -spreadAngle / 2;
            float angleStep = spreadAngle / (bulletCount - 1);

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + (angleStep * i);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                GameObject bullet = bulletPool.GetBullet();
                bullet.transform.position = firePoint.position;
                Vector2 spreadDirection = rotation * ((Target.position - firePoint.position).normalized);
                bullet.GetComponent<EnemyBullet>().Direction(spreadDirection);
            }
        }
    }
    void AttackPattern2()//¿øÇü
    {
        float angleStep = 360f / circleBullet;

        for(int i = 0; i < circleBullet; i++)
        {
            float angle = angleStep * i;
            Quaternion rotation = Quaternion.Euler(0, 0, angle);
            GameObject bullet = bulletPool.GetBullet();
            bullet.transform.position = pattern2fire.position;
            Vector2 circleDirection = rotation * Vector2.right;
            bullet.GetComponent<EnemyBullet>().Direction(circleDirection);
        }
    }
    protected override void TakeDamge()
    {
        base.TakeDamge();
        hpBar.UpdateHP(hp);

        if (hp <= 0)
        {
            hpBar.Hide();
            PlayerControl.rolling = true;
            
        }
    }
   
}
