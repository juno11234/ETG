using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float fire { get; private set; }
    public Vector2 moveDirection { get; private set; }
    PlayerControl playerControl;

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    void OnFire(InputValue value)
    {
        fire=value.Get<float>();
        if (fire > 0)
        {
            TryGetComponent<PlayerControl>(out playerControl);
            playerControl.Fire();
        }
        
    }
}
