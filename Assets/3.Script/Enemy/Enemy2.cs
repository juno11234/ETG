using UnityEngine;

public class Enemy2 : EnemyBase
{
    [SerializeField]
    Transform enemyGun;

    [SerializeField]
    int bulletCount = 5;

    [SerializeField]
    float spread = 30f;

    public override void Attack()
    {
        if (Time.time > lastAttack + attakcCooldown)
        {
            float startAngle = -spread / 2;
            float angleStep = spread / (bulletCount - 1);

            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + (angleStep * i);
                Quaternion rotation = Quaternion.Euler(0, 0, angle);
                GameObject bullet = bulletPool.GetBullet();
                bullet.transform.position = enemyGun.position;
                Vector2 spreadDirection = rotation * ((Target.position - enemyGun.position).normalized);
                bullet.GetComponent<EnemyBullet>().Direction(spreadDirection);
            }

            lastAttack = Time.time;
        }

    }

}
