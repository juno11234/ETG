using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BossHPUI : MonoBehaviour
{
    public Slider hpBar;
    public GameObject hpBarContainer;
    public TextMeshProUGUI bosstext;

    float maxHP;
    

    public void Initialize(float bossMaxHP)
    {
        maxHP = bossMaxHP;
        
        hpBar.maxValue = maxHP;
        hpBar.value = maxHP;
        
        hpBarContainer.SetActive(false);
        bosstext.gameObject.SetActive(false);
    }
    public void Show()
    {
        hpBarContainer.SetActive(true);
        bosstext.gameObject.SetActive(true);
    }
    public void Hide()
    {
        hpBarContainer.SetActive(false);
        bosstext.gameObject.SetActive(false);
    }
    public void UpdateHP(float currentHP)
    {
        hpBar.value = currentHP;
    }
}
