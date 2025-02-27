using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    public abstract void EnterState(EnemyBase enemy);
    public abstract void UpdateState(EnemyBase enemy);
    public abstract void ExitState(EnemyBase enemy);

}
