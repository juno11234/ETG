using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;

    [SerializeField]
    float speed = 3f;

    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<SpriteRenderer>(out spriteRenderer);

        TrapMove();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            TrapFlip();
        }
    }
    void TrapMove()
    {
        rb.velocity = new Vector2(speed, rb.velocity.x);
    }
    void TrapFlip()
    {
        speed = -speed;
        rb.velocity = new Vector2(speed, rb.velocity.x);
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
