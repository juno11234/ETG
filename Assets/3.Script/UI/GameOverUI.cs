using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    CanvasGroup fadeCanvas;

    [SerializeField]
    Image crosshair;

    [SerializeField]
    Animator bookAni;

    [SerializeField]
    GameObject gameOverUI;

    [SerializeField]
    float fadeDuration = 1.5f;

    [SerializeField]
    float shakeIntensity = 2f;

    [SerializeField]
    float shakeDuration = 1f;

    bool isGameOver = false;
    PlayerInputHandler playerInput;
    CinemachineVirtualCamera vCam;
    float originalAmplitude;
    float originalFrequency;
    SpriteRenderer playerRenderer;
    Animator playerAnimator;
    void Start()
    {
        playerInput = FindAnyObjectByType<PlayerInputHandler>();
        fadeCanvas.gameObject.SetActive(false);
        crosshair.gameObject.SetActive(false);
        gameOverUI.SetActive(false);
        bookAni.gameObject.SetActive(false);
        vCam = FindAnyObjectByType<CinemachineVirtualCamera>();
        //playerAnimator = TryGetComponent<Animator>(out playerInput)(); 틀림
        playerInput.TryGetComponent<Animator>(out playerAnimator);
        playerRenderer = playerInput.GetComponent<SpriteRenderer>();
        //playerAnimator=playerInput.GetComponent<Animator>();
        var noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        

    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;

        StartCoroutine(GameOver_Co());
    }
    IEnumerator GameOver_Co()
    {
        Time.timeScale = 0f;

        float elapsedTime = 0;
        fadeCanvas.gameObject.SetActive(true);
        while (elapsedTime < fadeDuration)
        {
            fadeCanvas.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeCanvas.alpha = 1;
        playerRenderer.color = new Color(1, 1, 1, 1);


        crosshair.gameObject.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);
        yield return StartCoroutine(CameraShake());
        yield return new WaitForSecondsRealtime(0.2f);


        playerAnimator.SetTrigger("Die");
        yield return new WaitForSecondsRealtime(1f);
        bookAni.gameObject.SetActive(true);
        bookAni.updateMode = AnimatorUpdateMode.UnscaledTime;
        bookAni.SetTrigger("Open");
        yield return new WaitForSecondsRealtime(2.2f);
        gameOverUI.SetActive(true);

        StartCoroutine(WaitForRestart());
    }

    IEnumerator CameraShake()
    {
        var noise = vCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        originalAmplitude = noise.m_AmplitudeGain;
        originalFrequency = noise.m_FrequencyGain;
        float elapsedTime = 0f;
        //  흔들림 강도 적용
        noise.m_AmplitudeGain = shakeIntensity;
        noise.m_FrequencyGain = 4f;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        //  원래 값으로 복구
        noise.m_AmplitudeGain = originalAmplitude;
        noise.m_FrequencyGain = originalFrequency;
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
