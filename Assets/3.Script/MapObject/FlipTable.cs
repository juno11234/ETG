using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipTable : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    BoxCollider2D box;
    int tableHp = 10;
    void Start()
    {
        TryGetComponent<Animator>(out animator);
        TryGetComponent<SpriteRenderer>(out sprite);
        TryGetComponent<BoxCollider2D>(out box);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("bullet") || coll.CompareTag("EnemyBullet"))
        {
            tableHp--;
            if (tableHp <= 0)
            {
                BrokenTable();
            }
        }
    }
    void BrokenTable()
    {
        animator.SetTrigger("Broken");
        Destroy(box);
        sprite.sortingOrder = 0;
    }
}
