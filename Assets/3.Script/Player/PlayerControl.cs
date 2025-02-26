using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;    

    Animator animator;
    Rigidbody2D rb;
    PlayerInputHandler inputHandler;
    
     void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<PlayerInputHandler>(out inputHandler);
    }
    void Update()
    {       
        FlipToMouse();
    }
     void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = inputHandler.moveDirection * moveSpeed;
    }
    
    void FlipToMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool right = transform.localScale.x > 0;
        bool flip = (mousePosition.x > transform.position.x && !right) ||
                    (mousePosition.x < transform.position.x && right);

        if (flip)
        {
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }
        
    }
}
