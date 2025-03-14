using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    int maxAmmo = 6;

    [SerializeField]
    float reloadTime = 2f;

    [SerializeField]
    Animator gunAni;

    [SerializeField]
    Image hurtEffect;

    PBPooling bulletPool;
    Animator animator;
    Rigidbody2D rb;
    PlayerInputHandler inputHandler;

    int currentAmmo;
    bool reloading = false;
    public static bool rolling = false;
    float lastfire = 0f;
    bool die = false;
    float lastRoolTime = -100f;
    Vector2 aim;
    
    SpriteRenderer spriteRenderer;
    Color originalColor;
    Coroutine reloadCoroutine;
    BulletUI bulletUi;
    HPUI hpUI;
    GameOverUI gameOverUI;

    void Start()
    {
        currentAmmo = maxAmmo;
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<PlayerInputHandler>(out inputHandler);
        bulletPool = FindAnyObjectByType<PBPooling>();
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        TryGetComponent<Animator>(out animator);
        originalColor = spriteRenderer.color;
         hpUI= FindAnyObjectByType<HPUI>();
        bulletUi = FindAnyObjectByType<BulletUI>();
        bulletUi.UpdateAmmo(currentAmmo);
        gameOverUI = FindAnyObjectByType<GameOverUI>();

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
        if (rolling || die) return;
        rb.velocity = inputHandler.moveDirection * moveSpeed;
    }
    public void Fire()
    {
        if (rolling || die || reloading || currentAmmo <= 0) return;
        if (Time.time > lastfire + fireRate)
        {
            currentAmmo--;
            bulletUi.UpdateAmmo(currentAmmo);
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
        if (currentAmmo == 0)
        {
            StartCoroutine(Reload_Co());
        }
    }
    public void Reload()
    {
        if (rolling || die || reloading || currentAmmo == maxAmmo) return;
        reloadCoroutine = StartCoroutine(Reload_Co());
    }
    IEnumerator Reload_Co()
    {
        gunAni.SetBool("Reload", true);
        reloading = true;
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        bulletUi.UpdateAmmo(currentAmmo);
        gunAni.SetBool("Reload", false);
        reloading = false;
    }
    void CancelReload()
    {
        if (reloadCoroutine != null)
        {
            StopCoroutine(reloadCoroutine);
            reloadCoroutine = null;
        }

        reloading = false;

    }
    public void Roll()
    {
        if (rolling || Time.time < lastRoolTime + rollCool || die) return;

        if (reloading)
        {
            CancelReload();
        }

       
        rolling = true;
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
        if (coll.gameObject.CompareTag("Trap") || coll.gameObject.CompareTag("Enemy") && !die)
        {
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        if (rolling) return;

        StartCoroutine(FlashRed());
        HP -= 1;
        hpUI.UdateHPUI(HP);
        if (HP <= 0)
        {
            Die();
        }
    }
    IEnumerator FlashRed()
    {
        rolling = true;
        spriteRenderer.color = Color.red;
        hurtEffect.color = new Color(1f, 0f, 0f, 1f);
        yield return new WaitForSeconds(0.5f);
        hurtEffect.color = new Color(1f, 0f, 0f, 0f);
        spriteRenderer.color = originalColor;
        rolling = false;
    }

    void Die()
    {
        hand.SetActive(false);
        gameOverUI.TriggerGameOver();
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
