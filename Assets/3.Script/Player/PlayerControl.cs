using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject newBullet;
    [SerializeField] Transform gun;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] float rollSpeed = 7f;
    [SerializeField] int HP = 6;
    

    PBPooling bulletPool;
    Animator animator;
    Rigidbody2D rb;
    PlayerInputHandler inputHandler;
    float lastfire = 0f;
    Vector3 aim;

    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<PlayerInputHandler>(out inputHandler);
        bulletPool = FindAnyObjectByType<PBPooling>();
    }
    void Update()
    {
        FlipToMouse();
        aim = Mouse.Instance.GetMousePos();
    }
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        rb.velocity = inputHandler.moveDirection * moveSpeed;
    }
    public void Fire()
    {
        if (Time.time > lastfire + fireRate)
        {
            Vector2 direction = (aim - gun.position).normalized;
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
        
    }
    void TakeDamage()
    {
        HP -= 1;
        if (HP <= 0)
        {
            Die();
        }
    }
    void Die()
    {

    }
    
   
    void ReturnToPool()
    {
        bulletPool.ReturnBullet(gameObject);
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
