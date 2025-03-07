using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ClearUI : MonoBehaviour
{
    [SerializeField]
    GameObject clearUI;
    [SerializeField]
    Animator bookAni;
    bool clear = false;
    PlayerInputHandler playerInput;

    void Start()
    {
        bookAni.gameObject.SetActive(false);
        clearUI.SetActive(false);
        playerInput = FindAnyObjectByType<PlayerInputHandler>();
    }
    public void Clear()
    {
        if (clear) return;
            clear = true;

        StartCoroutine(GameClear_Co());
    }
    IEnumerator GameClear_Co()
    {
        Time.timeScale = 0f;
        bookAni.gameObject.SetActive(true);
        bookAni.updateMode = AnimatorUpdateMode.UnscaledTime;
        bookAni.SetTrigger("Open");
        yield return new WaitForSecondsRealtime(2.2f);
        clearUI.SetActive(true);

        StartCoroutine(WaitForRestart());
    }
    IEnumerator WaitForRestart()
    {
        while (true)
        {
            if (playerInput.restart > 0)
            {
                SceneManager.LoadScene("Intro");
                Time.timeScale = 1f;
                yield break;
            }
            yield return null;
        }
    }
}
