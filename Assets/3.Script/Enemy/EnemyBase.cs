using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;

    [SerializeField]
    Animator animator;
    
    [SerializeField]
    float dieDelay = 1f;
    
    [SerializeField]
    protected int hp = 2;
    
    [SerializeField]
    protected float attakcCooldown = 2f;
    
    [SerializeField]
    GameObject bulletPrefab;

    
    public Transform Target;
    public float detectRange = 5f;

    protected float lastAttack = -100f;
    protected int attackDMG = 1;
    protected EnemyBulletPool bulletPool;
    protected bool die = false;
    CapsuleCollider2D capsule;
    IEnemyState currentState;    
    SpriteRenderer spriteRender;
    Color originalColor;


    protected void Awake()
    {
        TryGetComponent<CapsuleCollider2D>(out capsule);
        TryGetComponent<Animator>(out animator);
        TryGetComponent<SpriteRenderer>(out spriteRender);
        originalColor = spriteRender.color;
        SetState(new ChaseState());
        bulletPool = FindAnyObjectByType<EnemyBulletPool>();
    }
    private void Update()
    {
        //Debug.Log($"[EnemyBase] 현재 위치: {transform.position}, 플레이어 위치: {Target.position}");
        Target = FindObjectOfType<PlayerControl>()?.transform;
        Flip();
        if(!die)currentState?.UpdateState(this);        
    }
    public void SetState(IEnemyState newState)
    {
       // Debug.Log($"[EnemyBase] 상태 변경: {currentState?.GetType().Name} → {newState.GetType().Name}");

        currentState = newState;
        currentState.EnterState(this);
    }

    public void Move(Vector2 direction)
    {
         animator.SetBool("Chase", true);
        transform.Translate(direction * speed * Time.deltaTime);        
    }

    public virtual void Attack()
    { }
    protected void TakeDamge()
    {
        StartCoroutine(FlashRed());
        hp -= 1;
        if (hp <= 0)
        {
            die = true;
            Die();
        }
    }
    protected void Flip()
    {
        bool left = transform.localScale.x > 0;
        bool flip = (Target.position.x < transform.position.x && !left)
            || (Target.position.x > transform.position.x && left);
        if (flip)
        {
            transform.localScale = new Vector2(-transform.localScale.x, 1f);
        }
    }

    protected void Die()
    {
        animator.SetTrigger("Die");
        capsule.enabled = false;
        Destroy(gameObject, dieDelay);
    }
    IEnumerator FlashRed()
    {
        spriteRender.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRender.color = originalColor;
    }
     void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("bullet"))
        {
            TakeDamge();
        }
    }
     

}
