using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState
{
    void EnterState(EnemyBase enemy);
     void UpdateState(EnemyBase enemy);
     void ExitState(EnemyBase enemy);

}
