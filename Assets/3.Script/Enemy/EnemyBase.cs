using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] Animator animator;
    [SerializeField] float dieDelay = 1f;
    [SerializeField] protected int hp=2;
    protected int attackDMG = 1;
   
    SpriteRenderer spriteRender;
    Color originalColor;


    protected void Awake()
    {
        TryGetComponent<Animator>(out animator);
        TryGetComponent<SpriteRenderer>(out spriteRender);
        originalColor = spriteRender.color;
    }

    protected void Move(Vector2 direction)
    {

        transform.position = direction * speed * Time.deltaTime;
    }


    public void TakeDamge()
    {
        StartCoroutine(FlashRed());
        hp -= 1;
        if (hp <= 0)
        {
            Die();
        }
    }
    
    protected void Die()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, dieDelay);
    }
    IEnumerator FlashRed()
    {
        spriteRender.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRender.color = originalColor;
    }


}
