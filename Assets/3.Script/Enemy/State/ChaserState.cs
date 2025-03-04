using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState :IEnemyState
{
    public void EnterState(EnemyBase enemy)
    {
        
    }
    public void UpdateState(EnemyBase enemy)
    {
        if (enemy.Target == null) return;

        float distancePlayer = Vector2.Distance(enemy.transform.position, enemy.Target.position);

        if (distancePlayer <= enemy.detectRange)
        {
            enemy.SetState(new AttackState());
        }
        else
        {
            Vector2 direction = (enemy.Target.position - enemy.transform.position).normalized;
            enemy.Move(direction);
        }
    }
    
}
