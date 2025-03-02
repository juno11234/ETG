using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    Transform gun;

    [SerializeField]
    float fireRate = 0.5f;

    [SerializeField]
    float rollSpeed = 7f;

    [SerializeField]
    float rollCool = 1.5f;

    [SerializeField]
    float rollDuration = 0.5f;

    [SerializeField]
    int HP = 6;

    [SerializeField]
    GameObject hand;

    PBPooling bulletPool;
    Animator animator;
    Rigidbody2D rb;
    PlayerInputHandler inputHandler;
    
    float lastfire = 0f;
    bool die=false;
    float lastRoolTime = -100f;
    Vector2 aim;
    bool rolling = false;
    bool invincible = false;
    SpriteRenderer spriteRenderer;
    Color originalColor;
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<PlayerInputHandler>(out inputHandler);
        bulletPool = FindAnyObjectByType<PBPooling>();
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        TryGetComponent<Animator>(out animator);
        originalColor = spriteRenderer.color;

    }
    void Update()
    {
        FlipToMouse();
        aim = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (rolling||die) return;
        rb.velocity = inputHandler.moveDirection * moveSpeed;
    }
    public void Fire()
    {
        if (rolling ||die) return;
        if (Time.time > lastfire + fireRate)
        {
            Vector2 direction = (aim - (Vector2)gun.position).normalized;
            GameObject newBullet = bulletPool.GetBullet();

            if (newBullet != null)
            {
                newBullet.transform.position = gun.transform.position;
                if (newBullet.TryGetComponent(out PlayerBullet bulletScript))
                { bulletScript.Direction(direction); }
            }
            lastfire = Time.time;
        }
    }
    public void Roll()
    {
        if (rolling || Time.time < lastRoolTime + rollCool ||die) return;

        rolling = true;
        invincible = true;
        lastRoolTime = Time.time;

        StartCoroutine(Roll_Co());
    }

    IEnumerator Roll_Co()
    {        
        float elapsedTime = 0f;
        Vector2 rollDirection = (aim - (Vector2)transform.position).normalized;

        hand.SetActive(false);
        animator.SetBool("Rolling", true);

        while (elapsedTime < rollDuration)
        {
            rb.velocity = rollDirection * rollSpeed;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
       ;
        animator.SetBool("Rolling", false);
        hand.SetActive(true);
        rolling = false;
        invincible = false;
    }
     void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("EnemyBullet") && !die)
        {
            TakeDamage();
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy")&&!die)
        {
            TakeDamage();
        }
    }

     void TakeDamage()
    {
        if (invincible) return;

        StartCoroutine(FlashRed());
        HP -= 1;
        if (HP <= 0)
        {
            Die();            
        }
    }
    IEnumerator FlashRed()
    {
        invincible=true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        spriteRenderer.color = originalColor;
        invincible = false;
    }
    
    void Die()
    {                
        hand.SetActive(false);
        animator.SetTrigger("Die");
        die = true;
    }

    void FlipToMouse()
    {

        bool right = transform.localScale.x > 0;
        bool flip = (aim.x > transform.position.x && !right) ||
                    (aim.x < transform.position.x && right);

        if (flip)
        {
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }
    }

    
}
