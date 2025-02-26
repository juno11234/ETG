using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 moveDirection { get; private set; }

    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    void OnFire(InputValue value)
    {

    }
}
