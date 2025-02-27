using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] float fireRate = 0.5f;

    Animator animator;
    Rigidbody2D rb;
    PlayerInputHandler inputHandler;
    float lastfire = 0f;
    Vector3 aim;
    
    void Start()
    {
        TryGetComponent<Rigidbody2D>(out rb);
        TryGetComponent<PlayerInputHandler>(out inputHandler);

    }
    void Update()
    {
        FlipToMouse();
       aim=Mouse.Instance.GetMousePos();
        
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
            GameObject newBullet = Instantiate(bullet, gun.position, Quaternion.identity);

            lastfire = Time.time;
        }
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
