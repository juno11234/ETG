using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : EnemyState
{
  public override void EnterState(EnemyBase enemy)
    {
        Debug.Log("enemy�߰ݻ�Ÿ");
    }
    public override void UpdateState(EnemyBase enemy)
    {
        
    }

    public override void ExitState(EnemyBase enemy)
    {
        Debug.Log("enemy�߰��ߴ�");
    }



}
