using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    float fire; 
    float roll; 
    float reload;
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
    void OnReload(InputValue value)
    {
        reload = value.Get<float>();
        if (reload > 0)
        {
            playerControl.Reload();
        }
    }
}
