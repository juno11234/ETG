using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState :IEnemyState
{
        
    public void EnterState(EnemyBase enemy)
    {

    }
    public void UpdateState(EnemyBase enemy)
    {
        if (enemy.Target == null) return;
        float distancePlayer = Vector2.Distance(enemy.transform.position, enemy.Target.position);
       
        // Debug.Log($"[AttackState] 현재 거리: {distancePlayer}, detectRange: {enemy.detectRange}");

        if (distancePlayer > enemy.detectRange)
        {
            enemy.SetState(new ChaseState());
           
            //Debug.Log("추격변경");
        }
        else
        {
            enemy.Attack();
            
        }
    }
  



}
