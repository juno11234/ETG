using UnityEngine;

public class Box : MonoBehaviour
{
    BoxCollider2D collider;
    CapsuleCollider2D collider2;
    int layerChange = 0;
    SpriteRenderer sprite;
    protected Animator animator;
    void Start()
    {

        TryGetComponent<BoxCollider2D>(out collider);
        TryGetComponent<CapsuleCollider2D>(out collider2);
        TryGetComponent<Animator>(out animator);
        TryGetComponent<SpriteRenderer>(out sprite);
    }

    protected void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("bullet") || coll.CompareTag("EnemyBullet"))
        {
            Broken();
        }
    }
    protected void Broken()
    {
        animator.SetTrigger("Broken");
        sprite.sortingOrder = layerChange;
        if (collider != null)
            Destroy(collider);
        else
            Destroy(collider2);
    }
}
