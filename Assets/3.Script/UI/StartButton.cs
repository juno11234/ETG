using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class StartButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    TextMeshProUGUI buttonText;

    [SerializeField]
    int normalFontSize = 125;

    [SerializeField]
    int hoverFontSize = 135;

    void Start()
    {
        buttonText.fontSize = normalFontSize;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.fontSize = hoverFontSize;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.fontSize = normalFontSize;
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
