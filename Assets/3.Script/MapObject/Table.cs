using System.Collections;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    GameObject TopFlip;

    [SerializeField]
    GameObject BotFlip;

    Animator animator;
    GameObject player;
    PlayerInputHandler inputHandler;

    void Start()
    {
        inputHandler = FindAnyObjectByType<PlayerInputHandler>();
        TryGetComponent<Animator>(out animator);
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && inputHandler.tableAction > 0)
        {
            player = coll.gameObject;
            TableFlip();
        }
    }
    private void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Player") && inputHandler.tableAction > 0)
        {
            player = coll.gameObject;
            TableFlip();
        }
    }
    void TableFlip()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 tablePosition = transform.position;
        float flipY = playerPosition.y - tablePosition.y;

        if (flipY > 0)
        {
            StartCoroutine(FlipAni(BotFlip, "Bottom"));
        }
        else
        {
            StartCoroutine(FlipAni(TopFlip, "Top"));
        }
    }
    IEnumerator FlipAni(GameObject preFab, string trigger)
    {
        animator.SetTrigger(trigger);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        Destroy(gameObject);
        Instantiate(preFab, position, rotation);
    }
}
