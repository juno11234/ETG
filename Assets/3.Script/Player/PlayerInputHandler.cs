using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float fire { get; private set; }
    public float roll { get; private set; }
    public Vector2 moveDirection { get; private set; }
    PlayerControl playerControl;
    private void Start()
    {
        TryGetComponent<PlayerControl>(out playerControl);
    }
    void OnMove(InputValue value)
    {
        moveDirection = value.Get<Vector2>();
    }
    void OnFire(InputValue value)
    {
        fire = value.Get<float>();
        if (fire > 0)
        {

            playerControl.Fire();
        }
    }
    void OnRoll(InputValue value)
    {
        roll = value.Get<float>();
        if (roll > 0)
        {
            playerControl.Roll();
        }
    }
}
